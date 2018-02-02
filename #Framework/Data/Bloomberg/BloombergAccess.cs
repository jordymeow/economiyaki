using System;
using System.Collections.Generic;
using Bloomberglp.Blpapi;
using BGRequest = Bloomberglp.Blpapi.Request;
using Finance.Framework.Types;

namespace Finance.Framework.DataAccess.Bloomberg
{
    /// <summary>
    /// Market data via Bloomberg
    /// </summary>
    public class BloombergAccess : GenericAccess
    {
        private readonly int _serverPort = 8194;
        private readonly string _serverHost = "localhost";
        private const string _serviceHostReferenceData = "//blp/refdata";
        private const string _serviceHostRealtimeData = "//blp/mktdata";
        private Service _serviceReferenceData;
        private readonly SessionOptions _sessionOptions;
        private Session _session;
        private Session _sessionRealtime;
        private bool _Connected;
        private bool _loggingEnabled;

        public override bool Connected { get { return _Connected; } }
        public override Guid[] CurrentGuids { get { Guid[] arr = new Guid[_guids.Keys.Count]; _guids.Keys.CopyTo(arr, 0); return arr; } }
        readonly Dictionary<Guid, BloombergRequest> _guids = new Dictionary<Guid, BloombergRequest>();

        #region Connection / Instance
        public BloombergAccess()
        {
            _sessionOptions = new SessionOptions();
            _sessionOptions.ServerHost = _serverHost;
            _sessionOptions.ServerPort = _serverPort;
        }

        public BloombergAccess(string serverHost, int serverPort)
        {
            _serverHost = serverHost;
            _serverPort = serverPort;
            _sessionOptions = new SessionOptions();
            _sessionOptions.ServerHost = serverHost;
            _sessionOptions.ServerPort = serverPort;
        }

        public override void Dispose()
        {
            while (CurrentGuids.Length > 0)
                CancelRequest(CurrentGuids[0]);
        }

        public override bool Connect(bool _enableLogging)
        {
            try
            {
                _loggingEnabled = _enableLogging;
                _session = new Session(_sessionOptions, BloombergEvent_Handler);
                _session.Start();
                _session.OpenService(_serviceHostReferenceData);
                _sessionRealtime = new Session(_sessionOptions, BloombergEvent_Handler);
                _sessionRealtime.Start();
                _sessionRealtime.OpenService(_serviceHostRealtimeData);
                _serviceReferenceData = _session.GetService(_serviceHostReferenceData);
                _Connected = true;
                return true;
            }
            catch (Exception ex)
            {
                if (_loggingEnabled)
                    InvokeMiscellaneousEvent(MiscEventType.Error, null, ex.Message);
                return false;
            }
        }

        public override void Disconnect()
        {
            if (_session != null)
                _session.Stop();
            _Connected = false;
        }

        #endregion

