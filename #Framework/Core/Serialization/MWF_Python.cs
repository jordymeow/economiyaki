using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace Finance.Framework.Core.Serialization
{
    [Serializable]
    public class MWFPython : MWFBase
    {
        public bool IsStarted;
        public string [] Code;
        public SerializableDictionary<string, object> Constants;

        public override Type ModuleType
        {
            get { return typeof(MarketPython); }
        }

        public MWFPython()
            : base(Guid.Empty, "", new Size(), new Point(), FormWindowState.Normal, new List<Guid>())
        {
        }

        public MWFPython(Guid ID, string title, Size size, Point location, FormWindowState windowState, MarketPython marketPython, ICollection<Guid> modules)
            : base(ID, title, size, location, windowState, modules)
        {
            Code = marketPython.Code;
            IsStarted = marketPython.IsStarted;
            Constants = marketPython.Constants;
        }

        public MWFPython(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            foreach (SerializationEntry o in info)
            {
                switch (o.Name)
                {
                    case "Code":
                        Code = (string[])info.GetValue("Code", typeof(string[]));
                        break;
                    case "IsStarted":
                        IsStarted = info.GetBoolean("IsStarted");
                        break;
                    case "Constants":
                        Constants = (SerializableDictionary<string, object>)info.GetValue("Constants", typeof(SerializableDictionary<string, object>));
                        break;
                }
            }
            if (Constants == null)
                Constants = new SerializableDictionary<string, object>();
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", Code);
            info.AddValue("IsStarted", IsStarted);
            info.AddValue("Constants", Constants);
            base.GetObjectData(info, context);
        }
    }
}