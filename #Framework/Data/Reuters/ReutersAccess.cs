using System;
using System.Collections.Generic;
using SSLRecord;
using SSLTimeSeries;
using Finance.Framework.Types;

namespace Finance.Framework.DataAccess.Reuters
{
    /// <summary>
    /// Market data via Reuters
    /// </summary>
    public class ReutersAccess : GenericAccess
    {
        private readonly string _service = "IDN_SELECTFEED";
        private readonly IDictionary<object, Request> recordToRequest = new Dictionary<object, Request>();
        private readonly IDictionary<Guid, Request> guidToRequest = new Dictionary<Guid, Request>();
        private readonly IList<Request> acklessRequests = new List<Request>();
        public override bool Connected { get { return (CurrentGuids.Length > 0); } }
        public override Guid[] CurrentGuids { get { Guid[] arr = new Guid[guidToRequest.Keys.Count]; guidToRequest.Keys.CopyTo(arr, 0); return arr; } }
        private bool _loggingEnabled;

        #region Connection / Instance
        /// <summary>
        /// Initializes a new instance of the <see cref="ReutersAccess"/> class.
        /// </summary>
        public ReutersAccess()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReutersAccess"/> class.
        /// </summary>
        /// <param name="service">The service (default is IDN_SELECTFEED).</param>
        public ReutersAccess(string service)
        {
            _service = service;
        }

        public override void Dispose()
        {
            while (CurrentGuids.Length > 0)
                CancelRequest(CurrentGuids[0]);
        }

        public override bool Connect(bool enableLogging)
        {
            _loggingEnabled = enableLogging;
            return true;
        }

        public override void Disconnect()
        {
            List<Guid> id = new List<Guid>();

            lock (acklessRequests)
            {
                foreach (Request request in acklessRequests)
                    CancelRequest(request.Guid);
            }
            lock (guidToRequest)
            {
                foreach (Guid key in guidToRequest.Keys)
                    id.Add(key);
                recordToRequest.Clear();
                for (int c = 0; c < id.Count; c++)
                    CancelRequest(id[c]);
                guidToRequest.Clear();
            }
        }
        #endregion

        #region Reuters Events [STATIC / REALTIME]
        /// <summary>
        /// Gets the data from reply.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="indices">The indices.</param>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        private static Data GetDataFromReply(Request request, Array indices, Array values)
        {
            Data data = new Data(request.Guid, request.Equity, DateTime.Now);
            for (int c = 0; c < values.Length; c++)
            {
                string str = request.Fields[(int)indices.GetValue(c)];
                object obj = values.GetValue(c);

                Field? marketField = ReutersTransco.TranscoToMarketField(str);
                if (marketField.HasValue)
                {
                    if (obj is string && ((string)obj).Length > 0 && ((string)obj)[0] == '+')
                        data.Add(marketField.Value, ((string)obj).Remove(0, 1));
                    else
                        data.Add(marketField.Value, obj);
                }
                else
                {
                    if (obj is string && ((string)obj).Length > 0 && ((string)obj)[0] == '+')
                        data.Add(str, ((string)obj).Remove(0, 1));
                    else
                        data.Add(str, obj);
                }
            }
            return data;
        }

        void RecordTick(Record rec, bool Stale, ref Array indices, ref Array values)
        {
            if (!recordToRequest.ContainsKey(rec))
            {
                InvokeMiscellaneousEvent(MiscEventType.Error, rec, "Request could not be found.");
                return;
            }
            Request request = recordToRequest[rec];
            Data data = GetDataFromReply(request, indices, values);
            // INVOKE THE AD-HOC EVENT
            if (request.Type == RequestType.Realtime)
                InvokeRealtimeMarketDataEvent(data);
            else
                throw new NotImplementedException(request.Type + " hasn't been implemented yet.");

        }