        #region Messages Handlers
        public void BloombergEvent_Handler(Event ev, Session session)
        {
            if (ev.Type == Event.EventType.SUBSCRIPTION_STATUS)
            {
                IEnumerator<Message> lstMessages = ev.GetMessages().GetEnumerator();
                while (lstMessages.MoveNext())
                {
                    Message currMsg = lstMessages.Current;
                    if (!_guids.ContainsKey((Guid)currMsg.CorrelationID.Object))
                        return;
                    BloombergRequest request = _guids[(Guid)currMsg.CorrelationID.Object];
                    if (currMsg.MessageType.ToString() == "SubscriptionFailure")
                    {
                        Element reason = currMsg["reason"];
                        Element description = reason["description"];
                        InvokeMiscellaneousEvent(MiscEventType.Error, request.Guid, "Can't subscribe to " + request.Equity + ": " + description.GetValueAsString());
                        _guids.Remove(request.Guid);
                    }
                }
            }
            if (ev.Type == Event.EventType.RESPONSE || ev.Type == Event.EventType.SUBSCRIPTION_DATA)
            {
                IEnumerator<Message> lstMessages = ev.GetMessages().GetEnumerator();
                while (lstMessages.MoveNext())
                {
                    Message currMsg = lstMessages.Current;
                    if (!_guids.ContainsKey((Guid)currMsg.CorrelationID.Object))
                        return;
                    BloombergRequest request = _guids[(Guid)currMsg.CorrelationID.Object];
                    switch (request.Type)
                    {
                        case RequestType.Static:
                            {
                                // REALTIME OR STATIC REQUEST
                                if (currMsg.HasElement("responseError"))
                                    throw new NotImplementedException();
                                Element securityData = (Element)currMsg.GetElement("securityData")[0];
                                Element security = securityData["security"];
                                string securityName = security.GetValueAsString();
                                Element fields = securityData["fieldData"];
                                Data data = new Data((Guid)currMsg.CorrelationID.Object, securityName);
                                foreach (Element field in fields.Elements)
                                {
                                    string fieldName = field.Name.ToString();
                                    Field? fld = BloombergTransco.TranscoToMarketField(fieldName);
                                    if (fld != null)
                                        data.Add(fld.Value, field.GetValue());
                                    else
                                        data.Add(fieldName, field.GetValue());
                                }
                                InvokeStaticMarketDataEvent(data);
                                _guids.Remove(request.Guid);
                            }
                            break;
                        case RequestType.Realtime:
                            {
                                // REALTIME OR STATIC REQUEST
                                if (currMsg.HasElement("responseError"))
                                    throw new NotImplementedException();
                                DateTime actionTime = DateTime.Now;
                                if (currMsg.HasElement("TIME"))
                                    actionTime = currMsg.GetElementAsDate("TIME").ToSystemDateTime();
                                else if (currMsg.HasElement("EVENT_TIME"))
                                    actionTime = currMsg.GetElementAsDate("EVENT_TIME").ToSystemDateTime();
                                // Either the time or date at which the security last traded. All values are translated into the 
                                // user's date and time. If the last trade occurred today, it will show the time of the trade, 
                                // otherwise it will show the date on which the security last traded.
                                Data data = new Data(request.Guid, currMsg.TopicName, actionTime);
                                string[] lstFields = new string[request.Fields.Keys.Count];
                                request.Fields.Keys.CopyTo(lstFields, 0);
                                foreach (string field in lstFields)
                                {
                                    Field? fld = BloombergTransco.TranscoToMarketField(field);
                                    if (currMsg.HasElement(field))
                                        request.Fields[field] = currMsg.GetElement(field).GetValue();
                                    if (fld.HasValue && request.Fields[field] != null)
                                        data.Add(fld.Value, request.Fields[field]);
                                    else
                                        data.Add(field, request.Fields[field]);
                                }

                                #region ExtraFields
                                //// Last price for the security. Field updates in realtime.
                                //if (currMsg.HasElement("LAST_PRICE"))
                                //{
                                //    double lastPrice = currMsg.GetElementAsFloat64("LAST_PRICE");
                                //    data.Add(Field.Last, lastPrice);
                                //}

                                //// Current bid price in the market.
                                //if (currMsg.HasElement("BID"))
                                //{
                                //    double bidPrice = currMsg.GetElementAsFloat64("BID");
                                //    data.Add(Field.Bid, bidPrice);
                                //}

                                //// Current ask price in the market.
                                //if (currMsg.HasElement("ASK"))
                                //{
                                //    double askPrice = currMsg.GetElementAsFloat64("ASK");
                                //    data.Add(Field.Ask, askPrice);
                                //}

                                //// Current high price in the market.
                                //if (currMsg.HasElement("HIGH"))
                                //{
                                //    double highPrice = currMsg.GetElementAsFloat64("HIGH");
                                //    data.Add(Field.High, highPrice);
                                //}

                                //// Lowest price the security reached during the current trading day. If the market is closed then 
                                //// it is the lowest price the security reached on the last day the market was open. Field updates 
                                //// in realtime.
                                //if (currMsg.HasElement("LOW"))
                                //{
                                //    double low = currMsg.GetElementAsFloat64("LOW");
                                //    data.Add(Field.Low, low);
                                //}

                                //// The best bid price for a NASDAQ security or just the most recent bid price for any other 
                                //// security.
                                //if (currMsg.HasElement("BEST_BID"))
                                //{
                                //    double bestBid = currMsg.GetElementAsFloat64("BEST_BID");
                                //    data.Add("BEST_BID", bestBid);
                                //}

                                //// The best ask price for a NASDAQ security or just the most recent bid price for any other 
                                //// security.
                                //if (currMsg.HasElement("BEST_ASK"))
                                //{
                                //    double bestAsk = currMsg.GetElementAsFloat64("BEST_ASK");
                                //    data.Add("BEST_ASK", bestAsk);
                                //}

                                //// In cases where the price of a security is not necessarily representative of the last traded 
                                //// price, this field returns the actual last traded price. This applies to equities and is not 
                                //// exchange specific. It is available for the following Exchanges: London SE, Doha SE, Abu Dhabi 
                                //// SE,Dubai SE, Bahrain SE. Field updates in realtime.
                                //if (currMsg.HasElement("LAST_TRADE"))
                                //{
                                //    double lastTrade = currMsg.GetElementAsFloat64("LAST_TRADE");
                                //    data.Add("LAST_TRADE", lastTrade);
                                //}

                                //// The first price at which the security traded on the current day. If the market is closed then 
                                //// it is the first price at which the security traded on the last day the market was open. Field 
                                //// updates in realtime.
                                //if (currMsg.HasElement("OPEN"))
                                //{
                                //    double open = currMsg.GetElementAsFloat64("OPEN");
                                //    data.Add(Field.Open, open);
                                //}

                                //// The price of the last trade of the previous session. The 'previous' session is defined 
                                //// according to the MPDF setting for 'Use previous close prior to open' option.
                                //if (currMsg.HasElement("PREV_SES_LAST_PRICE"))
                                //{
                                //    double previousSessionLastPrice = currMsg.GetElementAsFloat64("PREV_SES_LAST_PRICE");
                                //    data.Add("PREV_SES_LAST_PRICE", previousSessionLastPrice);
                                //}

                                //// The current local trading date. This date is defined by the exchange or source of the market 
                                //// data for the current security. It is the date from the most recent or current trading session. 
                                //// This date does not take time zone defaults into consideration. Field updates in realtime.
                                //if (currMsg.HasElement("TRADING_DT_REALTIME"))
                                //{
                                //    DateTime tradingDate = currMsg.GetElementAsDatetime("TRADING_DT_REALTIME").ToSystemDateTime();
                                //    data.Add("TRADING_DT_REALTIME", tradingDate);
                                //}

                                //// Real Time Price Change 1 day net. This field does not adjust for cash dividends.
                                //if (currMsg.HasElement("RT_PX_CHG_NET_1D"))
                                //{
                                //    double priceChangeOneDayNet = currMsg.GetElementAsFloat64("RT_PX_CHG_NET_1D");
                                //    data.Add("RT_PX_CHG_NET_1D", priceChangeOneDayNet);
                                //}

                                //// Real Time Price Change 1 day percent. This field adjusts for cash dividends.
                                //if (currMsg.HasElement("RT_PX_CHG_PCT_1D"))
                                //{
                                //    double priceChangeOneDayPercent = currMsg.GetElementAsFloat64("RT_PX_CHG_PCT_1D");
                                //    data.Add("RT_PX_CHG_PCT_1D", priceChangeOneDayPercent);
                                //}

                                //// Indicated whether 'Bid Price' field contains 'regular' bid price (Y) or indicative bid price
                                //if (currMsg.HasElement("IND_BID_FLAG"))
                                //{
                                //    bool indicativeBidPrice = currMsg.GetElementAsBool("IND_BID_FLAG");
                                //    data.Add("IND_BID_FLAG", indicativeBidPrice);
                                //}

                                //// Indicated whether 'Ask Price' field contains 'regular' ask price (1) or indicative ask price
                                //if (currMsg.HasElement("IND_ASK_FLAG"))
                                //{
                                //    bool indicativeAskPrice = currMsg.GetElementAsBool("IND_ASK_FLAG");
                                //    data.Add("IND_ASK_FLAG", indicativeAskPrice);
                                //}

                                //// Today's open price. Field updates in realtime.
                                //if (currMsg.HasElement("OPEN_TDY"))
                                //{
                                //    double todayOpenPrice = currMsg.GetElementAsFloat64("OPEN_TDY");
                                //    data.Add("OPEN_TDY", todayOpenPrice);
                                //}

                                //// Today's last price. Field updates in realtime.
                                //if (currMsg.HasElement("LAST_PRICE_TDY"))
                                //{
                                //    double todayLastPrice = currMsg.GetElementAsFloat64("LAST_PRICE_TDY");
                                //    data.Add("LAST_PRICE_TDY", todayLastPrice);
                                //}

                                //// Today's bid price. Field updates in realtime.
                                //if (currMsg.HasElement("BID_TDY"))
                                //{
                                //    double todayBidPrice = currMsg.GetElementAsFloat64("BID_TDY");
                                //    data.Add("BID_TDY", todayBidPrice);
                                //}

                                //// Today's ask price. Field updates in realtime.
                                //if (currMsg.HasElement("ASK_TDY"))
                                //{
                                //    double todayAskPrice = currMsg.GetElementAsFloat64("ASK_TDY");
                                //    data.Add("ASK_TDY", todayAskPrice);
                                //}

                                //// Today's high price. Field updates in realtime.
                                //if (currMsg.HasElement("HIGH_TDY"))
                                //{
                                //    double todayHighPrice = currMsg.GetElementAsFloat64("HIGH_TDY");
                                //    data.Add("HIGH_TDY", todayHighPrice);
                                //}

                                //// Today's low price. Field updates in realtime.
                                //if (currMsg.HasElement("LOW_TDY"))
                                //{
                                //    double todayLowPrice = currMsg.GetElementAsFloat64("LOW_TDY");
                                //    data.Add("LOW_TDY", todayLowPrice);
                                //}

                                //// Real Time Pricing Source: The composite bid and ask prices for a security.
                                //if (currMsg.HasElement("RT_PRICING_SOURCE"))
                                //{
                                //    string realTimePricingSource = currMsg.GetElementAsString("RT_PRICING_SOURCE");
                                //    data.Add("RT_PRICING_SOURCE", realTimePricingSource);
                                //}
                                #endregion

                                // Indicates whether client is receiving a delayed or realtime tick stream from Bloomberg 
                                // through API.
                                //if (currMsg.HasElement("IS_DELAYED_STREAM"))
                                //{
                                //    bool isDelayedStream = currMsg.GetElementAsBool("IS_DELAYED_STREAM");
                                //    data.Add("IS_DELAYED_STREAM", isDelayedStream);
                                //}

                                InvokeRealtimeMarketDataEvent(data);
                            }
                            break;
                        case RequestType.History:
                            {
                                // REALTIME OR STATIC REQUEST
                                if (currMsg.HasElement("responseError"))
                                    throw new NotImplementedException();
                                DateTime actionTime = DateTime.Now;
                                if (currMsg.HasElement("TIME"))
                                    actionTime = currMsg.GetElementAsDate("TIME").ToSystemDateTime();
                                else if (currMsg.HasElement("EVENT_TIME"))
                                    actionTime = currMsg.GetElementAsDate("EVENT_TIME").ToSystemDateTime();
                                // Either the time or date at which the security last traded. All values are translated into the 
                                // user's date and time. If the last trade occurred today, it will show the time of the trade, 
                                // otherwise it will show the date on which the security last traded.
                                Element securityData = currMsg.GetElement("securityData");
                                Element security = securityData["security"];
                                Element fields = securityData["fieldData"];

                                HistoryData histData = new HistoryData(request.Guid, security.GetValueAsString(), actionTime);
                                for (int c = 0; c < fields.NumValues; c++)
                                {
                                    Element data = (Element)fields[c];
                                    DateTime date = data.GetElementAsDate("date").ToSystemDateTime();
                                    HistoryDataItem dataItem = new HistoryDataItem(request.Guid, date);
                                    for (int d = 0; d < data.NumElements; d++)
                                    {
                                        Element currElement = data.GetElement(d);
                                        if (currElement.Name == Name.FindName("date"))
                                            continue;
                                        HistoryField? fld = BloombergTransco.TranscoToHistoryMarketField(currElement.Name.ToString());
                                        if (fld.HasValue)
                                            dataItem.Add(fld.Value, currElement.GetValue());
                                        else
                                            dataItem.Add(currElement.Name.ToString(), currElement.GetValue());
                                
                                    }
                                    histData.Add(dataItem);
                                }
                                InvokeHistoryMarketDataEvent(histData);
                                _guids.Remove(request.Guid);
                            }
                            break;
                        default:
                            _guids.Remove(request.Guid);
                            throw new NotImplementedException(request.Type + " has not been implemented yet.");
                    }
                }
            }
        }
        #endregion

