using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Finance.Framework.Core.Graphics;
using Finance.MarketAccess;
using Finance.Framework.Core.Serialization;
using Finance.Framework.Types;
using System.ComponentModel;

namespace Finance.Framework.Core
{
    [ToolboxBitmap(typeof(EmbeddedResourceFinder), "Finance.Framework.Core.Resources.disconnect.png")]
    public partial class MarketData : UserControl, IGfxModule
    {
        [Browsable(true), Description("ID of this Market Control."), Category("Myaki"), DefaultValue(null)]
        public Guid ID { get { return _ID; } set { _ID = value; } }

        [Browsable(true), Description("Interval"), Category("Myaki"), DefaultValue(1000)]
        public int Interval;

        [Browsable(true), Description("Show menubar."), Category("Myaki"), DefaultValue(true)]
        public bool ShowMenuBar { get { return toolMenu.Visible; } set { toolMenu.Visible = value; pnlDataItem.Location = toolMenu.Visible ? new Point(0, 0) : new Point(0, 25); } }

        public event UnlinkedEventHandler UnlinkedEvent;
        public Stack<MarketDataItem> DataItems { get { return _dataItems; } }
        public bool IsStarted { get { return mnuStart.Checked; } }
        public RequestType RequestType;

        private Guid _ID;
        private Guid _mergingGuid = Guid.NewGuid();
        private GfxMessage _temporizedMessage;
        private readonly Size _MarketCtrlSize = new Size(228, 80);
        private readonly IList<GfxMessage> _lstMessages = new List<GfxMessage>();
        private readonly Stack<MarketDataItem> _dataItems = new Stack<MarketDataItem>();
        private readonly IDictionary<Guid, HistoryData> _histMessagesToMerge = new Dictionary<Guid, HistoryData>();
        private readonly IDictionary<Guid, Data> _messagesToMerge = new Dictionary<Guid, Data>();

        #region Instance & Dispose
        private void BasicInitialization()
        {
            InitializeComponent();
            string[] sa = typeof(EmbeddedResourceFinder).Assembly.GetManifestResourceNames();
            RequestType = RequestType.Realtime;
            AddMarketDataItem();
            Disposed += MarketDataFlux_Disposed;
        }

        public MarketData()
        {
            ID = Guid.NewGuid();
            Interval = 1000;
            BasicInitialization();
        }

        public MarketData(MWFBase mwf)
        {
            ID = mwf.ID;
            InitializeComponent();
            MWFData mwfData = mwf as MWFData;
            if (mwfData == null)
                throw new NotSupportedException();
            foreach (DataItemConfig currentConf in mwfData.ItemsConfig)
                AddMarketDataItem(currentConf); 
            Interval = mwfData.Interval;
            RequestType = mwfData.RequestType;
            switch (RequestType)
            {
                case RequestType.History:
                    ModeMenuItem_Click(historyToolStripMenuItem, null);
                    break;
                case RequestType.Realtime:
                    ModeMenuItem_Click(realtimeToolStripMenuItem, null);
                    break;
                case RequestType.Static:
                    ModeMenuItem_Click(staticToolStripMenuItem, null);
                    break;
            }
            Disposed += MarketDataFlux_Disposed;
            if (mwfData.IsStarted)
                Start();
        }

        void MarketDataFlux_Disposed(object sender, EventArgs e)
        {
            if (UnlinkedEvent != null)
                UnlinkedEvent.Invoke(this, this);
            _lstMessages.Clear();
            if (_dataItems != null)
                while (_dataItems.Count > 0)
                {
                    MarketDataItem item = _dataItems.Pop();
                    item.Dispose();
                }
        }
        #endregion

        #region IGfxModule
        public void AddMessages(IList<GfxMessage> messages)
        {
            foreach (GfxMessage current in messages)
                AddMessage(current);
        }

        public string MyakiName
        {
            get
            {
                string res = "";
                foreach (MarketDataItem item in _dataItems)
                    res += item.Settings.Equity + " | ";
                return res.Length > 3 ? res.Remove(res.Length - 3, 3) : "Data";
            }
        }

        public Size MyakiSize
        {
            get { return _MarketCtrlSize; }
        }

