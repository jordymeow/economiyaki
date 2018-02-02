using System;
using System.Windows.Forms;

namespace Finance.Framework.Core.Graphics.Python
{
    public partial class FrmVarModify : Form
    {
        public string NewValue;

        public FrmVarModify(string name, string value)
        {
            InitializeComponent();
            Text += " '" + name + "'";
            txtNewValue.Text = value;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            NewValue = txtNewValue.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}