        #region Requests
        public override Guid RequestStatic(IList<string> equities, IList<Field> fields, IList<string> userfields)
        {
            List<string> lstFields = new List<string>();

            if (!Connected)
                Connect(false);
            BGRequest request = _serviceReferenceData.CreateRequest("ReferenceDataRequest");
            Element requestFields = request["fields"];

            Guid id = Guid.NewGuid();
            if (equities.Count > 1)
                InvokeMiscellaneousEvent(MiscEventType.Info, id, "Bloomberg API only supports one equity / request.");
            if (fields != null)
                foreach (Field field in fields)
                {
                    requestFields.AppendValue(BloombergTransco.TranscoFromMarketField(field));
                    lstFields.Add(BloombergTransco.TranscoFromMarketField(field));
                }
            if (userfields != null)
                foreach (string field in userfields)
                {
                    requestFields.AppendValue(field);
                    lstFields.Add(field);
                }
            request.Append("securities", equities[0]);

            BloombergRequest internalRequest = new BloombergRequest(id, equities[0], RequestType.Static);
            foreach (string field in lstFields)
                internalRequest.Fields.Add(field, null);
            _guids.Add(id, internalRequest);

            _session.SendRequest(request, new CorrelationID(id));
            return id;
        }

        public override Guid RequestHistory(IList<string> equities, IList<HistoryField> fields, IList<string> userfields, DateTime from, DateTime to)
        {
            List<string> lstFields = new List<string>();

            if (!Connected)
                Connect(false);
            BGRequest request = _serviceReferenceData.CreateRequest("HistoricalDataRequest");
            Element requestFields = request["fields"];

            Guid id = Guid.NewGuid();
            if (equities.Count > 1)
                InvokeMiscellaneousEvent(MiscEventType.Info, id, "Bloomberg API only supports one equity / request.");
            if (fields != null)
                foreach (HistoryField field in fields)
                {
                    requestFields.AppendValue(BloombergTransco.TranscoFromHistoryMarketField(field));
                    lstFields.Add(BloombergTransco.TranscoFromHistoryMarketField(field));
                }
            if (userfields != null)
                foreach (string field in userfields)
                {
                    requestFields.AppendValue(field);
                    lstFields.Add(field);
                }
            request.Append("securities", equities[0]);

            // HISTORY SPECIFICS
            request.Set("periodicityAdjustment", "ACTUAL");
            request.Set("periodicitySelection", "DAILY");
            request.Set("startDate", BloombergTransco.TranscoToBloombergDate(from));
            request.Set("endDate", BloombergTransco.TranscoToBloombergDate(to));
            //request.Set("maxDataPoints", 100);
            request.Set("returnEids", true);

            BloombergRequest internalRequest = new BloombergRequest(id, equities[0], RequestType.History);
            foreach (string field in lstFields)
                internalRequest.Fields.Add(field, null);
            _guids.Add(id, internalRequest);
            _session.SendRequest(request, new CorrelationID(id));
            return id;
        }

