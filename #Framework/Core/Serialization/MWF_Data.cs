using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using System.Windows.Forms;
using Finance.MarketAccess;
using Finance.Framework.Types;

namespace Finance.Framework.Core.Serialization
{
    [Serializable]
    public class MWFData : MWFBase
    {
        public bool IsStarted;
        public int Interval;
        public RequestType RequestType;
        public List<DataItemConfig> ItemsConfig;

        public override Type ModuleType
        {
            get { return typeof(MarketData); }
        }

        public MWFData()
            : base(Guid.Empty, "", new Size(), new Point(), FormWindowState.Normal, new List<Guid>())
        {
        }

        public MWFData(Guid ID, string title, Size size, Point location, FormWindowState windowState, MarketData marketData, ICollection<Guid> inputModules)
            : base(ID, title, size, location, windowState, inputModules)
        {
            Stack<MarketDataItem>.Enumerator e = marketData.DataItems.GetEnumerator();
            ItemsConfig = new List<DataItemConfig>();
            while (e.MoveNext())
                ItemsConfig.Add(e.Current.Settings);
            ItemsConfig.Reverse();
            IsStarted = marketData.IsStarted;
            Interval = marketData.Interval;
            RequestType = marketData.RequestType;
        }

        public MWFData(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            foreach (SerializationEntry o in info)
            {
                switch (o.Name)
                {
                    case "ItemsConfig":
                        ItemsConfig = (List<DataItemConfig>)info.GetValue("ItemsConfig", typeof(List<DataItemConfig>));
                        break;

                    case "IsStarted":
                        IsStarted = info.GetBoolean("IsStarted");
                        break;

                    case "RequestType":
                        RequestType = (RequestType)info.GetValue("RequestType", typeof(RequestType));
                        break;

                    case "Interval":
                        Interval = info.GetInt32("Interval");
                        break;
                }
            }
            if (ItemsConfig == null)
                ItemsConfig = new List<DataItemConfig>();
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ItemsConfig", ItemsConfig);
            info.AddValue("IsStarted", IsStarted);
            info.AddValue("RequestType", RequestType);
            info.AddValue("Interval", RequestType);
            base.GetObjectData(info, context);
        }
    }
}
