using System;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.Serialization;
using System.Text;

namespace Finance.Framework.Types
{
    [DataContract]
    public class HistoryDataItem : IEnumerable<DataFieldItem>
    {
        [DataMember]
        public Guid Guid;

        [DataMember]
        public DateTime Time;

        [DataMember]
        private Dictionary<HistoryField, object> _fields;

        [DataMember]
        private Dictionary<string, object> _userfields;

        public int Count { get { return _fields.Count + _userfields.Count; } }

        public HistoryDataItem(Guid id, DateTime time)
        {
            Guid = id;
            _fields = new Dictionary<HistoryField, object>();
            _userfields = new Dictionary<string, object>();
            Time = time;
        }

        /// <summary>
        /// Removes the specified field.
        /// </summary>
        /// <param name="field">The field.</param>
        public void Remove(string field)
        {
            if (Enum.IsDefined(typeof(HistoryField), field))
            {
                HistoryField hField = (HistoryField)Enum.Parse(typeof(HistoryField), field);
                if (_fields.ContainsKey(hField))
                    _fields.Remove(hField);
            }
            else if (_userfields.ContainsKey(field))
                _userfields.Remove(field);
        }

        /// <summary>
        /// Adds the specified field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        public void Add(HistoryField field, object value)
        {
            _fields.Add(field, GenericAccess.ConvertToDecimal(value));
        }

        /// <summary>
        /// Adds the specified user field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        public void Add(string field, object value)
        {
            _userfields.Add(field, GenericAccess.ConvertToDecimal(value));
        }

        public override string ToString()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(String.Format("{0} {1} -> ", Time.ToShortDateString(), Time.ToShortTimeString()));
            bool isFirst = true;
            foreach (HistoryField field in _fields.Keys)
            {
                if (isFirst)
                    isFirst = false;
                else
                    strBuilder.Append(", ");
                strBuilder.Append(String.Format("{0} = {1}", field, _fields[field]));
            }
            foreach (string field in _userfields.Keys)
            {
                if (isFirst)
                    isFirst = false;
                else
                    strBuilder.Append(", ");
                strBuilder.Append(String.Format("{0} = {1}", field, _userfields[field]));
            }
            strBuilder.Append('.');
            return strBuilder.ToString();
        }

        public IEnumerator<DataFieldItem> GetEnumerator()
        {
            Dictionary<HistoryField, object>.Enumerator e = _fields.GetEnumerator();
            while (e.MoveNext())
                yield return new DataFieldItem(Time, e.Current.Key.ToString(), e.Current.Value);
            Dictionary<string, object>.Enumerator e2 = _userfields.GetEnumerator();
            while (e2.MoveNext())
                yield return new DataFieldItem(Time, e2.Current.Key, e2.Current.Value);
        }

        /// <summary>
        /// Gets the value with the specified field name.
        /// </summary>
        /// <value></value>
        public object this[string field]
        {
            get
            {
                if (Enum.IsDefined(typeof(HistoryField), field))
                {
                    HistoryField hField = (HistoryField)Enum.Parse(typeof(HistoryField), field);
                    if (_fields.ContainsKey(hField))
                        return _fields[hField];
                }
                else if (_userfields.ContainsKey(field))
                    return _userfields[field];
                return null;
            }
            set
            {
                if (_userfields.ContainsKey(field))
                    _userfields[field] = GenericAccess.ConvertToDecimal(value);
                else
                    _userfields.Add(field, GenericAccess.ConvertToDecimal(value));
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
