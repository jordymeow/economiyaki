using System;
using System.Collections.Generic;
using Finance.Framework.Types;

namespace Finance.Framework.DataAccess.Bloomberg
{
    internal class BloombergRequest
    {
        public Guid Guid;
        public string Equity;
        public RequestType Type;
        public Dictionary<string, object> Fields = new Dictionary<string, object>();

        public BloombergRequest(Guid guid, string equity, RequestType type)
        {
            Guid = guid;
            Type = type;
            Equity = equity;
        }
    }
}
