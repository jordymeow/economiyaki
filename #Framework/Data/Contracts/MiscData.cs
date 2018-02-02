using System.Runtime.Serialization;

namespace Finance.Framework.Types
{
    [DataContract]
    public class MiscData
    {
        [DataMember]
        public MiscEventType Type;

        [DataMember]
        public string Message;

        [DataMember]
        public object Data;

        public MiscData(MiscEventType type, string message, object data)
        {
            Type = type;
            Message = message;
            Data = data;
        }
    }
}
