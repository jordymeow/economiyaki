using System;
using System.Windows.Forms;

namespace Finance.Framework.Core.Graphics.Python
{
    public partial class FrmConstants : Form
    {
        private readonly PythonExecuter _executer;

        public FrmConstants(PythonExecuter executer)
        {
            InitializeComponent();
            _executer = executer;
            foreach (string name in executer.Constants.Keys)
            {
                ListViewItem item = new ListViewItem(new string[] { name, executer.Constants[name].ToString() });
                lstVars.Items.Add(item);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            double val;
            bool success = double.TryParse(txtValue.Text, out val) ? _executer.ConstantAdd(txtName.Text, val) : _executer.ConstantAdd(txtName.Text, txtValue.Text);
            if (!success) return;
            ListViewItem item = new ListViewItem(new string[] { txtName.Text, txtValue.Text });
            lstVars.Items.Add(item);
            txtValue.Text = "";
            txtName.Text = "";
        }

        private void mnuRemove_Click(object sender, EventArgs e)
        {
            if (lstVars.SelectedItems.Count < 1)
                return;
            ListViewItem item = lstVars.SelectedItems[0];
            _executer.ConstantRemove(item.Text);
            lstVars.Items.Remove(item);
        }

        private void mnuModify_Click(object sender, EventArgs e)
        {
            if (lstVars.SelectedItems.Count < 1)
                return;
            ListViewItem item = lstVars.SelectedItems[0];
            FrmVarModify frmModify = new FrmVarModify(item.Text, item.SubItems[1].Text);
            if (frmModify.ShowDialog() != DialogResult.OK) return;
            double val;
            bool success = double.TryParse(frmModify.NewValue, out val) ? _executer.ConstantModify(item.Text, val) : _executer.ConstantModify(item.Text, frmModify.NewValue);
            if (!success) return;
            item.SubItems[1].Text = frmModify.NewValue;
        }
    }
}