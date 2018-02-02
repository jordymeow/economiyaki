using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Finance.MarketAccess;
using Finance.Framework.Core.Serialization;
using Finance.Framework.Types;
using System.ComponentModel;

namespace Finance.Framework.Core
{
    [ToolboxBitmap(typeof(EmbeddedResourceFinder), "Finance.Framework.Core.Resources.application_xp_terminal.png")]
    public partial class MarketLogger : UserControl, IGfxModule
    {
        [Browsable(true), Description("ID of this Market Control."), Category("Myaki"), DefaultValue(null)]
        public Guid ID { get { return _ID; } set { _ID = value; } }

        [Browsable(true), Description("Show menubar."), Category("Myaki"), DefaultValue(true)]
        public bool ShowMenuBar { get { return toolMenu.Visible; } set { toolMenu.Visible = value; txtLog.Location = toolMenu.Visible ? new Point(0, 0) : new Point(0, 25); } }

        public event MouseEventHandler MoveRequest;
        public event UnlinkedEventHandler UnlinkedEvent;
        public event MessageEventHandler MessageEvent;

        private Guid _ID;
        private const int ResultSeparatorSize = 55;
        private readonly MarketCtrlHelper _helper;

        #region Instance & Dispose
        public MarketLogger()
        {
            InitializeComponent();
            ID = Guid.NewGuid();
            _helper = new MarketCtrlHelper(this, txtLog, toolMenu);
            _helper.MessageEvent += NewMessageEvent;
            toolOut.MouseDown += _helper.StartDrag;
            Disposed += MarketLogger_Disposed;
        }

        public MarketLogger(MWFBase mwf)
            : this()
        {
            ID = mwf.ID;
            MWFLogger mwfData = mwf as MWFLogger;
            if (mwfData == null)
                throw new NotSupportedException();
        }

        void MarketLogger_Disposed(object sender, EventArgs e)
        {
            if (UnlinkedEvent != null)
                UnlinkedEvent.Invoke(this, this);
        }
        #endregion

        #region User Events
        private void toolClear_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
        }

