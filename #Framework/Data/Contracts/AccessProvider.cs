using System.Runtime.Serialization;

namespace Finance.Framework.Types
{
    [DataContract]
    public enum AccessProvider
    {
        [EnumMember]
        Reuters,
        [EnumMember]
        Bloomberg,
        [EnumMember]
        Network
    }
}
