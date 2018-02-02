using System;
using System.Windows.Forms;

namespace Finance.Framework.Core.Graphics.Excel
{
    public partial class FrmVarModify : Form
    {
        public string Cell;
        public string DataDefinition;

        public FrmVarModify(string cell, string dataDefinition)
        {
            InitializeComponent();
            Cell = cell;
            DataDefinition = dataDefinition;
            txtCell.Text = cell;
            txtDataDefinition.Text = dataDefinition;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Cell = txtCell.Text;
            DataDefinition = txtDataDefinition.Text;
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