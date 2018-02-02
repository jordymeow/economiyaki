using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Finance.MarketAccess;
using ZedGraph;
using Finance.Framework.Core.Serialization;
using Finance.Framework.Types;
using System.ComponentModel;
using Finance.Framework.Core.Graphics.Excel;

namespace Finance.Framework.Core.Excel
{
    [ToolboxBitmap(typeof(EmbeddedResourceFinder), "Finance.Framework.Core.Resources.excel.png")]
    public partial class MarketSpreadsheet : UserControl, IGfxModule
    {
        [Browsable(true), Description("ID of this Market Control."), Category("Myaki"), DefaultValue(null)]
        public Guid ID { get { return _ID; } set { _ID = value; } }

        [Browsable(true), Description("Show menubar."), Category("Myaki"), DefaultValue(true)]
        public bool ShowMenuBar { get { return toolMenu.Visible; } set { toolMenu.Visible = value; axSpreadsheet.Location = toolMenu.Visible ? new Point(0, 0) : new Point(0, 25); } }

        public Dictionary<string, string> Definitions = new Dictionary<string, string>();

        public event MessageEventHandler MessageEvent;
        public event MouseEventHandler MoveRequest;
        public event UnlinkedEventHandler UnlinkedEvent;

        private Guid _ID;
        private List<string> _unmanagedDefinitions = new List<string>();
        private readonly MarketCtrlHelper _helper;

        #region Instance & Dispose
        public MarketSpreadsheet()
        {
            InitializeComponent();
            ID = Guid.NewGuid();
            _helper = new MarketCtrlHelper(this, axSpreadsheet, toolMenu);
            toolOut.MouseDown += _helper.StartDrag;
            _helper.MessageEvent += NewMessageEvent;
            _helper.UnlinkEvent += Helper_UnlinkedEvent;
            //TODO: Aiaiai
            Disposed += MarketGraph_Disposed;
        }

        public MarketSpreadsheet(MWFBase mwf)
            : this()
        {
            ID = mwf.ID;
            throw new NotSupportedException();
        }

        void MarketGraph_Disposed(object sender, EventArgs e)
        {
            if (UnlinkedEvent != null)
                UnlinkedEvent.Invoke(this, this);
        }
        #endregion

        #region User Events
        void Helper_UnlinkedEvent(object source, IGfxModule module)
        {
        }

        private void toolStrip_MouseDown(object sender, MouseEventArgs e)
        {
            if (MoveRequest != null)
                MoveRequest.Invoke(sender, e);
        }

        private void mnuLinker_Click(object sender, EventArgs e)
        {
            FrmLinker linker = new FrmLinker(Definitions);
            linker.ShowDialog();
        }

        private void mnuUnmanagedInput_Click(object sender, EventArgs e)
        {
            foreach (string str in _unmanagedDefinitions)
            {
                if (!Definitions.ContainsKey(str))
                    Definitions.Add(str, "");
            }
            mnuUnmanagedInput.Visible = false;
            mnuLinker_Click(sender, e);
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

        [Browsable(false)]
        public string MyakiName
        {
            get { return "Spreadsheet"; }
        }

        [Browsable(false)]
        public Size MyakiSize
        {
            get { return new Size(400, 300); }
        }

        [Browsable(false)]
        public GraphicBorderStyle MyakiBorderStyle
        {
            get { return GraphicBorderStyle.Sizable; }
        }

        [Browsable(false)]
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

        public void ManagerMessage(GfxMessage msg)
        {
            try
            {
                if (msg.ContentType == typeof(Data))
                {
                    Data data = (Data)msg.Content;
                    foreach (DataFieldItem item in data)
                    {
                        string[] cells;
                        string identif = data.Security + "|" + item.Field;
                        if (Definitions.ContainsKey(identif))
                        {
                            string cellStr = Definitions[identif];
                            if (cellStr == null)
                                continue;
                            if (cellStr.Contains(":"))
                                cells = Definitions[identif].Split(':');
                            else if (cellStr.Length == 2)
                            {
                                cells = new string[2];
                                cells[0] = cellStr[0].ToString();
                                cells[1] = cellStr[1].ToString();
                            }
                            else
                                continue;
                            int row = -1, col = -1;
                            if (!int.TryParse(cells[1], out row))
                                continue;
                            if (int.TryParse(cells[0], out col))
                                axSpreadsheet.Cells[row, col] = item.Value;
                            else if (cells[0].Length == 1)
                            {
                                char charact = cells[0][0];
                                col = (int)charact - 64;
                                axSpreadsheet.Cells[row, col] = item.Value;
                            }
                        }
                        else
                        {
                            if (!_unmanagedDefinitions.Contains(identif))
                            {
                                _unmanagedDefinitions.Add(identif);
                                mnuUnmanagedInput.Visible = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void NewMessageEvent(object source, GfxMessage msg, IList<GfxMessage> lst)
        {
            if (InvokeRequired)
            {
                Invoke(new MessageEventHandler(NewMessageEvent), new object[] { source, msg, lst });
                return;
            }
            try
            {
                if (msg != null)
                    ManagerMessage(msg);
                if (lst != null)
                    foreach (GfxMessage current in lst)
                        ManagerMessage(current);
            }
            catch (Exception ex)
            {
                MessageEvent.Invoke(this, new GfxMessage(GfxType.Information, new MiscData(MiscEventType.Info, ex.Message, ex)), null);
            }
        }
        #endregion

        #region MWF Base
        public MWFBase GetMWF(string title, Size size, Point location, FormWindowState windowState)
        {
            return null;
        }
        #endregion
    }
}