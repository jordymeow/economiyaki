using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace Finance.Framework.Core.Serialization
{
    [Serializable]
    public class MWFExport : MWFBase
    {
        public override Type ModuleType
        {
            get { return typeof(MarketExport); }
        }

        public MWFExport()
            : base(Guid.Empty, "", new Size(), new Point(), FormWindowState.Normal, new List<Guid>())
        {
        }

        public MWFExport(Guid ID, string title, Size size, Point location, FormWindowState windowState, MarketExport marketExport, ICollection<Guid> modules)
            : base(ID, title, size, location, windowState, modules)
        {
        }

        public MWFExport(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
