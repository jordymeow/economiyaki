using System;
using Finance.MarketAccess;
using Finance.Framework.Types;

namespace Finance.Framework.Core
{
    [Serializable]
    public class DataItemConfig
    {
        public DateTime DateFrom = DateTime.Today.Subtract(new TimeSpan(365, 0, 0, 0));
        public DateTime DateTo = DateTime.Today;
        public string Equity = "EURJPY=";
        public string[] Fields = new string[] {"F_Bid", "F_Ask"};
        public AccessProvider MarketAccessLib = AccessProvider.Reuters;
        public string MarketAccessParam;
    }
}