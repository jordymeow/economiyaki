using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Finance.Framework.Types
{
    [ServiceContract(Name="NetworkDataServer", CallbackContract=typeof(INetworkDataCallback), SessionMode=SessionMode.Required)]
    public interface INetworkDataServer
    {
        [OperationContract(IsOneWay=false, IsInitiating=true)]
        bool Connect();

        [OperationContract(IsOneWay = true, IsTerminating=true)]
        void Disconnect();

        [OperationContract(IsOneWay = false)]
        Guid RequestStatic(IList<string> equities, IList<Field> fields, IList<string> userfields);

        [OperationContract(IsOneWay = false)]
        Guid RequestHistory(IList<string> equities, IList<HistoryField> fields, IList<string> userfields, DateTime from, DateTime to);

        [OperationContract(IsOneWay = false)]
        Guid RequestRealtime(IList<string> equities, IList<Field> fields, IList<string> userfields);

        [OperationContract(IsOneWay = false)]
        Guid GetIndexComposition(string equity);

        [OperationContract(IsOneWay = false)]
        bool CancelRequest(Guid guid);
    }
}
