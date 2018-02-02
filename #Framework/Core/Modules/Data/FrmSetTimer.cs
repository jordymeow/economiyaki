using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Finance.Framework.Core
{
    public partial class FrmSetTimer : Form
    {
        public int Interval;

        public FrmSetTimer(int interval)
        {
            InitializeComponent();
            txtInterval.Text = interval.ToString();
        }

        private void txtInterval_Validating(object sender, CancelEventArgs e)
        {
            if (int.TryParse(txtInterval.Text, out Interval) && Interval >= 100) return;
            e.Cancel = false;
            MessageBox.Show("This value needs to be a number >= 100 (ms).", "Application.ProductName", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}