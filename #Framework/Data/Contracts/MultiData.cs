using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Collections;

namespace Finance.Framework.Types
{
    public class MultiData : IEnumerable<Data>
    {
        [DataMember]
        public Guid Guid;

        [DataMember]
        public DateTime Time;

        [DataMember]
        private IDictionary<string, Data> _stocks = new Dictionary<string,Data>();

        public MultiData(Guid guid)
        {
            Guid = guid;
        }

        public MultiData(Guid guid, DateTime time)
            : this(guid)
        {
            Time = time;
        }

        public int Count
        {
            get { return _stocks.Count; }
        }

        public void Add(string equity, Data data)
        {
            _stocks.Add(equity, data);
        }

        public IEnumerator<Data> GetEnumerator()
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

        static public bool operator !=(MultiData multiData, string type)
        {
            return type != "MultiData";
        }

        static public bool operator ==(MultiData multiData, string type)
        {
            return type == "MultiData";
        }

        public bool Contains(string equity)
        {
            return _stocks.ContainsKey(equity);
        }

        /// <summary>
        /// Gets or sets the value with the specified field name.
        /// </summary>
        /// <value></value>
        public Data this[string equity]
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