        void RecordHasData(Record rec, bool stale, ref Array indices, ref Array values)
        {
            if (!recordToRequest.ContainsKey(rec))
            {
                InvokeMiscellaneousEvent(MiscEventType.Error, rec, "Request could not be found.");
                return;
            }
            Request request = recordToRequest[rec];
            // DATA PREPARATION
            Data data = GetDataFromReply(request, indices, values);
            // INVOKE THE AD-HOC EVENT
            switch (request.Type)
            {
                case RequestType.Static:
                    InvokeStaticMarketDataEvent(data);
                    break;
                case RequestType.Realtime:
                    InvokeRealtimeMarketDataEvent(data);
                    break;
                case RequestType.IndexComposition:
                    ManageIndexCompositionEvent(data, request);
                    break;
                default:
                    throw new NotImplementedException(request.Type + " hasn't been implemented yet.");
            }
            // CHECKS THE KIND OF REQUEST
            if (request.Type == RequestType.Static)
                CancelRequest(request.Guid);
        }

        void RecordInactive(Record rec, string text)
        {
            Request request = recordToRequest[rec];
            CancelRequest(request.Guid);
            if (_loggingEnabled)
                InvokeMiscellaneousEvent(MiscEventType.Info, request, "RecordInactive: " + text);
        }

        void RecordUpdate(Record rec, bool Stale, ref Array indices, ref Array values)
        {
            InvokeMiscellaneousEvent(MiscEventType.Info, values, "RecordUpdate");
        }

        void RecordStale(Record rec, string text)
        {
            Request request = recordToRequest[rec];
            InvokeMiscellaneousEvent(MiscEventType.Info, request, "RecordStale: " + text);
        }

        void RecordNotStale(Record rec, string text)
        {
            Request request = recordToRequest[rec];
            InvokeMiscellaneousEvent(MiscEventType.Info, request, "RecordNotStale: " + text);
        }

        void RecordInfo(Record rec, string text)
        {
            Request request = recordToRequest[rec];
            InvokeMiscellaneousEvent(MiscEventType.Info, request, "RecordInfo: " + text);
        }

        void RecordCorrectionTick(Record rec, bool Stale, ref Array indices, ref Array values)
        {
            Request request = recordToRequest[rec];
            InvokeMiscellaneousEvent(MiscEventType.Info, request, "RecordCorrectionTick");
        }

        void RecordCloseTick(Record rec, bool Stale, ref Array indices, ref Array values)
        {
            Request request = recordToRequest[rec];
            InvokeMiscellaneousEvent(MiscEventType.Info, request, "RecordCloseTick");
        }
        #endregion

        #region Special Request & Events
        public override Guid GetIndexComposition(string equity)
        {
            IList<string> equities = new List<string>(1);
            equities.Add("0#" + equity);
            return CreateNewRequest(RequestType.IndexComposition, equities, new Field[] { Field.F_Name }, new string[] { "LINK_1", "LINK_2", "LINK_3", "LINK_4", "LINK_5", "LINK_6", "LINK_7", "LINK_8", "LINK_9", "LINK_10", "LINK_11", "LINK_12", "LINK_13", "LINK_14", "REF_COUNT", "PREF_LINK" });
        }

        private void ManageIndexCompositionEvent(Data data, Request request)
        {
            CancelRequestFromReuters(request);

            IList<string> equities = new List<string>(1);
            IEnumerator<DataFieldItem> ienum = data.GetEnumerator();
            while (ienum.MoveNext())
                if (ienum.Current.Field.StartsWith("LINK_") && !string.IsNullOrEmpty(ienum.Current.Value as string) && (ienum.Current.Value as string) != ((Data)request.Data).Security)
                    ((Data)request.Data).Add(((Data)request.Data).Count.ToString(), ienum.Current.Value);
            if ((decimal)data["REF_COUNT"] < 14)
            {
                InvokeStaticMarketDataEvent(request.Data as Data);
                return;
            }
            equities.Add(data["PREF_LINK"].ToString());
            CreateNewRequest(request, RequestType.IndexComposition, equities, null, request.Fields);
        }
        #endregion

