using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Finance.Framework.Core
{
    [ToolboxBitmap(typeof(EmbeddedResourceFinder), "Finance.Framework.Core.Resources.layout_link.png")]
    public partial class MyakiPanel : Panel
    {
        [Browsable(false)]
        public IDictionary<Guid, IGfxModule> Modules = new Dictionary<Guid, IGfxModule>();

        [Browsable(true), Description("Modules ID"), Category("Myaki")]
        public IList<string> ModulesID = new List<string>();

        public MyakiPanel()
        {
            InitializeComponent();
            ControlAdded += MyakiPanel_ControlAdded;
            ControlRemoved += MyakiPanel_ControlRemoved;
        }

        void MyakiPanel_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (e.Control is IGfxModule)
            {
                Modules.Remove(((IGfxModule)e.Control).ID);
                ModulesID.Remove(((IGfxModule)e.Control).ID.ToString());
            }
        }

        void MyakiPanel_ControlAdded(object sender, ControlEventArgs e)
        {
            if (e.Control is IGfxModule)
            {
                Modules.Add(((IGfxModule)e.Control).ID, (IGfxModule)e.Control);
                ModulesID.Add(((IGfxModule)e.Control).ID.ToString());
            }
        }
    }
}

