using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using Finance.Framework.Core.Graphics.Python;
using Finance.Framework.Core.Graphics;
using Finance.Framework.Core.Serialization;
using System.ComponentModel;

namespace Finance.Framework.Core
{
    [ToolboxBitmap(typeof(EmbeddedResourceFinder), "Finance.Framework.Core.Resources.brick_edit.png")]
    public partial class MarketPython : UserControl, IGfxModule
    {
        [Browsable(true), Description("ID of this Market Control."), Category("Myaki"), DefaultValue(null)]
        public Guid ID { get { return _ID; } set { _ID = value; } }

        [Browsable(true), Description("Code."), Category("Myaki"), DefaultValue("")]
        public string[] Code { get { return codeEditor.Document.Lines; } }

        [Browsable(true), Description("Is control is started."), Category("Myaki"), DefaultValue(false)]
        public bool IsStarted { get { return mnuPlay.Checked; } }

        [Browsable(true), Description("Show menubar."), Category("Myaki"), DefaultValue(true)]
        public bool ShowMenuBar { get { return toolMenu.Visible; } set { toolMenu.Visible = value; codeEditor.Location = toolMenu.Visible ? new Point(0, 0) : new Point(0, 25); } }

        public SerializableDictionary<string, object> Constants { get { return _pythonExecuter.Constants; } }
        public event UnlinkedEventHandler UnlinkedEvent;
        public event MouseEventHandler MoveRequest;

        private Guid _ID;
        private event MessageEventHandler _MessageEvent;
        private readonly IList<GfxMessage> _lstInMessages = new List<GfxMessage>();
        private readonly IList<GfxMessage> _lstOutMessages = new List<GfxMessage>();
        private readonly PythonExecuter _pythonExecuter;
        private readonly MarketCtrlHelper _helper;

        #region Instance & Dispose
        public MarketPython()
        {
            InitializeComponent();
            _pythonExecuter = new PythonExecuter(this);
            ID = Guid.NewGuid();
            _helper = new MarketCtrlHelper(this, new Control[] { codeEditor, txtLog }, toolMenu);
            _helper.MessageEvent += NewMessageEvent;
            toolOut.MouseDown += _helper.StartDrag;
            _pythonExecuter.StdOut += Executer_StdOut;
            _pythonExecuter.ErrOut += Executer_ErrOut;
            if (File.Exists(Application.StartupPath + "\\IronPython.syn"))
                codeEditor.Document.SyntaxFile = Application.StartupPath + "\\IronPython.syn";
            Disposed += MarketLogger_Disposed;
            ReadSnippetsXMLFile();
        }
        public MarketPython(MWFBase mwf)
            : this()
        {
            _pythonExecuter = new PythonExecuter(this);
            ID = mwf.ID;
            MWFPython mwfData = mwf as MWFPython;
            if (mwfData == null)
                throw new NotSupportedException();
            _pythonExecuter.Constants = mwfData.Constants;

            for (int c = 0; c < mwfData.Code.Length; c++)
                mwfData.Code[c] = mwfData.Code[c].TrimEnd('\n');
                
            codeEditor.Document.Lines = mwfData.Code;
            if (mwfData.IsStarted)
                mnuPlay_Click(null, null);
        }

        /// <summary>
        /// Reads the snippets XML file.
        /// </summary>
        private void ReadSnippetsXMLFile()
        {
            if (!File.Exists(Application.StartupPath + "\\Snippets.xml")) return;
            XmlTextReader xmlReader = new XmlTextReader(new StreamReader(Application.StartupPath + "\\Snippets.xml"));
            while (xmlReader.Read())
            {
                if (xmlReader.Name != "Snippet") continue;
                string name = xmlReader.GetAttribute("Name");
                string type = xmlReader.GetAttribute("Type");
                string[] code = xmlReader.ReadElementContentAsString().Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                switch (type)
                {
                    case "MultiData":
                        dataMenuItem.DropDownItems.Add(name, null,
                                                       delegate { for (int c = 0; c < code.Length; c++) codeEditor.Document.Insert(code[c], codeEditor.Caret.CurrentRow.Index + c); });
                        break;
                    case "Data":
                        dataItemMenuItem.DropDownItems.Add(name, null,
                                                           delegate { for (int c = 0; c < code.Length; c++) codeEditor.Document.Insert(code[c], codeEditor.Caret.CurrentRow.Index + c); });
                        break;
                    case "HistoryData":
                        historyDataMenuItem.DropDownItems.Add(name, null,
                                                              delegate { for (int c = 0; c < code.Length; c++) codeEditor.Document.Insert(code[c], codeEditor.Caret.CurrentRow.Index + c); });
                        break;
                    case "MultiHistoryData":
                        multiHistoryMenuItem.DropDownItems.Add(name, null,
                                                              delegate { for (int c = 0; c < code.Length; c++) codeEditor.Document.Insert(code[c], codeEditor.Caret.CurrentRow.Index + c); });
                        break;
                    default:
                        throw new NotImplementedException();

                }
            }
            xmlReader.Close();
        }