        public override Guid RequestRealtime(IList<string> equities, IList<Field> fields, IList<string> userfields)
        {
            IList<string> lstFields = new List<string>();
            Guid id = Guid.NewGuid();

            if (equities.Count > 1)
                InvokeMiscellaneousEvent(MiscEventType.Info, id, "Bloomberg API only supports one equity / request.");
            if (fields != null)
                foreach (Field field in fields)
                    lstFields.Add(BloombergTransco.TranscoFromMarketField(field));
            if (userfields != null)
                foreach (string field in userfields)
                    lstFields.Add(field);

            IList<string> options = new List<string>();
            //options.Add("interval = 0.1");
            Subscription sub = new Subscription(equities[0], lstFields, options, new CorrelationID(id));
            List<Subscription> lstSub = new List<Subscription>();
            lstSub.Add(sub);

            BloombergRequest request = new BloombergRequest(id, equities[0], RequestType.Realtime);
            foreach (string field in lstFields)
                request.Fields.Add(field, null);
            _guids.Add(id, request);
            _sessionRealtime.Subscribe(lstSub);
            return id;
        }

        public override bool CancelRequest(Guid guid)
        {
            if (_guids.ContainsKey(guid))
            {
                BloombergRequest request = _guids[guid];
                if (request.Type == RequestType.Realtime)
                {
                    _sessionRealtime.Cancel(new CorrelationID(guid));
                    _guids.Remove(guid);
                    return true;
                }
                if (request.Type == RequestType.Static)
                {
                    _session.Cancel(new CorrelationID(guid));
                    _guids.Remove(guid);
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Special Request
        public override Guid GetIndexComposition(string equity)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        #endregion
    }
}