        public GraphicBorderStyle MyakiBorderStyle
        {
            get { return GraphicBorderStyle.Sizable; }
        }

        private event MessageEventHandler _MessageEvent;
        public event MessageEventHandler MessageEvent
        {
            add
            {
                _MessageEvent += value;
                ProcessNewMessages();
            }
            remove
            {
                _MessageEvent -= value;
            }
        }

        public IList<Guid> InputModules
        {
            get { return null; }
        }

        public void AddInputModule(IGfxModule module)
        {
            throw new Exception("It's impossible to add modules as input on the Market Data control.");
        }
        #endregion

        #region Drag & Drop
        private void toolOut_MouseDown(object sender, MouseEventArgs e)
        {
            DataObject data = new DataObject(typeof(IGfxModule).FullName, this);
            toolOut.DoDragDrop(data, DragDropEffects.Link);
        }
        #endregion

        #region Messages Management
        private void ProcessNewMessages()
        {
            if (_lstMessages.Count > 0)
            {
                _MessageEvent.Invoke(this, null, _lstMessages);
                _lstMessages.Clear();
            }
            toolOutputMessage.Visible = false;
        }

        /// <summary>
        /// Pushes the message, and manages the merging.
        /// </summary>
        /// <param name="message">The message.</param>
        public GfxMessage PushMessage(GfxMessage message)
        {
            lock (_messagesToMerge)
            {
                Data data = (Data)message.Content;
                if (_messagesToMerge.ContainsKey(data.Guid))
                    _messagesToMerge[data.Guid] = data;
                else
                    _messagesToMerge.Add(data.Guid, data);
                if (_messagesToMerge.Count == _dataItems.Count)
                {
                    // Merges and return the final message
                    MultiData mergedData = new MultiData(_mergingGuid, data.Time);
                    foreach (Data currentData in _messagesToMerge.Values)
                    {
                        if (mergedData.Contains(currentData.Security))
                        {
                            IEnumerator<DataFieldItem> e = currentData.GetEnumerator();
                            while (e.MoveNext())
                                if (!mergedData[currentData.Security].Contains(e.Current.Field))
                                    mergedData[currentData.Security].Add(e.Current.Field, e.Current.Value);

                        }
                        else
                            mergedData.Add(currentData.Security, currentData);
                    }
                    _messagesToMerge.Clear();
                    GfxMessage newMessage;

                    if (mergedData.Count == 1)
                    {
                        IEnumerator<Data> e = mergedData.GetEnumerator();
                        e.MoveNext();
                        newMessage = new GfxMessage(message.MessageType, e.Current);
                    }
                    else switch (message.MessageType)
                        {
                            case GfxType.RealtimeData:
                                newMessage = new GfxMessage(GfxType.RealtimeDataMulti, mergedData);
                                break;
                            case GfxType.StaticData:
                                newMessage = new GfxMessage(GfxType.StaticDataMulti, mergedData);
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                    return newMessage;
                }
                return null;
            }
        }

        /// <summary>
        /// Pushes the message, and manages the merging (for historical data).
        /// </summary>
        /// <param name="message">The message.</param>
        public GfxMessage PushHistoryMessage(GfxMessage message)
        {
            lock (_histMessagesToMerge)
            {
                HistoryData data = (HistoryData)message.Content;
                if (_histMessagesToMerge.ContainsKey(data.Guid))
                    _histMessagesToMerge[data.Guid] = data;
                else
                    _histMessagesToMerge.Add(data.Guid, data);
                if (_histMessagesToMerge.Count == _dataItems.Count)
                {
                    try
                    {
                        // SELECT THE DATES FOR WHICH THERE IS DATA FOR EVERY STOCK
                        List<DateTime> lstDates = new List<DateTime>();
                        bool isFirst = true;
                        foreach (HistoryData currentData in _histMessagesToMerge.Values)
                        {
                            if (isFirst)
                            {
                                foreach (HistoryDataItem item in currentData)
                                    lstDates.Add(item.Time);
                                isFirst = false;
                            }
                            else
                            {
                                Stack<DateTime> datesToRemove = new Stack<DateTime>();
                                foreach (DateTime currentDate in lstDates)
                                    if (!currentData.Contains(currentDate))
                                        datesToRemove.Push(currentDate);
                                while (datesToRemove.Count > 0)
                                    lstDates.Remove(datesToRemove.Pop());
                            }
                        }
                        // TRIM THE STOCKS
                        MultiHistoryData multiHisto = new MultiHistoryData(_mergingGuid, message.Time);
                        foreach (HistoryData currentData in _histMessagesToMerge.Values)
                        {
                            Stack<DateTime> datesToRemove = new Stack<DateTime>();
                            foreach (HistoryDataItem currentItem in currentData)
                                if (!lstDates.Contains(currentItem.Time))
                                    datesToRemove.Push(currentItem.Time);
                            while (datesToRemove.Count > 0)
                                currentData.Remove(datesToRemove.Pop());
                            multiHisto.Add(currentData.Security, currentData);
                        }
                        _histMessagesToMerge.Clear();
                        Stop();
                        return new GfxMessage(GfxType.HistoryDataMulti, multiHisto);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Application.ProductName", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    finally
                    {
                        _histMessagesToMerge.Clear();
                    }

                }
                else if (_histMessagesToMerge.Count > _dataItems.Count)
                {
                    _histMessagesToMerge.Clear();
                    throw new NotSupportedException("Should not happen...");
                }
                return null;
            }
        }

        /// <summary>
        /// Adds the message, manage MERGING or TEMPORIZED or DIRECT, and try to send it.
        /// </summary>
        /// <param name="message">The message.</param>
        public void AddMessage(GfxMessage message)
        {
            GfxMessage messageToSend;

            // MERGING MODE (RECV MSGS -> MERGE -> SEND)
            if (_dataItems.Count > 1)
            {
                if (message.MessageType == GfxType.StaticData || message.MessageType == GfxType.RealtimeData)
                    messageToSend = PushMessage(message);
                else if (message.MessageType == GfxType.HistoryData && _dataItems.Count > 1)
                    messageToSend = PushHistoryMessage(message);
                else
                    throw new NotImplementedException(message.MessageType + " isn't supported yet.");
            }
            // NORMAL MODE (RECV MSG -> SEND)
            else
                messageToSend = message;

            // TEMPORIZED MODE (RECV MSGS -> MERGE, MERGE, MERGE / NEVER SEND)
            if (RequestType == RequestType.Realtime && mnuTimer.Checked)
            {
                _temporizedMessage = messageToSend;
                return;
            }
            TrySendMessage(messageToSend);
        }

        /// <summary>
        /// Tries to send the message, or backup for a delayed send.
        /// </summary>
        /// <param name="message">The message.</param>
        private void TrySendMessage(GfxMessage message)
        {
            if (message == null) return;
            if (message.MessageType == GfxType.HistoryData) Stop();
            if (_MessageEvent != null)
            {
                // Send the message
                _MessageEvent.Invoke(this, message, null);
            }
            else
            {
                // Backup the messages to send them later
                if (message.MessageType != GfxType.Information)
                {
                    _lstMessages.Add(message);
                    toolOutputMessage.Text = _lstMessages.Count.ToString();
                    toolOutputMessage.ToolTipText = String.Format("Type: {0} / Message: {1}", _lstMessages[0].ContentType, _lstMessages[0].MessageType);
                    if (!toolOutputMessage.Visible)
                        toolOutputMessage.Visible = true;
                }
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (_temporizedMessage == null)
                return;
            _temporizedMessage = new GfxMessage(_temporizedMessage.MessageType, _temporizedMessage.Content);
            TrySendMessage(_temporizedMessage);
        }
        #endregion

        #region User events
        public event MouseEventHandler MoveRequest;

        private void mnuAdd_Click(object sender, EventArgs e)
        {
            AddMarketDataItem();
        }

        private void AddMarketDataItemPrivate(MarketDataItem item)
        {
            pnlDataItem.Controls.Add(item);
            _dataItems.Push(item);
            mnuRemove.Visible = true;
        }

        public MarketDataItem AddMarketDataItem(DataItemConfig config)
        {
            MarketDataItem item = new MarketDataItem(this, config);
            AddMarketDataItemPrivate(item);
            return item;
        }

        public MarketDataItem AddMarketDataItem()
        {
            MarketDataItem item = new MarketDataItem(this);
            AddMarketDataItemPrivate(item);
            return item;
        }

        private void mnuRemove_Click(object sender, EventArgs e)
        {
            MarketDataItem item = _dataItems.Pop();
            pnlDataItem.Controls.Remove(item);
            item.Dispose();
            if (_dataItems.Count == 1)
                mnuRemove.Visible = false;
        }

        private void toolStrip_MouseDown(object sender, MouseEventArgs e)
        {
            if (MoveRequest != null)
                MoveRequest.Invoke(sender, e);
        }

        private void Stop()
        {
            if (timer.Enabled)
                timer.Stop();
            foreach (MarketDataItem item in _dataItems)
                item.Stop();
            mnuMode.Enabled = true;
            mnuTimer.Enabled = true;
            mnuStart.Checked = false;
            mnuAdd.Visible = true;
            mnuRemove.Visible = true;
            _messagesToMerge.Clear();
            _mergingGuid = Guid.Empty;
        }

        private void Start()
        {
            _lstMessages.Clear();
            toolOutputMessage.Visible = false;
            foreach (MarketDataItem item in _dataItems)
                item.Start();
            if (RequestType == RequestType.Realtime && mnuTimer.Checked)
            {
                timer.Interval = Interval;
                timer.Start();
            }
            mnuMode.Enabled = false;
            mnuTimer.Enabled = false;
            mnuStart.Checked = true;
            mnuAdd.Visible = false;
            mnuRemove.Visible = false;
        }

        private void mnuStart_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler(mnuStart_Click), new object[] { sender, e });
                return;
            }
            if (mnuStart.Checked)
                Stop();
            else
                Start();
        }

