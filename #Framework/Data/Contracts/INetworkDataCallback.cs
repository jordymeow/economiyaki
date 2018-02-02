using System.ServiceModel;

namespace Finance.Framework.Types
{
    public interface INetworkDataCallback
    {
        [OperationContract(IsOneWay=true)]
        void ReceiveMiscellaneousData(MiscData data);

        [OperationContract(IsOneWay = true)]
        void ReceiveStaticMarketData(Data data);

        [OperationContract(IsOneWay = true)]
        void ReceiveRealtimeMarketData(Data data);

        [OperationContract(IsOneWay = true)]
        void ReceiveHistoryMarketData(HistoryData data);
    }
}
