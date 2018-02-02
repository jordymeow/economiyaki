using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Finance.MarketAccess;
using Finance.Framework.Core.Serialization;
using Finance.Framework.Types;
using Finance.Framework.Core.Properties;
using System.ComponentModel;

namespace Finance.Framework.Core
{
    [ToolboxBitmap(typeof(EmbeddedResourceFinder), "Finance.Framework.Core.Resources.database_refresh.png")]
    public partial class MarketExport : UserControl, IGfxModule
    {
        public event UnlinkedEventHandler UnlinkedEvent;
        public event MessageEventHandler MessageEvent;
        public event MouseEventHandler MoveRequest;

        [Browsable(true), Description("ID of this Market Control."), Category("Myaki"), DefaultValue(null)]
        public Guid ID { get { return _ID; } set { _ID = value; } }

        [Browsable(true), Description("Show menubar."), Category("Myaki"), DefaultValue(true)]
        public bool ShowMenuBar { get { return toolMenu.Visible; } set { toolMenu.Visible = value; lstStocks.Location = toolMenu.Visible ? new Point(0, 0) : new Point(0, 25); } }

        private Guid _ID;
        private readonly IList<MultiData> _lstMultiData = new List<MultiData>();
        private readonly IList<Data> _lstDataItem = new List<Data>();
        private readonly IList<HistoryData> _lstHistoryData = new List<HistoryData>();
        private readonly IDictionary<string, ListViewItem> _itemsMultiData = new Dictionary<string, ListViewItem>();
        private readonly IDictionary<string, ListViewItem> _itemsDataItem = new Dictionary<string, ListViewItem>();
        private readonly IDictionary<string, ListViewItem> _itemsHistoryData = new Dictionary<string, ListViewItem>();
        private readonly MarketCtrlHelper _helper;
        private Thread _playerThread;
        private bool _threadRunning;
        private int _accelerator = 1;
        private delegate void ApplyTextToPlayButtonHandler(string text);
        private delegate void StopHandler();

        #region Instance & Dispose
        public MarketExport()
        {
            InitializeComponent();
            ID = Guid.NewGuid();
            cboSpeed.SelectedIndex = 0;
            Disposed += MarketExport_Disposed;
            _helper = new MarketCtrlHelper(this, lstStocks, toolMenu);
            _helper.MessageEvent += NewMessageEvent;
            toolOut.MouseDown += _helper.StartDrag;
        }

        public MarketExport(MWFBase mwf)
            : this()
        {
            ID = mwf.ID;
            MWFExport mwfData = mwf as MWFExport;
            if (mwfData == null)
                throw new NotSupportedException();
        }

        void MarketExport_Disposed(object sender, EventArgs e)
        {
            if (UnlinkedEvent != null)
                UnlinkedEvent.Invoke(this, this);
            if (_threadRunning)
                Stop();
        }
        #endregion

        #region User Events
        private void toolStrip_MouseDown(object sender, MouseEventArgs e)
        {
            if (MoveRequest != null)
                MoveRequest.Invoke(sender, e);
        }

