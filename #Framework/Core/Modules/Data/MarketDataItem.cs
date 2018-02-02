using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using Finance.MarketAccess;
using Finance.Framework.Types;
using Finance.Framework.Core.Properties;
using Finance.Framework.DataAccess.Bloomberg;
using Finance.Framework.DataAccess.Network;
using Finance.Framework.DataAccess.Reuters;

namespace Finance.Framework.Core
{
    public partial class MarketDataItem : UserControl, IDisposable
    {
        public DataItemConfig Settings { get { return _Settings; } }

        private Guid _Guid = Guid.Empty;
        private GenericAccess _MarketData;
        private readonly DataItemConfig _Settings;
        private readonly MarketData _MarketDataParent;
        private readonly FadingThread fadingStaticThread;
        private readonly FadingThread fadingMiscThread;

        #region Constructors & Dispose
        public MarketDataItem(MarketData parent, DataItemConfig config)
        {
            _MarketDataParent = parent;
            InitializeComponent();
            _Settings = config;
            txtSecurity.Text = config.Equity;
            fadingStaticThread = new FadingThread(picStatic, FadingThread.FadingType.Green);
            fadingMiscThread = new FadingThread(picMisc, FadingThread.FadingType.Purple);
            Disposed += MarketDataItem_Disposed;
        }

        public MarketDataItem(MarketData _parent)
            : this(_parent, new DataItemConfig())
        {          
        }

        void MarketDataItem_Disposed(object sender, EventArgs e)
        {
            lock (this)
            {
                if (_Guid == Guid.Empty) return;
                _MarketData.CancelRequest(_Guid);
                _Guid = Guid.Empty;
                _MarketData.Disconnect();
            }
        }

        void IDisposable.Dispose()
        {
            _MarketData.Disconnect();
        }
        #endregion

