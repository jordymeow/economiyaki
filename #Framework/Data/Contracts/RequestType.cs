using System.Runtime.Serialization;

namespace Finance.Framework.Types
{
    [DataContract]
    public enum RequestType
    {
        Static,
        History,
        Realtime,
        IndexComposition
    }
}