        private void mnuClear_Click(object sender, EventArgs e)
        {
            _lstMultiData.Clear();
            _lstDataItem.Clear();
            _lstHistoryData.Clear();
            _itemsMultiData.Clear();
            _itemsDataItem.Clear();
            _itemsHistoryData.Clear();
            lstStocks.Items.Clear();
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

        public string MyakiName
        {
            get { return "Export"; }
        }

        public Size MyakiSize
        {
            get { return new Size(475, 200); }
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
        private void SendOutMessage(object sender, GfxMessage msg, IList<GfxMessage> lstMsg)
        {
            if (MessageEvent != null)
                MessageEvent.Invoke(this, msg, lstMsg);
        }

        delegate void AddDataHandler(Guid id, string security, string group, DateTime time);

        private void AddData(Guid id, string security, string group, DateTime time)
        {
            if (InvokeRequired)
            {
                Invoke(new AddDataHandler(AddData), new object[] { id, security, group, time });
                return;
            }

            ListViewItem viewItem;
            if (_itemsDataItem.ContainsKey(id + security))
                viewItem = _itemsDataItem[id + security];
            else
            {
                viewItem = lstStocks.Items.Add(security);
                _itemsDataItem.Add(id + security, viewItem);
            }
            viewItem.Group = lstStocks.Groups[group];
            if (viewItem.SubItems.Count < 2)
            {
                viewItem.SubItems.Add("1");
                viewItem.SubItems.Add(time.ToShortDateString() + ", " + time.ToShortTimeString());
                viewItem.SubItems.Add(time.ToShortDateString() + ", " + time.ToShortTimeString());
                viewItem.SubItems.Add("1");
                viewItem.SubItems[1].Tag = 1;
            }
            else
                viewItem.SubItems[1].Tag = (int)viewItem.SubItems[1].Tag + 1;
            viewItem.SubItems[3].Text = time.ToShortDateString() + ", " + time.ToShortTimeString();
            viewItem.SubItems[1].Text = viewItem.SubItems[1].Tag.ToString();
        }

        private void PushNewMessage(Data item)
        {
            _lstDataItem.Add(item);
            AddData(item.Guid, item.Security, "Data", item.Time);
        }

        private void PushNewMessage(MultiData data)
        {
            _lstMultiData.Add(data);
            foreach (Data item in data)
                AddData(item.Guid, item.Security, "MultiData", item.Time);
        }

        private void PushNewMessage(HistoryData data)
        {
            _lstHistoryData.Add(data);
            foreach (HistoryDataItem item in data)
                AddData(item.Guid, data.Security, "HistoryData", item.Time);
        }

        void NewMessageEvent(GfxMessage msg)
        {
            if (msg.ContentType == typeof(Data))
                PushNewMessage(msg.Content as Data);
            else if (msg.ContentType == typeof(MultiData))
                PushNewMessage(msg.Content as MultiData);
            else if (msg.ContentType == typeof(HistoryData))
                PushNewMessage(msg.Content as HistoryData);
            else
                throw new NotImplementedException(msg.ContentType + " has not been implemented.");
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
            else if (lst != null)
                foreach (GfxMessage currentMsg in lst)
                    NewMessageEvent(currentMsg);
        }
        #endregion

        #region Files Management
        private void IOCSVDone(IAsyncResult result)
        {
            if (InvokeRequired)
            {
                MethodInvoker method = delegate { picLoad.Visible = false; };
                picLoad.Invoke(method, null);
            }
            else
                picLoad.Visible = false;
        }

        private void ImportDataItemCSV(string path)
        {
            MethodInvoker invoker = delegate
                                            {
                                                bool isFirst = true;
                                                StreamReader writer = new StreamReader(path);
                                                IDictionary<int, string> fields = new Dictionary<int, string>();
                                                while (!writer.EndOfStream)
                                                {
                                                    string currentLine = writer.ReadLine();
                                                    if (isFirst)
                                                    {
                                                        isFirst = false;
                                                        string[] fieldsStr = currentLine.Split(',');
                                                        if (fieldsStr.Length < 3)
                                                            throw new Exception("Incomplete data.");
                                                        for (int c = 0; c + 3 < fieldsStr.Length; c++)
                                                            fields.Add(c, fieldsStr[c + 3]);
                                                        continue;
                                                    }
                                                    string[] splitted = currentLine.Split(',');
                                                    Data item = new Data(new Guid(splitted[0]), splitted[1]);
                                                    item.Time = DateTime.FromOADate(double.Parse(splitted[2]));
                                                    for (int c = 0; c + 3 < splitted.Length; c++)
                                                        if (splitted[c + 3] != "")
                                                            item.Add(fields[c], splitted[c + 3]);
                                                    _lstDataItem.Add(item);
                                                    AddData(item.Guid, item.Security, "Data", item.Time);
                                                }
                                                writer.Close();
                                            };
            invoker.BeginInvoke(IOCSVDone, null);
            picLoad.Visible = true;
        }

        private void ExportDataItemCSV(string path)
        {
            MethodInvoker invoker = delegate
                                        {
                                            if (_lstDataItem.Count <= 0) return;
                                            StringBuilder builder = new StringBuilder();
                                            IDictionary<string, int> fields = new Dictionary<string, int>();
                                            foreach (Data item in _lstDataItem)
                                                foreach (DataFieldItem field in item)
                                                    if (!fields.ContainsKey(field.Field))
                                                        fields.Add(field.Field, fields.Count);

                                            builder.Append("Guid,Security,Date");
                                            foreach (string field in fields.Keys)
                                                builder.Append("," + field);
                                            builder.AppendLine();

                                            IDictionary<Guid, DateTime> lastUpdate = new Dictionary<Guid, DateTime>();
                                            foreach (Data item in _lstDataItem)
                                            {
                                                if (!lastUpdate.ContainsKey(item.Guid))
                                                    lastUpdate.Add(item.Guid, item.Time);
                                                else
                                                {
                                                    DateTime last = lastUpdate[item.Guid];
                                                    if (last.Year == item.Time.Year && last.Month == item.Time.Month &&
                                                        last.Day == item.Time.Day &&
                                                        last.Hour == item.Time.Hour && last.Minute == item.Time.Minute &&
                                                        last.Second == item.Time.Second)
                                                        continue;
                                                    lastUpdate[item.Guid] = item.Time;
                                                }
                                                builder.Append(item.Guid + "," + item.Security + "," +
                                                               item.Time.ToOADate());
                                                string[] tmpArray = new string[fields.Count];
                                                foreach (DataFieldItem field in item)
                                                    tmpArray[fields[field.Field]] = field.Value.ToString();
                                                foreach (string str in tmpArray)
                                                    builder.Append("," + str);
                                                builder.AppendLine();
                                            }
                                            StreamWriter writer = new StreamWriter(path);
                                            writer.Write(builder.ToString());
                                            writer.Close();
                                        };
            invoker.BeginInvoke(IOCSVDone, null);
            picLoad.Visible = true;
        }

        private void mnuLoad_Click(object sender, EventArgs e)
        {
            ToolStripDropDownItem clickedItem = sender as ToolStripDropDownItem;
            if (clickedItem == null) return;
            OpenFileDialog loadDialog = new OpenFileDialog();
            loadDialog.Filter = "CSV File (.csv)|*.csv";
            loadDialog.CheckPathExists = true;
            loadDialog.CheckFileExists = true;
            if (loadDialog.ShowDialog() != DialogResult.OK)
                return;
        OpenFileAgain:
            try
            {
                if (clickedItem.Text == "Data")
                    ImportDataItemCSV(loadDialog.FileName);
                else
                    throw new NotImplementedException();
            }
            catch (IOException ex)
            {
                if (MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation) == DialogResult.Retry)
                    goto OpenFileAgain;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Application.ProductName", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuSave_Click(object sender, EventArgs e)
        {
            ToolStripDropDownItem clickedItem = sender as ToolStripDropDownItem;
            if (clickedItem == null) return;
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.CheckPathExists = true;
            saveDialog.Filter = "CSV File (.csv)|*.csv";
            if (saveDialog.ShowDialog() != DialogResult.OK)
                return;
        SaveFileAgain:
            try
            {
                if (clickedItem.Text == "Data")
                    ExportDataItemCSV(saveDialog.FileName);
                else
                    throw new NotImplementedException();
            }
            catch (IOException ex)
            {
                if (MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.RetryCancel) == DialogResult.Retry)
                    goto SaveFileAgain;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Start / Stop / Player
        private void Stop()
        {
            _threadRunning = false;
            mnuPlay.Text = "";
            mnuPlay.Image = Resources.resultset_next;
        }

        private void Start()
        {
            mnuPlay.Image = Resources.stop;
            _playerThread = new Thread(Player_ThreadFunc);
            _playerThread.IsBackground = true;
            _threadRunning = true;
            _playerThread.Start(_lstDataItem);
        }

        private void Player_ThreadFunc(object o)
        {
            List<Data> itemsToCopy = o as List<Data>;
            if (itemsToCopy == null || itemsToCopy.Count < 1) return;

            List<Data> items = new List<Data>();
            foreach (Data item in itemsToCopy)
                items.Add(item);

            DateTime realStartTime = DateTime.Now;
            DateTime simulationStartTime = DateTime.MaxValue;
            DateTime now = DateTime.Now;
            float weightItem = 100F / items.Count;
            float total = 0F;
            bool isFirst = true;
            Data current = items[0];
            while (items.Count > 0 && _threadRunning)
            {
                Thread.Sleep(5);
                now = now.AddMilliseconds(5 * _accelerator);
                if (isFirst)
                {
                    isFirst = false;
                    simulationStartTime = current.Time;
                }
                while (_threadRunning && (_accelerator == -1 || current.Time.Subtract(simulationStartTime) <= now.Subtract(realStartTime)))
                {
                    items.Remove(current);
                    GfxMessage msg = new GfxMessage(GfxType.RealtimeData, current);
                    msg.Time = current.Time;
                    Invoke(new MessageEventHandler(SendOutMessage), new object[] { null, msg, null });
                    total += weightItem;
                    Invoke(new ApplyTextToPlayButtonHandler(ApplyTextToPlayButton), new object[] { Math.Floor(total) + "%" });
                    if (items.Count == 0)
                        break;
                    current = items[0];
                }
            }
            Invoke(new StopHandler(Stop), null);
        }

        private void mnuPlay_StopClick(object sender, EventArgs e)
        {
            if (!_threadRunning)
                Start();
            else
                Stop();
        }

        private void cboSpeed_TextUpdate(object sender, EventArgs e)
        {
            if (cboSpeed.Text == "Instant")
                _accelerator = -1;
            else
            {
                if (!int.TryParse(cboSpeed.Text.Remove(0, 1), out _accelerator))
                    MessageBox.Show("This acceleration value is not recognized.");
            }
        }

        private void ApplyTextToPlayButton(string text)
        {
            mnuPlay.Text = text;
        }
        #endregion

        #region MWF Base
        public MWFBase GetMWF(string title, Size size, Point location, FormWindowState windowState)
        {
            return new MWFExport(ID, title, size, location, windowState, this, _helper.InputModules);
        }
        #endregion

    }
}