        void MarketLogger_Disposed(object sender, EventArgs e)
        {
            if (UnlinkedEvent != null)
                UnlinkedEvent.Invoke(this, this);
            _lstInMessages.Clear();
            _lstOutMessages.Clear();

        }
        #endregion

        #region User events
        private void mnuDebug_Click(object sender, EventArgs e)
        {
            try
            {
                txtLog.Clear();
                if (_lstInMessages.Count > 0)
                    _pythonExecuter.DebugCode(codeEditor.Document.Text, _lstInMessages[0]);
                else
                    _pythonExecuter.DebugCode(codeEditor.Document.Text, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuStep_Click(object sender, EventArgs e)
        {
            try
            {
                txtLog.Clear();
                if (_lstInMessages.Count > 0)
                {
                    GfxMessage msg = _pythonExecuter.StepByStep(codeEditor.Document.Text, _lstInMessages[0]);
                    if (msg != null)
                    {
                        _lstInMessages.RemoveAt(0);
                        PushOutputMessage(msg);
                        toolInputMessage.Text = _lstInMessages.Count.ToString();
                    }
                }
                else
                    MessageBox.Show("Can't do step by step without any input message to process!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuPlay_Click(object sender, EventArgs e)
        {
            try
            {
                txtLog.Clear();
                if (_pythonExecuter.Started)
                    Stop();
                else
                {
                    codeEditor.Enabled = false;
                    mnuPlay.Checked = true;
                    _pythonExecuter.Start(codeEditor.Document.Text);
                    ProcessInMessages();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Stop()
        {
            _pythonExecuter.Stop();
            mnuPlay.Checked = false;
            codeEditor.Enabled = true;
        }

        private void toolInputMessage_Click(object sender, EventArgs e)
        {
            FrmMessagesDetails frmDetails = new FrmMessagesDetails(_lstInMessages);
            frmDetails.ShowDialog();
            if (_lstInMessages.Count == 0)
                toolInputMessage.Visible = false;
            else
                toolInputMessage.Text = _lstInMessages.Count.ToString();
        }

        private void toolStrip_MouseDown(object sender, MouseEventArgs e)
        {
            if (MoveRequest != null)
                MoveRequest.Invoke(sender, e);
        }

        private void toolOutputMessage_Click(object sender, EventArgs e)
        {
            FrmMessagesDetails frmDetails = new FrmMessagesDetails(_lstOutMessages);
            frmDetails.ShowDialog();
            if (_lstOutMessages.Count == 0)
                toolOutputMessage.Visible = false;
            else
                toolOutputMessage.Text = _lstOutMessages.Count.ToString();
        }

        private void mnuVarsBox_Click(object sender, EventArgs e)
        {
            FrmConstants frmConstants = new FrmConstants(_pythonExecuter);
            frmConstants.ShowDialog();
        }
        #endregion

        #region IGfxModule
        public void AddMessage(GfxMessage message)
        {
            NewMessageEvent(this, message, null);
        }

        public void AddMessages(IList<GfxMessage> messages)
        {
            NewMessageEvent(this, null, messages);
        }

        public event MessageEventHandler MessageEvent
        {
            add
            {
                _MessageEvent += value;
                ProcessOutMessages();
            }
            remove
            {
                _MessageEvent -= value;
            }
        }

        public string MyakiName
        {
            get { return "Python"; }
        }

        public Size MyakiSize
        {
            get { return new Size(480, 500); }
        }

        public GraphicBorderStyle MyakiBorderStyle
        {
            get { return GraphicBorderStyle.Sizable; }
        }

        public IList<Guid> InputModules
        {
            get { return _helper.InputModules; }
        }

        public void AddInputModule(IGfxModule module)
        {
            _helper.InputModuleLink(module);
        }
        #endregion

        #region Message Handlers
        /// <summary>
        /// Send all the OUT messages.
        /// </summary>
        private void ProcessOutMessages()
        {
            _MessageEvent.Invoke(this, null, _lstOutMessages);
            _lstOutMessages.Clear();
            toolOutputMessage.Visible = false;
        }

        /// <summary>
        /// Processes all the messages.
        /// </summary>
        private void ProcessInMessages()
        {
            if (_lstInMessages.Count == 1)
            {
                GfxMessage msg = _pythonExecuter.Execute(_lstInMessages[0]);
                if (msg != null)
                {
                    _lstInMessages.RemoveAt(0);
                    PushOutputMessage(msg);
                }
                else
                {
                    _pythonExecuter.Stop();
                    codeEditor.Enabled = true;
                    mnuPlay.Checked = false;
                }
            }
            else if (_lstInMessages.Count > 0)
            {
                List<GfxMessage> newMessages = new List<GfxMessage>();
                while (_lstInMessages.Count > 0)
                {
                    GfxMessage msg = _pythonExecuter.Execute(_lstInMessages[0]);
                    if (msg != null)
                    {
                        newMessages.Add(msg);
                        _lstInMessages.RemoveAt(0);
                    }
                    else
                    {
                        _pythonExecuter.Stop();
                        codeEditor.Enabled = true;
                        mnuPlay.Checked = false;
                        break;
                    }
                }
                PushOutputMessage(newMessages);
            }
            if (_lstOutMessages.Count == 0)
                toolOutputMessage.Visible = false;
            else
            {
                toolOutputMessage.Text = _lstOutMessages.Count.ToString();
                toolOutputMessage.Visible = true;
            }
            if (_lstInMessages.Count == 0)
                toolInputMessage.Visible = false;
            else
            {
                toolInputMessage.Text = _lstInMessages.Count.ToString();
                toolInputMessage.ToolTipText = String.Format("Type: {0} / Message: {1}", _lstInMessages[0].ContentType, _lstInMessages[0].MessageType);
            }
        }

        /// <summary>
        /// Pushes a new input message.
        /// </summary>
        /// <param name="data">The data.</param>
        public void PushInputMessage(GfxMessage data)
        {
            // The Python script was executed successfully
            if (_pythonExecuter.Started)
            {
                GfxMessage outData = _pythonExecuter.Execute(data);
                if (outData != null)
                {
                    PushOutputMessage(outData);
                    return;
                }
            }
            // The Python script WAS NOT executed successfully -> buffer
            _lstInMessages.Add(data);
            toolInputMessage.Text = _lstInMessages.Count.ToString();
            toolInputMessage.ToolTipText = String.Format("Type: {0} / Message: {1}", _lstInMessages[0].ContentType, _lstInMessages[0].MessageType);
            if (!toolInputMessage.Visible)
                toolInputMessage.Visible = true;
        }

        /// <summary>
        /// Pushes the output message for the next modules.
        /// </summary>
        /// <param name="data">The data.</param>
        void PushOutputMessage(GfxMessage data)
        {
            if (_MessageEvent != null && _MessageEvent.GetInvocationList().Length > 0)
                _MessageEvent.Invoke(this, data, null);
            else
            {
                _lstOutMessages.Add(data);
                toolOutputMessage.Text = _lstOutMessages.Count.ToString();
                toolOutputMessage.ToolTipText = String.Format("Type: {0} / Message: {1}", _lstOutMessages[0].ContentType, _lstOutMessages[0].MessageType);
                if (!toolOutputMessage.Visible)
                    toolOutputMessage.Visible = true;
            }
        }

        /// <summary>
        /// Pushes the output message for the next modules.
        /// </summary>
        /// <param name="data">The data.</param>
        void PushOutputMessage(IList<GfxMessage> data)
        {
            if (_MessageEvent != null && _MessageEvent.GetInvocationList().Length > 0)
                _MessageEvent.Invoke(this, null, data);
            else
            {
                foreach (GfxMessage msg in data)
                    _lstOutMessages.Add(msg);
                toolOutputMessage.Text = _lstOutMessages.Count.ToString();
                toolOutputMessage.ToolTipText = String.Format("Type: {0} / Message: {1}", _lstOutMessages[0].ContentType, _lstOutMessages[0].MessageType);
                if (!toolOutputMessage.Visible)
                    toolOutputMessage.Visible = true;
            }
        }

        void NewMessageEvent(GfxMessage data)
        {
            if (data.MessageType == GfxType.Information)
                return;
            PushInputMessage(data);
        }

        void NewMessageEvent(object source, GfxMessage msg, IList<GfxMessage> lst)
        {
            if (InvokeRequired)
            {
                Invoke(new MessageEventHandler(NewMessageEvent), new object[] { source, msg, lst });
                return;
            }
            if (msg != null)
                NewMessageEvent(msg);
            else
                foreach (GfxMessage current in lst)
                    NewMessageEvent(current);
        }

        void Executer_StdOut(object sender, string txt)
        {
            txtLog.SelectionColor = Color.Black;
            txtLog.AppendText(txt + Environment.NewLine);
        }

        void Executer_ErrOut(object sender, string txt, int line)
        {
            Stop();
            txtLog.SelectionColor = Color.Red;
            txtLog.AppendText(txt + Environment.NewLine);
            if (line > 0)
            {
                codeEditor.GotoLine(line);
                codeEditor.Selection.MakeSelection();
                codeEditor.InfoTipText = "Error";
                codeEditor.InfoTipVisible = true;
            }
        }
        #endregion

        #region MWF Base
        public MWFBase GetMWF(string title, Size size, Point location, FormWindowState windowState)
        {
            return new MWFPython(ID, title, size, location, windowState, this, _helper.InputModules);
        }
        #endregion
    }
}
