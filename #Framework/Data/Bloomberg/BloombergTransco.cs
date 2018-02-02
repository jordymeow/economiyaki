using System;
using Finance.Framework.Types;

namespace Finance.Framework.DataAccess.Bloomberg
{
    public class BloombergTransco
    {
        #region History MarketField
        /// <summary>
        /// Transcoes to history market field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        static internal HistoryField? TranscoToHistoryMarketField(string field)
        {
            switch (field)
            {
                case "PX_LAST":
                    return HistoryField.F_Last;

                case "PX_OPEN":
                    return HistoryField.F_Open;

                case "PX_ASK":
                    return HistoryField.F_Ask;

                case "MRG_TOTAL_BUY":
                    return HistoryField.F_Buy;

                case "MRG_TOTAL_SELL":
                    return HistoryField.F_Sell;

                case "PX_HIGH":
                    return HistoryField.F_High;

                case "PX_LOW":
                    return HistoryField.F_Low;

                case "PX_VOLUME":
                    return HistoryField.F_Volume;

                default:
                    return null;
            }
        }

        /// <summary>
        /// Transcoes from history market field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        static internal string TranscoFromHistoryMarketField(HistoryField field)
        {
            switch (field)
            {
                case HistoryField.F_Last:
                    return "PX_LAST";

                case HistoryField.F_Open:
                    return "PX_OPEN";

                case HistoryField.F_Ask:
                    return "PX_ASK";

                case HistoryField.F_Buy:
                    return "MRG_TOTAL_BUY";

                case HistoryField.F_Sell:
                    return "MRG_TOTAL_SELL";

                case HistoryField.F_High:
                    return "PX_HIGH";

                case HistoryField.F_Low:
                    return "PX_LOW";

                case HistoryField.F_Volume:
                    return "PX_VOLUME";

                default:
                    throw new NotImplementedException("The field " + field.ToString() + " hasn't been implemented yet.");
            }
        }
        #endregion

        #region MarketField
        static internal string TranscoToBloombergDate(DateTime date)
        {
            string year = date.Year.ToString();
            string month = date.Month < 10 ? "0" + date.Month : date.Month.ToString();
            string day = date.Day < 10 ? "0" + date.Day : date.Day.ToString();
            return year + month + day;
        }

        /// <summary>
        /// Transcoes to market field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        static internal Field? TranscoToMarketField(string field)
        {
            switch (field)
            {
                case "LAST_PRICE":
                    return Field.F_Last;

                case "NAME":
                    return Field.F_Name;

                case "OPEN":
                    return Field.F_Open;

                case "CLOSE":
                    return Field.F_Close;

                case "ASK":
                    return Field.F_Ask;

                case "BID":
                    return Field.F_Bid;

                case "CRNCY":
                    return Field.F_Currency;

                case "HIGH":
                    return Field.F_High;

                case "LOW":
                    return Field.F_Low;

                case "VOLUME":
                    return Field.F_Volume;

                default:
                    return null;
            }
        }

        /// <summary>
        /// Transcoes from market field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        static internal string TranscoFromMarketField(Field field)
        {
            switch (field)
            {
                case Field.F_Last:
                    return "LAST_PRICE";

                case Field.F_Name:
                    return "NAME";

                case Field.F_Open:
                    return "OPEN";

                case Field.F_Close:
                    return "CLOSE";

                case Field.F_Ask:
                    return "ASK";

                case Field.F_Bid:
                    return "BID";

                case Field.F_Currency:
                    return "CRNCY";

                case Field.F_High:
                    return "HIGH";

                case Field.F_Low:
                    return "LOW";

                case Field.F_Volume:
                    return "VOLUME";

                default:
                    throw new NotImplementedException("The field " + field.ToString() + " hasn't been implemented yet.");
            }
        }
        #endregion
    }
}
