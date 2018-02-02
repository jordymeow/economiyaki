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
    [ToolboxBitmap(typeof(EmbeddedResourceFinder), "Finance.Framework.Core.Resources.ShowGridlinesHS.png")]
    public partial class MarketGrid : UserControl, IGfxModule
    {
        [Browsable(true), Description("ID of this Market Control."), Category("Myaki"), DefaultValue(null)]
        public Guid ID { get { return _ID; } set { _ID = value; } }

        [Browsable(true), Description("Show menubar."), Category("Myaki"), DefaultValue(true)]
        public bool ShowMenuBar { get { return toolMenu.Visible; } set { toolMenu.Visible = value; dataGridView.Location = toolMenu.Visible ? new Point(0, 0) : new Point(0, 25); } }

        [Browsable(false), Description("DataGridView."), Category("Myaki"), DefaultValue(true)]
        public DataGridView DataGridView { get { return dataGridView; } }

        public event UnlinkedEventHandler UnlinkedEvent;
        public event MessageEventHandler MessageEvent;
        public event MouseEventHandler MoveRequest;

        private Guid _ID;
        private readonly IDictionary<string, DataGridViewRow> _guids = new Dictionary<string, DataGridViewRow>();
        private readonly MarketCtrlHelper _helper;

        #region Instance & Dispose
        public MarketGrid()
        {
            InitializeComponent();
            ID = Guid.NewGuid();
            _helper = new MarketCtrlHelper(this, dataGridView, toolMenu);
            toolOut.MouseDown += _helper.StartDrag;
            _helper.MessageEvent += NewMessageEvent;
            Disposed += MarketRealtimeGrid_Disposed;
        }

        public MarketGrid(MWFBase mwf)
            : this()
        {
            ID = mwf.ID;
            MWFGrid mwfData = mwf as MWFGrid;
            if (mwfData == null)
                throw new NotSupportedException();
        }

        void MarketRealtimeGrid_Disposed(object sender, EventArgs e)
        {
            if (UnlinkedEvent != null)
                UnlinkedEvent.Invoke(this, this);
        }
        #endregion

        #region User Events
        private void toolStrip_MouseDown(object sender, MouseEventArgs e)
        {
            if (MoveRequest != null)
                MoveRequest.Invoke(sender, e);
        }

        private void toolClear_Click(object sender, EventArgs e)
        {
            while (dataGridView.Columns.Count > 1)
                dataGridView.Columns.RemoveAt(1);
            dataGridView.Rows.Clear();
            _guids.Clear();
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
        public string MyakiName { get { return "Grid"; } }

        [Browsable(false)]
        public Size MyakiSize { get { return new Size(250, 150); } }

        [Browsable(false)]
        public GraphicBorderStyle MyakiBorderStyle { get { return GraphicBorderStyle.Sizable; } }

        [Browsable(false)]
        public IList<Guid> InputModules { get { return _helper.InputModules; } }

        public void AddInputModule(IGfxModule module)
        {
            _helper.InputModuleLink(module);
        }
        #endregion

        #region Message Handlers
        private void ManageDataItem(Data item)
        {
            DataGridViewRow row;
            if (!_guids.ContainsKey(item.Security))
            {
                int index = dataGridView.Rows.Add();
                row = dataGridView.Rows[index];
                row.Cells[0].Value = item.Security;
                row.ContextMenuStrip = contextMenu;
                _guids.Add(item.Security, row);
            }
            else
                row = _guids[item.Security];
            foreach (DataFieldItem field in item)
            {
                if (!dataGridView.Columns.Contains(field.Field))
                    dataGridView.Columns.Add(field.Field, field.Field);
                double precValue = 0, currentValue = 0;
                if (row.Cells[field.Field].Value is decimal && field.Value is decimal)
                {
                    precValue = (double)row.Cells[field.Field].Value;
                    currentValue = (double)field.Value;
                }
                precValue = Math.Round(precValue, 2);
                currentValue = Math.Round(currentValue, 2);
                if (precValue > currentValue)
                    row.Cells[field.Field].Style.ForeColor = Color.Red;
                else if (precValue < currentValue)
                    row.Cells[field.Field].Style.ForeColor = Color.Blue;
                if (currentValue != 0)
                    row.Cells[field.Field].Value = currentValue;
                else if (row.Cells[field.Field].Value != field.Value)
                    row.Cells[field.Field].Value = field.Value;
            }
        }

        private void ManageHistoryData(HistoryData data)
        {
            try
            {
                dataGridView.Columns.Clear();
                dataGridView.Rows.Clear();
                bool isFirst = true;
                foreach (HistoryDataItem item in data)
                {
                    if (isFirst)
                    {
                        dataGridView.Columns.Add("Date", "Date");
                        foreach (DataFieldItem current in item)
                            dataGridView.Columns.Add(current.Field, current.Field);
                        isFirst = false;
                    }
                    int c = 0;
                    object[] newRow = new object[item.Count + 1];

                    foreach (DataGridViewColumn col in dataGridView.Columns)
                    {
                        if (col.HeaderText == "Date")
                            newRow[c++] = item.Time.ToShortDateString();
                        else
                            newRow[c++] = item[col.HeaderText];
                    }
                    dataGridView.Rows.Add(newRow);
                }
            }
            catch (Exception)
            {
                dataGridView.BackgroundColor = Color.Red;
            }
        }

        private void ManageMessage(GfxMessage msg)
        {
            if (msg.Content is MultiData)
            {
                MultiData multiData = (MultiData)msg.Content;
                foreach (Data item in multiData)
                    ManageDataItem(item);
            }
            else if (msg.Content is Data)
            {
                Data data = (Data)msg.Content;
                ManageDataItem(data);
            }
            else if (msg.Content is HistoryData)
            {
                HistoryData data = (HistoryData)msg.Content;
                ManageHistoryData(data);
            }
            else if (msg.Content is MultiHistoryData)
            {
                MessageBox.Show("Grid can't manage MultiHistoryData.");
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
                ManageMessage(msg);
            else
                foreach (GfxMessage current in lst)
                    ManageMessage(current);
            if (MessageEvent != null)
                MessageEvent.Invoke(source, msg, lst);
        }
        #endregion

        private void SetAlert_Click(object sender, EventArgs e)
        {
            //TODO: Alert creation
        }

        #region MWF Base
        public MWFBase GetMWF(string title, Size size, Point location, FormWindowState windowState)
        {
            return new MWFGrid(ID, title, size, location, windowState, this, _helper.InputModules);
        }
        #endregion
    }
}
