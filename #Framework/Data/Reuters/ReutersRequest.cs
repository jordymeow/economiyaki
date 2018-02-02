using System;
using System.Collections.Generic;
using SSLRecord;
using SSLTimeSeries;
using Finance.Framework.Types;

namespace Finance.Framework.DataAccess.Reuters
{
    //internal enum RequestType
    //{
    //    Normal,
    //    IndexComposition
    //}

    internal class Request
    {
        public Guid Guid;
        public string Equity;
        public List<string> Fields = new List<string>();
        public RequestType Type;
        public RecordClass Record;
        public TimeSeries Series;
        public object Data;

        public Request(string equity, RequestType type, RecordClass record)
            : this(Guid.NewGuid(), equity, type, record) { }

        public Request(string equity, TimeSeries series)
            : this(Guid.NewGuid(), equity, series) { }

        public Request(Guid guid, string equity, RequestType type, RecordClass record)
        {
            if (type == RequestType.History)
                throw new Exception("You're not supposed to create a History request with a RecordClass!");
            Guid = guid;
            Type = type;
            Equity = equity;
            Record = record;
        }

        public Request(Guid guid, string equity, TimeSeries series)
        {
            Guid = guid;
            Type = RequestType.History;
            Equity = equity;
            Series = series;
        }
    }
}
