namespace Finance.MarketAccess
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.CodeDom.Compiler;
    using System.Diagnostics;
    using Finance.Framework.Types;

    [GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface NetworkDataServerChannel : INetworkDataServer, IClientChannel
    {
    }

    [DebuggerStepThroughAttribute]
    [GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public class NetworkDataServerClient : DuplexClientBase<INetworkDataServer>, INetworkDataServer
    {

        public NetworkDataServerClient(InstanceContext callbackInstance)
            :
                base(callbackInstance)
        {
        }

        public NetworkDataServerClient(InstanceContext callbackInstance, string endpointConfigurationName)
            :
                base(callbackInstance, endpointConfigurationName)
        {
        }

        public NetworkDataServerClient(InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress)
            :
                base(callbackInstance, endpointConfigurationName, remoteAddress)
        {
        }

        public NetworkDataServerClient(InstanceContext callbackInstance, string endpointConfigurationName, EndpointAddress remoteAddress)
            :
                base(callbackInstance, endpointConfigurationName, remoteAddress)
        {
        }

        public NetworkDataServerClient(InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, EndpointAddress remoteAddress)
            :
                base(callbackInstance, binding, remoteAddress)
        {
        }

        public bool Connect()
        {
            return Channel.Connect();
        }

        public void Disconnect()
        {
            Channel.Disconnect();
        }

        public Guid RequestStatic(IList<string> equities, IList<Field> fields, IList<string> userfields)
        {
            return Channel.RequestStatic(equities, fields, userfields);
        }

        public Guid RequestHistory(IList<string> equities, IList<HistoryField> fields, IList<string> userfields, DateTime from, DateTime to)
        {
            return Channel.RequestHistory(equities, fields, userfields, from, to);
        }

        public Guid RequestRealtime(IList<string> equities, IList<Field> fields, IList<string> userfields)
        {
            return Channel.RequestRealtime(equities, fields, userfields);
        }

        public bool CancelRequest(Guid guid)
        {
            return Channel.CancelRequest(guid);
        }

        public Guid GetIndexComposition(string equity)
        {
            return Channel.GetIndexComposition(equity);
        }
    }
}
