using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace Finance.Framework.Core.Serialization
{
    [Serializable]
    public class MWFGraph : MWFBase
    {

        public SerializableDictionary<string, int> Colors = new SerializableDictionary<string, int>();
        public bool PerformanceMode = false;

        public override Type ModuleType
        {
            get { return typeof(MarketGraph); }
        }

        public MWFGraph()
            : base(Guid.Empty, "", new Size(), new Point(), FormWindowState.Normal, new List<Guid>())
        {
        }

        public MWFGraph(Guid ID, string title, Size size, Point location, FormWindowState windowState, MarketGraph marketGraph, ICollection<Guid> modules)
            : base(ID, title, size, location, windowState, modules)
        {
            Dictionary<string, Color>.Enumerator e = marketGraph.Colors.GetEnumerator();
            while (e.MoveNext())
                Colors.Add(e.Current.Key, e.Current.Value.ToArgb());
            PerformanceMode = marketGraph.PerformanceMode;
        }

        public MWFGraph(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            foreach (SerializationEntry o in info)
            {
                switch (o.Name)
                {
                    case "Colors":
                        Colors = (SerializableDictionary<string, int>)info.GetValue("Colors", typeof(SerializableDictionary<string, int>));
                        break;
                    case "PerformanceMode":
                        PerformanceMode = info.GetBoolean("PerformanceMode");
                        break;
                }
            }
            if (Colors == null)
                Colors = new SerializableDictionary<string, int>();
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Colors", Colors);
            info.AddValue("PerformanceMode", PerformanceMode);
            base.GetObjectData(info, context);
        }
    }
}
