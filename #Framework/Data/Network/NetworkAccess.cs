using System;
using System.Collections.Generic;
using System.ServiceModel;
using Finance.Framework.Types;

namespace Finance.Framework.DataAccess.Network
{
    /// <summary>
    /// Market data via NetworkNetworkAccess
    /// </summary>
    public class NetworkAccess : GenericAccess, INetworkDataCallback
    {
        private bool _Connected;
        public override bool Connected { get { return _Connected; } }

        readonly DuplexChannelFactory<INetworkDataServer> factory;
        INetworkDataServer server;

        public NetworkAccess(string serverHost)
        {
            factory = new DuplexChannelFactory<INetworkDataServer>(this, new NetTcpBinding(SecurityMode.None), new EndpointAddress("net.tcp://" + serverHost));
            server = factory.CreateChannel();
        }

        public override bool Connect(bool enableLogging)
        {
            try
            {
                if (server.Connect())
                {
                    _Connected = true;
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                InvokeMiscellaneousEvent(MiscEventType.Error, null, ex.Message);
                return false;
            }
        }

        public override void Disconnect()
        {
            try
            {
                if (server != null)
                    server.Disconnect();
            }
            finally
            {
                if (factory.State == CommunicationState.Opened)
                    factory.Close();
                server = null;
                _Connected = false;
            }
        }

        public override Guid RequestStatic(IList<string> equities, IList<Field> fields, IList<string> userfields)
        {
            return _Connected ? server.RequestStatic(equities, fields, userfields) : Guid.Empty;
        }

        public override Guid RequestHistory(IList<string> equities, IList<HistoryField> fields, IList<string> userfields, DateTime from, DateTime to)
        {
            return _Connected ? server.RequestHistory(equities, fields, userfields, from, to) : Guid.Empty;
        }

        public override Guid RequestRealtime(IList<string> equities, IList<Field> fields, IList<string> userfields)
        {
            return _Connected ? server.RequestRealtime(equities, fields, userfields) : Guid.Empty;
        }

        public override bool CancelRequest(Guid guid)
        {
            return _Connected && server.CancelRequest(guid);
        }

        public override Guid GetIndexComposition(string equity)
        {
            return _Connected ? server.GetIndexComposition(equity) : Guid.Empty;
        }

        public override void Dispose()
        {
            if (!_Connected) return;
            server.Disconnect();
            server = null;
        }

        public override Guid[] CurrentGuids
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #region NetworkDataServerCallback Members

        public void ReceiveMiscellaneousData(MiscData data)
        {
            InvokeMiscellaneousEvent(MiscEventType.Info, data, null);
        }

        public void ReceiveStaticMarketData(Data data)
        {
            InvokeStaticMarketDataEvent(data);
        }

        public void ReceiveRealtimeMarketData(Data data)
        {
            InvokeRealtimeMarketDataEvent(data);
        }

        public void ReceiveHistoryMarketData(HistoryData data)
        {
            InvokeHistoryMarketDataEvent(data);
        }

        #endregion

    }
}
