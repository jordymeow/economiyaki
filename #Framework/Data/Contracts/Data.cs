using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Runtime.Serialization;

namespace Finance.Framework.Types
{
    [DataContract]
    public class Data : IEnumerable<DataFieldItem>
    {
        [DataMember]
        public Guid Guid;

        [DataMember]
        public DateTime Time;

        [DataMember]
        public string Security;

        [DataMember]
        private Dictionary<Field, object> _fields;

        [DataMember]
        private Dictionary<string, object> _userfields;

        public int Count { get { return _fields.Count + _userfields.Count; } }

        public Data(Guid guid, string equity)
        {
            Guid = guid;
            Security = equity;
            _fields = new Dictionary<Field, object>();
            _userfields = new Dictionary<string, object>();
            Time = DateTime.Now;
        }

        public Data(Guid guid, string equity, DateTime time)
            : this(guid, equity)
        {
            Time = time;
        }

        /// <summary>
        /// Removes the specified field.
        /// </summary>
        /// <param name="field">The field.</param>
        public void Remove(string field)
        {
            if (Enum.IsDefined(typeof(Field), field))
            {
                Field hField = (Field)Enum.Parse(typeof(Field), field);
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
        public void Add(Field field, object value)
        {
            if (_fields.ContainsKey(field))
                _fields[field] = GenericAccess.ConvertToDecimal(value);
            else
                _fields.Add(field, GenericAccess.ConvertToDecimal(value));
        }

        /// <summary>
        /// Adds the specified user field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        public void Add(string field, object value)
        {
            if (_userfields.ContainsKey(field))
                _userfields[field] = GenericAccess.ConvertToDecimal(value);
            else
                _userfields.Add(field, GenericAccess.ConvertToDecimal(value));
        }

        public override string ToString()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(String.Format("{0} {1} -> ", Time.ToShortDateString(), Time.ToShortTimeString()));
            bool isFirst = true;
            foreach (Field field in _fields.Keys)
            {
                if (isFirst)
                    isFirst = false;
                else
                    strBuilder.Append(", ");
                strBuilder.Append(String.Format("{0} = {1}", field.ToString(), _fields[field].ToString()));
            }
            foreach (string field in _userfields.Keys)
            {
                if (isFirst)
                    isFirst = false;
                else
                    strBuilder.Append(", ");
                strBuilder.Append(String.Format("{0} = {1}", field, _userfields[field].ToString()));
            }
            strBuilder.Append('.');
            return strBuilder.ToString();
        }

        public IEnumerator<DataFieldItem> GetEnumerator()
        {
            Dictionary<Field, object>.Enumerator e = _fields.GetEnumerator();
            while (e.MoveNext())
                yield return new DataFieldItem(Time, e.Current.Key, e.Current.Value);
            Dictionary<string, object>.Enumerator e2 = _userfields.GetEnumerator();
            while (e2.MoveNext())
                yield return new DataFieldItem(Time, e2.Current.Key, e2.Current.Value);
        }

        /// <summary>
        /// Gets or sets the value with the specified field name.
        /// </summary>
        /// <value></value>
        public object this[Field field]
        {
            get
            {
                return _fields.ContainsKey(field) ? _fields[field] : null;
            }
            set
            {
                if (_fields.ContainsKey(field))
                    _fields[field] = GenericAccess.ConvertToDecimal(value);
                else
                    _fields.Add(field, GenericAccess.ConvertToDecimal(value));
            }
        }

        public bool Contains(string field)
        {
            return this[field] != null;
        }

        public bool Contains(Field field)
        {
            return this[field] != null;
        }

        /// <summary>
        /// Gets or sets the value with the specified field name.
        /// </summary>
        /// <value></value>
        public object this[string field]
        {
            get
            {
                if (_userfields.ContainsKey(field))
                    return _userfields[field];
                if (Enum.IsDefined(typeof(Field), field))
                {
                    Field fld = (Field)Enum.Parse(typeof(Field), field);
                    return this[fld];
                }
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

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        static public bool operator !=(Data data, string type)
        {
            if (type == "Data")
                return false;
            return true;
        }

        static public bool operator ==(Data data, string type)
        {
            if (type == "Data")
                return true;
            return false;
        }
    }
}
