using System;
using System.Runtime.Serialization;

namespace Finance.Framework.Types
{
    [DataContract]
    public class DataFieldItem
    {
        [DataMember]
        private DateTime _Time;
	    public DateTime Time
	    {
		    get { return _Time;}
	    }

        [DataMember]
        private bool _IsEnumField;
        public bool IsEnumField
        {
            get { return _IsEnumField; }
        }

        [DataMember]
        private string _Field;
        public string Field
        {
            get { return _Field; }
        }

        [DataMember]
        private Field _EnumField;
        public Field EnumField
        {
            get { return _EnumField; }
        }

        [DataMember]
        private object _Value;
        public object Value
        {
            get { return _Value; }
        }

        public DataFieldItem(DateTime time, string field, object value)
        {
            _Time = time;
            _Field = field;
            _Value = value;
        }

        public DataFieldItem(DateTime time, Field field, object value)
        {
            _Time = time;
            _IsEnumField = true;
            // Added on 2009, June 2nd
            _Field = field.ToString();
            _EnumField = field;
            _Value = value;
        }
    }
}