        #region Reuters Events [HISTORY]
        void TimeSeriesComplete(TimeSeries series, string text)
        {
            lock (recordToRequest)
            {
                int resultCount = series.Count;
                Array arr = new object[resultCount];
                series.TableOfValues(ref arr, 0, 0, 0, 0);
                Request request = recordToRequest[series];
                if (arr.Length <= resultCount)
                {
                    InvokeHistoryMarketDataEvent(null);
                    CancelRequest(request.Guid);
                    return;
                }
                HistoryData histData = new HistoryData(request.Guid, request.Equity);
                for (int c = 0; c < series.Count; c++)
                {
                    if (series.ElementDefinitions().Count < 1)
                        continue;
                    DateTime time = series.ElementAt(c, 0).DateTime;
                    HistoryDataItem marketData = new HistoryDataItem(request.Guid, time);
                    for (int d = 0; d < series.ElementDefinitions().Count; d++)
                    {
                        TimeSeriesElement elem = series.ElementAt(c, d);
                        marketData.Time = elem.DateTime;
                        ElementDefinition elementDef = elem.Definition();
                        HistoryField? marketField = ReutersTransco.TranscoToHistoryMarketField(elementDef.Name);
                        if (marketField.HasValue)
                            marketData.Add(marketField.Value, elem.ValueAsDouble);
                        else
                            marketData.Add(elementDef.Name, elem.ValueAsDouble);
                    }
                    histData.Add(marketData);
                }
                InvokeHistoryMarketDataEvent(histData);
                CancelRequest(request.Guid);
            }
        }

        void series_TimeSeriesUpdate(TimeSeries series, ref Array table)
        {
            InvokeMiscellaneousEvent(MiscEventType.Info, table, "TimeSeriesUpdate");
        }

        void TimeSeriesNewSample(TimeSeries series, TimeSeriesSample sample)
        {
            InvokeMiscellaneousEvent(MiscEventType.Info, sample, "TimeSeriesNewSample");
        }

        void TimeSeriesInfo(TimeSeries series, string text)
        {
            InvokeMiscellaneousEvent(MiscEventType.Info, series, "TimeSeriesInfo: " + text);
        }

        void TimeSeriesInactive(TimeSeries series, string text)
        {
            if (recordToRequest.ContainsKey(series))
            {
                Request request = recordToRequest[series];
                InvokeMiscellaneousEvent(MiscEventType.Info, series, "TimeSeriesInactive: " + text);
                CancelRequest(request.Guid);
            }
        }
        #endregion

        #region Requests
        private Guid CreateNewHistoryRequest(IList<string> equities, ICollection<HistoryField> fields, ICollection<string> userfields, DateTime startDate, DateTime endDate)
        {
            if ((fields.Count == 0 && userfields.Count == 0) || equities.Count == 0)
                return Guid.Empty;
            if (equities.Count > 1)
                InvokeMiscellaneousEvent(MiscEventType.Info, null, "Reuters API only supports one equity / request.");
            // EVENTS
            TimeSeries series = new TimeSeries();
            Request request = new Request(equities[0], series);
            acklessRequests.Add(request);
            series.TimeSeriesComplete += delegate(TimeSeries receivedSeries, string text)
            {
                lock (recordToRequest)
                {
                    if (!recordToRequest.ContainsKey(receivedSeries))
                    {
                        if (acklessRequests.Contains(request))
                            acklessRequests.Remove(request);
                        recordToRequest.Add(receivedSeries, request);
                        guidToRequest.Add(request.Guid, request);
                    }
                }
            };
            series.TimeSeriesComplete += TimeSeriesComplete;
            series.TimeSeriesInactive += TimeSeriesInactive;
            if (_loggingEnabled)
            {
                series.TimeSeriesInfo += TimeSeriesInfo;
                series.TimeSeriesNewSample += TimeSeriesNewSample;
                series.TimeSeriesUpdate += series_TimeSeriesUpdate;
            }
            // DATA
            series.ServiceName = _service;
            series.Name = equities[0];
            series.PeriodType = (int)PeriodicityConstants.ssltsPrdDaily;
            series.SetRangeBetween(startDate, endDate);
            foreach (HistoryField field in fields)
                request.Series.AddElement(ReutersTransco.TranscoFromHistoryMarketField(field));
            if (userfields != null)
                foreach (string field in userfields)
                    request.Series.AddElement(field);
            series.Start();
            if (_loggingEnabled)
                InvokeMiscellaneousEvent(MiscEventType.Info, request.Guid, "Request " + request.Guid + " was created.");
            return request.Guid;
        }

        private Guid CreateNewRequest(RequestType type, IList<string> equities, ICollection<Field> fields, ICollection<string> userfields)
        {
            Request request = new Request(equities[0], type, null);
            if (fields != null)
                foreach (Field field in fields)
                    request.Fields.Add(ReutersTransco.TranscoFromMarketField(field));
            if (userfields != null)
                foreach (string field in userfields)
                    request.Fields.Add(field);
            return CreateNewRequest(request, type, equities, fields, userfields);
        }

