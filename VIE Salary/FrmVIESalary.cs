using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Finance.Framework.DataAccess.Reuters;
using Finance.Framework.Types;
using Finance.Framework.Core;

namespace VIE_Realtime_Salary
{
    public partial class FrmVIESalary : Form
    {
        public decimal Salary = 1915M;

        public FrmVIESalary()
        {
            InitializeComponent();
            marketGraphHistory.AddInputModule(marketGridHistory);
            GenericAccess reuters = new ReutersAccess();
            reuters.RealtimeMarketDataEvent += reuters_RealtimeMarketDataEvent;
            reuters.HistoryMarketDataEvent += reuters_HistoryMarketDataEvent;
            reuters.RequestHistory("EURJPY=", new string[] { "F_Ask", "F_Bid" }, new DateTime(2008, 1, 1),  DateTime.Today);
            reuters.RequestRealtime("EURJPY=", new string[] { "F_Ask", "F_Bid" });
        }

        void reuters_HistoryMarketDataEvent(object sender, HistoryData data)
        {
            foreach (HistoryDataItem item in data)
            {
                item["VIE"] = (decimal)item["F_Ask"] * Salary;
                item["Intern"] = 235000M;
                item.Remove("F_Ask");
            }
            marketGridHistory.AddMessage(new GfxMessage(GfxType.HistoryData, data));
            marketGridHistory.DataGridView.Columns[1].DefaultCellStyle.Format = "N";
        }

        void reuters_RealtimeMarketDataEvent(object sender, Data data)
        {
            data["VIE"] = (((decimal)data["F_Ask"] + (decimal)data["F_Bid"]) / 2) * Salary; ;
            data.Remove("F_Ask");
            data.Remove("F_Bid");
            marketGraphRealtime.AddMessage(new GfxMessage(GfxType.RealtimeData, data));
        }
    }
}