using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Finance.Framework.Types
{
    [DataContract]
    public class MultiHistoryData : IEnumerable<HistoryData>
    {
        [DataMember]
        public Guid Guid;

        [DataMember]
        public DateTime Time;

        [DataMember]
        private IDictionary<string, HistoryData> _stocks = new Dictionary<string, HistoryData>();

        public MultiHistoryData(Guid guid)
        {
            Guid = guid;
        }

        public MultiHistoryData(Guid guid, DateTime time)
            : this(guid)
        {
            Time = time;
        }

        public int Count
        {
            get { return _stocks.Count; }
        }

        public void Add(string equity, HistoryData data)
        {
            _stocks.Add(equity, data);
        }

        public IEnumerator<HistoryData> GetEnumerator()
        {
            return _stocks.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _stocks.Values.GetEnumerator();
        }

        public override string ToString()
        {
            return Time.ToShortDateString() + " " + Time.ToShortTimeString() + " - " + _stocks.Count + " stocks.";
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        static public bool operator !=(MultiHistoryData multiData, string type)
        {
            return type != "MultiHistoryData";
        }

        static public bool operator ==(MultiHistoryData multiData, string type)
        {
            return type == "MultiHistoryData";
        }

        public bool Contains(string equity)
        {
            return _stocks.ContainsKey(equity);
        }

        /// <summary>
        /// Gets or sets the value with the specified field name.
        /// </summary>
        /// <value></value>
        public HistoryData this[string equity]
        {
            get
            {
                return _stocks.ContainsKey(equity) ? _stocks[equity] : null;
            }
            set
            {
                if (_stocks.ContainsKey(equity))
                    _stocks[equity] = value;
                else
                    _stocks.Add(equity, value);
            }
        }
    }
}