        private Guid CreateNewRequest(Request request, RequestType type, IList<string> equities, ICollection<Field> fields, ICollection<string> userfields)
        {
            RecordClass record = new RecordClass();
            request.Record = record;
            request.Record.Name = equities[0];
            if ((fields != null && fields.Count == 0 && userfields != null && userfields.Count == 0) || equities.Count == 0)
                return Guid.Empty;
            if (equities.Count > 1)
                InvokeMiscellaneousEvent(MiscEventType.Info, null, "Reuters API only supports one equity / request.");
            // EVENTS
            acklessRequests.Add(request);
            record.RecordTick += RecordTick;
            record.RecordHasData += delegate(Record rec, bool Stale, ref Array indices, ref Array values)
            {
                if (!recordToRequest.ContainsKey(rec))
                {
                    if (acklessRequests.Contains(request))
                        acklessRequests.Remove(request);
                    recordToRequest.Add(rec, request);
                    guidToRequest.Add(request.Guid, request);
                }
            };
            record.RecordHasData += RecordHasData;
            if (_loggingEnabled)
            {
                record.RecordUpdate += RecordUpdate;
                record.RecordCloseTick += RecordCloseTick;
                record.RecordCorrectionTick += RecordCorrectionTick;
                record.RecordInactive += RecordInactive;
                record.RecordInfo += RecordInfo;
                record.RecordNotStale += RecordNotStale;
                record.RecordStale += RecordStale;
            }
            // DATA
            record.ServiceName = _service;

            if (fields != null)
                foreach (Field field in fields)
                    request.Record.Add(ReutersTransco.TranscoFromMarketField(field));
            if (userfields != null)
                foreach (string field in userfields)
                    request.Record.Add(field);
            // START
            switch (type)
            {
                case RequestType.IndexComposition:
                    if (request.Data == null)
                        request.Data = new Data(request.Guid, request.Equity.Replace("0#", ""), DateTime.Now);
                    goto case RequestType.Realtime;
                case RequestType.Realtime:
                    request.Record.Start();
                    break;
                case RequestType.Static:
                    request.Record.Snapshot(0);
                    break;
                case RequestType.History:
                    break;
                default:
                    throw new NotImplementedException(type + " hasn't been implemented yet.");
            }
            if (_loggingEnabled)
                InvokeMiscellaneousEvent(MiscEventType.Info, request.Guid, "Request " + request.Guid + " was created.");
            return request.Guid;
        }

        public override Guid RequestStatic(IList<string> equities, IList<Field> fields, IList<string> userfields)
        {
            return CreateNewRequest(RequestType.Static, equities, fields, userfields);
        }

        public override Guid RequestRealtime(IList<string> equities, IList<Field> fields, IList<string> userfields)
        {
            return CreateNewRequest(RequestType.Realtime, equities, fields, userfields);
        }

        public override Guid RequestHistory(IList<string> equities, IList<HistoryField> fields, IList<string> userfields, DateTime from, DateTime to)
        {
            return CreateNewHistoryRequest(equities, fields, userfields, from, to);
        }

        private void CancelRequestFromReuters(Request request)
        {
            try
            {
                if (request.Type == RequestType.History)
                {
                    request.Series.Stop();
                    recordToRequest.Remove(request.Series);
                }
                else
                {
                    request.Record.Stop();
                    recordToRequest.Remove(request.Record);
                }
            }
            catch
            {
            }
        }

        public override bool CancelRequest(Guid guid)
        {
            foreach (Request current in acklessRequests)
            {
                if (current.Guid == guid)
                {
                    CancelRequestFromReuters(current);
                    if (_loggingEnabled)
                        InvokeMiscellaneousEvent(MiscEventType.Info, guid, "Request " + guid + " was removed (was an ackless request).");
                    break;
                }
            }
            if (guidToRequest.ContainsKey(guid))
            {
                Request request = guidToRequest[guid];
                CancelRequestFromReuters(request);
                guidToRequest.Remove(request.Guid);
                if (_loggingEnabled)
                    InvokeMiscellaneousEvent(MiscEventType.Info, guid, "Request " + guid + " was removed.");
                return true;
            }
            return false;
        }
        #endregion
    }
}