        private void toolStrip_MouseDown(object sender, MouseEventArgs e)
        {
            if (MoveRequest != null)
                MoveRequest.Invoke(sender, e);
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
        public string MyakiName { get { return "Logger"; } }

        [Browsable(false)]
        public Size MyakiSize { get { return new Size(300, 180); } }

        [Browsable(false)]
        public GraphicBorderStyle MyakiBorderStyle { get { return GraphicBorderStyle.Sizable; } }

        [Browsable(false)]
        public IList<Guid> InputModules { get { return _helper.InputModules; } }

        public void AddInputModule(IGfxModule module)
        {
            _helper.InputModuleLink(module);
        }
        #endregion

        #region Message handlers
        private void WriteHeader(GfxMessage data)
        {
            string[] types = data.ContentType.ToString().Split('.');
            string header = " " + types[types.Length - 1] + " " + data.Time.ToShortTimeString() + Environment.NewLine;
            header = header.PadLeft(ResultSeparatorSize - header.Length, '_');
            txtLog.SelectionColor = Color.Gray;
            txtLog.AppendText(header);
        }

        private void NewMessageEvent(object source, GfxMessage data)
        {
            WriteHeader(data);
            if (data.Content == null)
            {
                txtLog.SelectionColor = Color.Red;
                txtLog.AppendText(String.Format("The logger doesn't manage {0} message.{1}", data.MessageType, Environment.NewLine));
                return;
            }
            if (data.Content is Data)
                DataItemHandler(source, (Data)data.Content);
            else if (data.Content is MultiData)
            {
                MultiData current = (MultiData)data.Content;
                IEnumerator<Data> e = current.GetEnumerator();
                while (e.MoveNext())
                    DataItemHandler(source, e.Current);
            }
            else if (data.Content is MiscData)
                MiscDataHandler(source, (MiscData)data.Content);
            else if (data.Content is HistoryData)
                HistoryDataHandler(source, (HistoryData)data.Content);
            else
            {
                txtLog.SelectionColor = Color.Red;
                txtLog.AppendText(String.Format("The logger doesn't manage {0} message.{1}", data.MessageType, Environment.NewLine));
            }
        }

        void NewMessageEvent(object source, GfxMessage msg, IList<GfxMessage> lst)
        {
            if (InvokeRequired)
            {
                Invoke(new MessageEventHandler(NewMessageEvent), new object[] { source, msg, lst });
                return;
            }
            if (msg != null)
            {
                NewMessageEvent(source, msg);
                if (MessageEvent != null)
                    MessageEvent(source, msg, null);
            }
            else
            {
                foreach (GfxMessage current in lst)
                    NewMessageEvent(source, current);
                if (MessageEvent != null)
                    MessageEvent(source, null, lst);
            }
        }

        void HistoryDataHandler(object sender, HistoryData data)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new GenericAccess.HistoryMarketDataHandler(HistoryDataHandler), new object[] { sender, data });
                return;
            }
            lock (txtLog)
            {
                if (data == null) return;
                bool isFirstDate = true;

                // CELL SIZES
                int[] cellSize = null;
                int cellCounter = 0;
                foreach (HistoryDataItem currentData in data)
                {
                    if (isFirstDate)
                    {
                        cellSize = new int[currentData.Count];
                        foreach (DataFieldItem current in currentData)
                            cellSize[cellCounter++] = current.Value.ToString().Length;
                        isFirstDate = false;
                    }

                    cellCounter = 0;
                    foreach (DataFieldItem current in currentData)
                        if (cellSize[cellCounter++] < current.Value.ToString().Length)
                            cellSize[cellCounter] = current.Value.ToString().Length;
                }

                // DATA OUTPUT
                isFirstDate = true;
                foreach (HistoryDataItem currentData in data)
                {
                    if (isFirstDate)
                    {
                        txtLog.SelectionColor = Color.Yellow;
                        txtLog.AppendText("Date".PadLeft(10, ' ') + "\t");
                        cellCounter = 0;
                        foreach (DataFieldItem current in currentData)
                            // ReSharper disable PossibleNullReferenceException
                            txtLog.AppendText(current.Field.PadLeft(cellSize[cellCounter++], ' ') + "\t");
                        // ReSharper restore PossibleNullReferenceException
                        isFirstDate = false;
                        txtLog.AppendText(Environment.NewLine);
                    }
                    txtLog.SelectionColor = Color.White;
                    txtLog.AppendText(currentData.Time.ToShortDateString().PadLeft(10, ' ') + "\t");
                    cellCounter = 0;
                    foreach (DataFieldItem current in currentData)
                        // ReSharper disable PossibleNullReferenceException
                        txtLog.AppendText(current.Value.ToString().PadLeft(cellSize[cellCounter++], ' ') + "\t");
                    // ReSharper restore PossibleNullReferenceException
                    txtLog.AppendText(Environment.NewLine);
                }
                txtLog.SelectionColor = Color.Gray;
            }
        }

        private void DisplayMarketDataBlock(Data data)
        {
            lock (txtLog)
            {
                if (data.Count < 1) return;
                txtLog.SelectionColor = Color.Aqua;
                txtLog.AppendText(data.Security + ":" + Environment.NewLine);
                foreach (DataFieldItem current in data)
                {
                    txtLog.SelectionColor = Color.Yellow;
                    txtLog.AppendText(current.Field + " = ");
                    txtLog.SelectionColor = Color.White;
                    txtLog.AppendText(current.Value + Environment.NewLine);
                }

            }
        }

        void DataItemHandler(object sender, Data data)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new GenericAccess.RealtimeMarketDataHandler(DataItemHandler), new object[] { sender, data });
                return;
            }
            DisplayMarketDataBlock(data);
        }

        void MiscDataHandler(object sender, MiscData data)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new GenericAccess.MiscellaneousHandler(MiscDataHandler), new object[] { sender, data });
                return;
            }
            lock (txtLog)
            {
                txtLog.SelectionColor = data.Type == MiscEventType.Error ? Color.Red : Color.Green;
                txtLog.AppendText(DateTime.Now.ToShortTimeString() + ": " + data.Message + Environment.NewLine);
                txtLog.ScrollToCaret();
            }
        }
        #endregion

        #region MWF Base
        public MWFBase GetMWF(string title, Size size, Point location, FormWindowState windowState)
        {
            return new MWFLogger(ID, title, size, location, windowState, this, _helper.InputModules);
        }
        #endregion
    }
}
