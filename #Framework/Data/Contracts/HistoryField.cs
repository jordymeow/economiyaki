using System.Runtime.Serialization;

namespace Finance.Framework.Types
{
    [DataContract]
    public enum HistoryField
    {
        [EnumMember]
        F_Last,
        [EnumMember]
        F_High,
        [EnumMember]
        F_Low,
        [EnumMember]
        F_Open,
        [EnumMember]
        F_Ask,
        [EnumMember]
        F_Buy,
        [EnumMember]
        F_Sell,
        [EnumMember]
        F_Volume
    }
}
