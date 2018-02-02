using System.Runtime.Serialization;

namespace Finance.Framework.Types
{
    [DataContract]
    public enum Field
    {
        [EnumMember]
        F_Name,
        [EnumMember]
        F_Last,
        [EnumMember]
        F_High,
        [EnumMember]
        F_Low,
        [EnumMember]
        F_Currency,
        [EnumMember]
        F_Open,
        [EnumMember]
        F_Close,
        [EnumMember]
        F_Bid,
        [EnumMember]
        F_Ask,
        [EnumMember]
        F_Volume,
        [EnumMember]
        F_PctChange
    }
}
