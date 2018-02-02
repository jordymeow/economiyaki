using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Finance.MarketAccess;
using Finance.Framework.Types;

namespace Finance.Framework.Core
{
    public partial class FrmDataItemSettings : Form
    {
        private readonly DataItemConfig _config;

        public FrmDataItemSettings(DataItemConfig config, RequestType requestType)
        {
            InitializeComponent();
            _config = config;
            cmbField.Items.AddRange(GenericAccess.Fields);
            cmbField.SelectedIndex = 0;
            cmbHistoryField.Items.AddRange(GenericAccess.HistoryFields);
            cmbHistoryField.SelectedIndex = 0;
            cmbMarketDataLib.Items.AddRange(Enum.GetNames(typeof(AccessProvider)));
            cmbMarketDataLib.SelectedIndex = 0;
            dateTimeTo.MaxDate = DateTime.Today;
            dateTimeFrom.MaxDate = DateTime.Today.Subtract(new TimeSpan(1, 0, 0, 0));
            foreach (string field in config.Fields)
                lstFields.Items.Add(field);
            grpHistory.Enabled = requestType == RequestType.History;
            cmbMarketDataLib.SelectedItem = _config.MarketAccessLib.ToString();
            txtParam.Text = _config.MarketAccessParam;
            dateTimeFrom.Value = config.DateFrom;
            dateTimeTo.Value = config.DateTo;
            txtEquity.Text = _config.Equity;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            _config.MarketAccessLib = (AccessProvider)Enum.Parse(typeof(AccessProvider), (string)cmbMarketDataLib.SelectedItem);
            _config.MarketAccessParam = txtParam.Text;
            _config.DateFrom = dateTimeFrom.Value;
            _config.DateTo = dateTimeTo.Value;
            _config.Equity = txtEquity.Text;

            // Fields
            List<string> lstStr = new List<string>();
            foreach (ListViewItem item in lstFields.Items)
                lstStr.Add(item.Text);
            _config.Fields = lstStr.ToArray();

            DialogResult = DialogResult.OK;
            Close();
        }

        private void cmdAddHistoryField_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem viewItem in lstFields.Items)
                if (viewItem.Text == cmbHistoryField.Text)
                    return;
            ListViewItem item = new ListViewItem(cmbHistoryField.Text);
            if (!Enum.IsDefined(typeof(Field), cmbHistoryField.Text))
                item.BackColor = Color.LightSteelBlue;
            lstFields.Items.Add(item);
        }

        private void btnAddField_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem viewItem in lstFields.Items)
                if (viewItem.Text == cmbField.Text)
                    return;
            ListViewItem item = new ListViewItem(cmbField.Text);
            if (!Enum.IsDefined(typeof(HistoryField), cmbField.Text))
                item.BackColor = Color.Khaki;
            lstFields.Items.Add(item);
        }

        private void lstFields_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            lstFields.Items.Remove(lstFields.SelectedItems[0]);
        }
    }
}