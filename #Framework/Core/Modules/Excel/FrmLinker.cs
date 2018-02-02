using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Finance.Framework.Core.Graphics.Excel
{
    public partial class FrmLinker : Form
    {
        Dictionary<string, string> _definitions;

        public FrmLinker(Dictionary<string, string> definitions)
        {
            InitializeComponent();
            _definitions = definitions;
            foreach (string name in definitions.Keys)
            {
                ListViewItem item = new ListViewItem(new string[] { definitions[name].ToString(), name });
                lstLinks.Items.Add(item);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ListViewItem item = new ListViewItem(new string[] { txtCell.Text, txtDataDefinition.Text });
            _definitions.Add(txtDataDefinition.Text, txtCell.Text);
            lstLinks.Items.Add(item);
            txtCell.Text = "";
            txtDataDefinition.Text = "";
        }

        private void mnuRemove_Click(object sender, EventArgs e)
        {
            if (lstLinks.SelectedItems.Count < 1)
                return;
            ListViewItem item = lstLinks.SelectedItems[0];
            lstLinks.Items.Remove(item);
        }

        private void mnuModify_Click(object sender, EventArgs e)
        {
            if (lstLinks.SelectedItems.Count < 1)
                return;
            ListViewItem item = lstLinks.SelectedItems[0];
            FrmVarModify frmModify = new FrmVarModify(item.Text, item.SubItems[1].Text);
            if (frmModify.ShowDialog() != DialogResult.OK) return;
            if (_definitions.ContainsKey(frmModify.DataDefinition))
                _definitions[frmModify.DataDefinition] = frmModify.Cell;
            else
            {
                _definitions.Add(frmModify.DataDefinition, frmModify.Cell);
                _definitions.Remove(item.SubItems[1].Text);
            }
            item.SubItems[0].Text = frmModify.Cell;
            item.SubItems[1].Text = frmModify.DataDefinition;
        }

        private void lstLinks_DoubleClick(object sender, EventArgs e)
        {
            mnuModify_Click(sender, e);
        }
    }
}