using System.Windows.Forms;
using System;
using Finance.Framework;
using Finance.Framework.Core;
using Finance.Framework.Core.Excel;

namespace Finance.Jumbler
{
    public partial class FrmToolbox : Form
    {
        public FrmToolbox()
        {
            InitializeComponent();
        }

        private void picData_MouseDown(object sender, MouseEventArgs e)
        {
            DoDragDrop(typeof(MarketData), DragDropEffects.Copy);
        }

        private void picLogger_MouseDown(object sender, MouseEventArgs e)
        {
            DoDragDrop(typeof(MarketLogger), DragDropEffects.Copy);
        }

        private void picGrid_MouseDown(object sender, MouseEventArgs e)
        {
            DoDragDrop(typeof(MarketGrid), DragDropEffects.Copy);
        }

        private void picPython_MouseDown(object sender, MouseEventArgs e)
        {
            DoDragDrop(typeof(MarketPython), DragDropEffects.Copy);
        }

        private void picSpreadsheet_MouseDown(object sender, MouseEventArgs e)
        {
            DoDragDrop(typeof(MarketSpreadsheet), DragDropEffects.Copy);
        }

        private void picExport_MouseDown(object sender, MouseEventArgs e)
        {
            DoDragDrop(typeof(MarketExport), DragDropEffects.Copy);
        }

        private void picGraph_MouseDown(object sender, MouseEventArgs e)
        {
            DoDragDrop(typeof(MarketGraph), DragDropEffects.Copy);
        }
    }
}