        private void ModeMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item == null)
                return;
            if (item.Checked == false) return;
            switch (item.Text)
            {
                case "Realtime":
                    RequestType = RequestType.Realtime;
                    break;
                case "History":
                    RequestType = RequestType.History;
                    break;
                case "Static":
                    RequestType = RequestType.Static;
                    break;
            }
        }

        private void ModeMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item == null)
                return;
            switch (item.Text)
            {
                case "Realtime":
                    mnuTimer.Visible = true;
                    realtimeToolStripMenuItem.Checked = true;
                    historyToolStripMenuItem.Checked = false;
                    staticToolStripMenuItem.Checked = false;
                    mnuMode.Text = "R";
                    break;
                case "History":
                    mnuTimer.Visible = false;
                    realtimeToolStripMenuItem.Checked = false;
                    historyToolStripMenuItem.Checked = true;
                    staticToolStripMenuItem.Checked = false;
                    mnuMode.Text = "H";
                    break;
                case "Static":
                    mnuTimer.Visible = false;
                    realtimeToolStripMenuItem.Checked = false;
                    historyToolStripMenuItem.Checked = false;
                    staticToolStripMenuItem.Checked = true;
                    mnuMode.Text = "S";
                    break;
            }
        }

        private void toolOutputMessage_Click(object sender, EventArgs e)
        {
            FrmMessagesDetails frmDetails = new FrmMessagesDetails(_lstMessages);
            frmDetails.ShowDialog();
            if (_lstMessages.Count == 0)
                toolOutputMessage.Visible = false;
            else
                toolOutputMessage.Text = _lstMessages.Count.ToString();
        }

        private void mnuTimer_Click(object sender, EventArgs e)
        {
            if (mnuTimer.Checked)
                mnuTimer.Checked = false;
            else
            {
                FrmSetTimer frmTimer = new FrmSetTimer(Interval);
                if (frmTimer.ShowDialog() == DialogResult.OK)
                {
                    Interval = frmTimer.Interval;
                    mnuTimer.Checked = true;
                }
            }
        }
        #endregion

        #region MWF Data
        public MWFBase GetMWF(string title, Size size, Point location, FormWindowState windowState)
        {
            return new MWFData(ID, title, size, location, windowState, this, null);
        }
        #endregion

    }
}
