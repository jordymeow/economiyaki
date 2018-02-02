using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace Finance.Framework.Core.Serialization
{
    [Serializable]
    public class MWFGrid : MWFBase
    {
        public override Type ModuleType
        {
            get { return typeof(MarketGrid); }
        }

        public MWFGrid()
            : base(Guid.Empty, "", new Size(), new Point(), FormWindowState.Normal, new List<Guid>())
        {
        }

        public MWFGrid(Guid ID, string title, Size size, Point location, FormWindowState windowState, MarketGrid marketGrid, IList<Guid> modules)
            : base(ID, title, size, location, windowState, modules)
        {
        }

        public MWFGrid(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
