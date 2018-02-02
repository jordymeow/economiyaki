using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Finance.Framework.Core
{
    public partial class FrmAlertAdd : Form
    {
        public FrmAlertAdd()
        {
            
            InitializeComponent();
        }

        private void Frm_Load(object sender, EventArgs e)
        {
            cboComparison.SelectedIndex = 0;
        }
    }
}