        #region Start / Stop
        public bool Start()
        {
            switch (_Settings.MarketAccessLib)
            {
                case AccessProvider.Bloomberg:
                    _MarketData = new BloombergAccess();
                    break;
                case AccessProvider.Reuters:
                    _MarketData = new ReutersAccess();
                    break;
                case AccessProvider.Network:
                    if (_Settings.MarketAccessParam == String.Empty)
                    {
                        MessageBox.Show("Param is empty! The 'Network' access requires a server host address.", "Application.ProductName", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    _MarketData = new NetworkAccess(_Settings.MarketAccessParam);
                    break;
            }
            _MarketData.HistoryMarketDataEvent += HistoryMarketDataEvent;
            _MarketData.MiscellaneousEvent += MiscellaneousEvent;
            _MarketData.RealtimeMarketDataEvent += RealtimeMarketDataEvent;
            _MarketData.StaticMarketDataEvent += StaticMarketDataEvent;
            txtSecurity.ReadOnly = true;
            if (!_MarketData.Connect(false) || !CreateRequest())
                return false;
            picConfig.Visible = false;
            return true;
        }

        public void Stop()
        {
            lock (this)
            {
                if (_Guid != Guid.Empty)
                {
                    _MarketData.CancelRequest(_Guid);
                    _Guid = Guid.Empty;
                    _MarketData.Disconnect();
                }
                txtSecurity.ReadOnly = false;
                picConfig.Visible = true;
            }
        }
        #endregion

        #region MarketData Handlers

        void StaticMarketDataEvent(object sender, Data data)
        {

            if (InvokeRequired)
            {
                Invoke(new GenericAccess.StaticMarketDataHandler(RealtimeMarketDataEvent), new object[] { sender, data });
                return;
            }
            lock (this)
            {
                ThreadPool.QueueUserWorkItem(fadingStaticThread.FadingThreadFunc, this);
                GfxMessage message = new GfxMessage(GfxType.StaticData, data);
                _MarketDataParent.AddMessage(message);
                _Guid = Guid.Empty;
            }
        }

        void RealtimeMarketDataEvent(object sender, Data data)
        {
            if (InvokeRequired)
            {
                Invoke(new GenericAccess.RealtimeMarketDataHandler(RealtimeMarketDataEvent), new object[] { sender, data });
                return;
            }
            lock (fadingStaticThread)
            {
                ThreadPool.QueueUserWorkItem(fadingStaticThread.FadingThreadFunc, this);
                GfxMessage message = new GfxMessage(GfxType.RealtimeData, data);
                _MarketDataParent.AddMessage(message);
            }
        }

        void HistoryMarketDataEvent(object sender, HistoryData data)
        {
            if (InvokeRequired)
            {
                Invoke(new GenericAccess.HistoryMarketDataHandler(HistoryMarketDataEvent), new object[] { sender, data });
                return;
            }
            lock (fadingStaticThread)
            {
                ThreadPool.QueueUserWorkItem(fadingStaticThread.FadingThreadFunc, this);
                GfxMessage message = new GfxMessage(GfxType.HistoryData, data);
                _MarketDataParent.AddMessage(message);
                _Guid = Guid.Empty;
            }
        }

        void MiscellaneousEvent(object sender, MiscData data)
        {
            if (InvokeRequired)
            {
                Invoke(new GenericAccess.MiscellaneousHandler(MiscellaneousEvent), new object[] { sender, data });
                return;
            }
            lock (fadingMiscThread)
            {
                ThreadPool.QueueUserWorkItem(fadingMiscThread.FadingThreadFunc, null);
                if (data.Type == MiscEventType.Error)
                    Stop();
                GfxMessage message = new GfxMessage(GfxType.Information, data);
                _MarketDataParent.AddMessage(message);
            }
        }
        #endregion

        #region User Events
        private void txtSecurity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                e.Handled = true;
        }

        private void picConfig_Click(object sender, EventArgs e)
        {
            FrmDataItemSettings config = new FrmDataItemSettings(_Settings, _MarketDataParent.RequestType);
            if (config.ShowDialog() == DialogResult.OK)
                txtSecurity.Text = Settings.Equity;
        }

        private void picConfig_MouseEnter(object sender, EventArgs e)
        {
            picConfig.Image = Resources.bullet_wrench_highlight;
        }

        private void picConfig_MouseLeave(object sender, EventArgs e)
        {
            picConfig.Image = Resources.bullet_wrench;
        }

        private void txtSecurity_TextChanged(object sender, EventArgs e)
        {
            _Settings.Equity = txtSecurity.Text;
        }
        #endregion

        #region MarketData Requesters
        private bool CreateRequest()
        {
            List<string> equity = new List<string>();
            equity.Add(_Settings.Equity);
            switch (_MarketDataParent.RequestType)
            {
                case RequestType.Static:
                case RequestType.Realtime:
                    {
                        List<Field> fields = new List<Field>();
                        List<string> userfield = new List<string>();
                        foreach (string field in _Settings.Fields)
                        {
                            if (Enum.IsDefined(typeof(Field), field))
                                fields.Add((Field)Enum.Parse(typeof(Field), field));
                            else
                                userfield.Add(field);
                        }
                        _Guid = _MarketDataParent.RequestType == RequestType.Realtime ? _MarketData.RequestRealtime(equity, fields, userfield) : _MarketData.RequestStatic(equity, fields, userfield);
                    }
                    break;
                case RequestType.History:
                    {
                        List<HistoryField> fields = new List<HistoryField>();
                        List<string> userfield = new List<string>();
                        foreach (string field in _Settings.Fields)
                        {
                            if (Enum.IsDefined(typeof(HistoryField), field))
                                fields.Add((HistoryField)Enum.Parse(typeof(HistoryField), field));
                            else
                                userfield.Add(field);
                        }
                        _Guid = _MarketData.RequestHistory(equity, fields, userfield, _Settings.DateFrom, _Settings.DateTo);
                    }
                    break;
                default:
                    new NotImplementedException();
                    break;
            }
            return true;
        }
        #endregion
    }
}
