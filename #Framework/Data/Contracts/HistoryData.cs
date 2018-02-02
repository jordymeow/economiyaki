using System;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.Serialization;

namespace Finance.Framework.Types
{
    [DataContract]
    public class HistoryData : IEnumerable<HistoryDataItem>
    {
        [DataMember]
        public Guid Guid;

        [DataMember]
        public string Security;

        [DataMember]
        public DateTime Time;

        [DataMember]
        private Dictionary<DateTime, HistoryDataItem> _dates;

        public HistoryData(Guid guid, string equity)
        {
            Guid = guid;
            _dates = new Dictionary<DateTime, HistoryDataItem>();
            Time = DateTime.Now;
            Security = equity;
        }

        public HistoryData(Guid guid, string equity, DateTime time)
            : this(guid, equity)
        {
            Time = time;
        }

        public int Count
        {
            get { return _dates.Count; }
        }

        public void Add(HistoryDataItem item)
        {
            _dates.Add(item.Time, item);
        }

        public IEnumerator<HistoryDataItem> GetEnumerator()
        {
            return _dates.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _dates.Values.GetEnumerator();
        }

        public override string ToString()
        {
            return Time.ToShortDateString() + " " + Time.ToShortTimeString() + " - " + _dates.Count + " results.";
        }

        static public bool operator != (HistoryData data, string type)
        {
            return type != "HistoryData";
        }

        static public bool operator ==(HistoryData data, string type)
        {
            return type == "HistoryData";
        }

        public bool Equals(HistoryData obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.Guid.Equals(Guid) && Equals(obj.Security, Security) && obj.Time.Equals(Time) && Equals(obj._dates, _dates);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == typeof (HistoryData) && Equals((HistoryData) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = Guid.GetHashCode();
                result = (result*397) ^ (Security != null ? Security.GetHashCode() : 0);
                result = (result*397) ^ Time.GetHashCode();
                result = (result*397) ^ (_dates != null ? _dates.GetHashCode() : 0);
                return result;
            }
        }

        public bool Contains(DateTime time)
        {
            return _dates.ContainsKey(time);
        }

        public void Remove(DateTime time)
        {
            _dates.Remove(time);
        }

        /// <summary>
        /// Gets the <see cref="Finance.Framework.Types.HistoryDataItem"/> with the specified time.
        /// </summary>
        /// <value></value>
        public HistoryDataItem this[DateTime time]
        {
            get
            { return _dates.ContainsKey(time) ? _dates[time] : null; }
        }

        /// <summary>
        /// Gets or sets the value with the specified field name.
        /// </summary>
        /// <value></value>
        public HistoryDataItem this[string time]
        {
            get
            {
                DateTime translatedTime;
                if (!DateTime.TryParse(time, out translatedTime))
                    throw new Exception("Can't convert " + time + " to a system date.");
                return _dates.ContainsKey(translatedTime) ? _dates[translatedTime] : null;
            }
        }
    }
}
