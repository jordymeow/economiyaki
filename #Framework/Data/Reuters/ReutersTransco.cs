using System;
using Finance.Framework.Types;

namespace Finance.Framework.DataAccess.Reuters
{
    public class ReutersTransco
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
                case "CLS":
                    return HistoryField.F_Last;

                case "OPN":
                    return HistoryField.F_Open;

                case "ASK":
                    return HistoryField.F_Ask;

                case "BUY":
                    return HistoryField.F_Buy;

                case "SELL":
                    return HistoryField.F_Sell;

                case "HI":
                    return HistoryField.F_High;

                case "LO":
                    return HistoryField.F_Low;

                case "VOL":
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
                    return "CLS";

                case HistoryField.F_Open:
                    return "OPN";

                case HistoryField.F_Ask:
                    return "ASK";

                case HistoryField.F_Buy:
                    return "BUY";

                case HistoryField.F_Sell:
                    return "SELL";

                case HistoryField.F_High:
                    return "HI";

                case HistoryField.F_Low:
                    return "LO";

                case HistoryField.F_Volume:
                    return "VOL";

                default:
                    throw new NotImplementedException("The field " + field.ToString() + " hasn't been implemented yet.");
            }
        }
        #endregion

        #region MarketField
        /// <summary>
        /// Transcoes to market field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        static internal Field? TranscoToMarketField(string field)
        {
            switch (field)
            {
                case "TRDPRC_1":
                    return Field.F_Last;

                case "DSPLY_NAME":
                    return Field.F_Name;

                case "OPEN_PRC":
                    return Field.F_Open;

                case "HST_CLOSE":
                    return Field.F_Close;

                case "ASK":
                    return Field.F_Ask;

                case "BID":
                    return Field.F_Bid;

                case "CURRENCY":
                    return Field.F_Currency;

                case "HIGH_1":
                    return Field.F_High;

                case "LOW_1":
                    return Field.F_Low;

                case "ACVOL_1":
                    return Field.F_Volume;

                case "PCTCHNG":
                    return Field.F_PctChange;

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
                    return "TRDPRC_1";

                case Field.F_Name:
                    return "DSPLY_NAME";

                case Field.F_Open:
                    return "OPEN_PRC";

                case Field.F_Close:
                    return "HST_CLOSE";

                case Field.F_Ask:
                    return "ASK";

                case Field.F_Bid:
                    return "BID";

                case Field.F_Currency:
                    return "CURRENCY";

                case Field.F_High:
                    return "HIGH_1";

                case Field.F_Low:
                    return "LOW_1";

                case Field.F_Volume:
                    return "ACVOL_1";

                case Field.F_PctChange:
                    return "PCTCHNG";

                default:
                    throw new NotImplementedException("The field " + field.ToString() + " hasn't been implemented yet.");
            }
        }
        #endregion
    }
}
