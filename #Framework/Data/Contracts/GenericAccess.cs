using System;
using System.Collections.Generic;

namespace Finance.Framework.Types
{
    public abstract class GenericAccess : IDisposable
    {
        #region Delegates & Events
        public delegate void MiscellaneousHandler(object sender, MiscData data);
        public delegate void StaticMarketDataHandler(object sender, Data data);
        public delegate void RealtimeMarketDataHandler(object sender, Data data);
        public delegate void HistoryMarketDataHandler(object sender, HistoryData data);

        /// <summary>
        /// Converts the value to Decimal. If it fails, the object is returned.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns></returns>
        static public object ConvertToDecimal(object value)
        {
            if (value is float || value is int || value is long || value is double || value is short)
                return Convert.ToDecimal(value);
            if (value is string)
            {
                decimal attempt;
                if (Decimal.TryParse((string)value, out attempt))
                    return attempt;
            }
            return value;
        }

        // Events
        /// <summary>
        /// Occurs when static data is received.
        /// </summary>
        public event StaticMarketDataHandler StaticMarketDataEvent;

        /// <summary>
        /// Occurs when realtime data is received.
        /// </summary>
        public event RealtimeMarketDataHandler RealtimeMarketDataEvent;

        /// <summary>
        /// Occurs when history data is received.
        /// </summary>
        public event HistoryMarketDataHandler HistoryMarketDataEvent;

        /// <summary>
        /// Occurs when an unknown or unmanaged event is received (to use for log purpose nor debugging).
        /// </summary>
        public event MiscellaneousHandler MiscellaneousEvent;

        /// <summary>
        /// Invokes the static market data event.
        /// </summary>
        /// <param name="data">The data.</param>
        protected void InvokeStaticMarketDataEvent(Data data)
        {
            if (StaticMarketDataEvent != null)
                StaticMarketDataEvent.Invoke(this, data);
        }

        /// <summary>
        /// Invokes the realtime market data event.
        /// </summary>
        /// <param name="data">The data.</param>
        protected void InvokeRealtimeMarketDataEvent(Data data)
        {
            if (RealtimeMarketDataEvent != null)
                RealtimeMarketDataEvent.Invoke(this, data);
        }

        /// <summary>
        /// Invokes the history market data event.
        /// </summary>
        /// <param name="data">The data.</param>
        protected void InvokeHistoryMarketDataEvent(HistoryData data)
        {
            if (HistoryMarketDataEvent != null)
                HistoryMarketDataEvent.Invoke(this, data);
        }

        /// <summary>
        /// Invokes the miscellaneous event.
        /// </summary>
        /// <param name="type">Type of MiscEvent.</param>
        /// <param name="data">The data.</param>
        /// <param name="message">The message content.</param>
        protected void InvokeMiscellaneousEvent(MiscEventType type, object data, string message)
        {
            if (MiscellaneousEvent != null)
                MiscellaneousEvent.Invoke(this, new MiscData(type, message, data));
        }
        #endregion

        #region Accessors
        /// <summary>
        /// Gets a value indicating whether this market access is connected.
        /// </summary>
        /// <value><c>true</c> if connected; otherwise, <c>false</c>.</value>
        abstract public bool Connected { get; }

        /// <summary>
        /// Gets the fields.
        /// </summary>
        /// <value>The fields.</value>
        static public string[] Fields { get { return Enum.GetNames(typeof(Field)); } }

        /// <summary>
        /// Gets the history fields.
        /// </summary>
        /// <value>The fields.</value>
        static public string[] HistoryFields { get { return Enum.GetNames(typeof(HistoryField)); } }

        /// <summary>
        /// Gets the current guids.
        /// </summary>
        /// <value>The current guids.</value>
        abstract public Guid[] CurrentGuids { get; }
        #endregion

        #region Basic Operations (Connect, Disconnect, CancelRequest, Dispose)
        /// <summary>
        /// Connects to the market access.
        /// </summary>
        abstract public bool Connect(bool enableLogging);

        /// <summary>
        /// Disconnects from the market access.
        /// </summary>
        abstract public void Disconnect();
        /// <summary>
        /// Cancels a previous request.
        /// </summary>
        /// <param name="guid">The GUID of the request.</param>
        /// <returns>Return true if the Guid was found.</returns>
        abstract public bool CancelRequest(Guid guid);

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        abstract public void Dispose();
        #endregion

        #region Request Static
        /// <summary>
        /// Creates a request with the specified fields and equities.
        /// </summary>
        /// <param name="equities">The equities.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="userfields">Fields that you want to add manually.</param>
        /// <returns>The GUID of the request.</returns>
        abstract public Guid RequestStatic(IList<string> equities, IList<Field> fields, IList<string> userfields);

        /// <summary>
        /// Creates a request with the specified fields and equities.
        /// </summary>
        /// <param name="equities">The equities.</param>
        /// <param name="fields">The fields.</param>
        /// <returns>The GUID of the request.</returns>
        virtual public Guid RequestStatic(IList<string> equities, IList<string> fields)
        {
            List<string> pUserfields = new List<string>();
            List<Field> pFields = new List<Field>();
            foreach (string field in fields)
            {
                if (Enum.IsDefined(typeof(Field), field))
                    pFields.Add((Field)Enum.Parse(typeof(Field), field));
                else
                    pUserfields.Add(field);
            }
            return RequestStatic(equities, pFields, pUserfields);
        }

        /// <summary>
        /// Creates a request with the specified field and equity.
        /// </summary>
        /// <param name="equity">The equity.</param>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        virtual public Guid RequestStatic(string equity, Field field)
        {
            List<string> equities = new List<string>(1);
            List<Field> fields = new List<Field>(1);
            equities.Add(equity);
            fields.Add(field);
            return RequestStatic(equities, fields, null);
        }

        /// <summary>
        /// Creates a request with the specified field and equity.
        /// </summary>
        /// <param name="equity">The equity.</param>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        virtual public Guid RequestStatic(string equity, IList<string> fields)
        {
            List<string> equities = new List<string>(1);
            equities.Add(equity);
            return RequestStatic(equities, null, fields);
        }

        /// <summary>
        /// Creates a request with the specified field and equity.
        /// </summary>
        /// <param name="equity">The equity.</param>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        virtual public Guid RequestStatic(string equity, string field)
        {
            List<string> equities = new List<string>(1);
            equities.Add(equity);
            if (Enum.IsDefined(typeof(Field), field))
            {
                List<Field> fields = new List<Field>(1);
                fields.Add((Field)Enum.Parse(typeof(Field), field));
                return RequestStatic(equities, fields, null);
            }
            else
            {
                List<string> userfields = new List<string>(1);
                userfields.Add(field);
                return RequestStatic(equities, null, userfields);
            }
        }
        #endregion

        #region Request History
        /// <summary>
        /// Creates a history request with the specified fields, equities during the given period.
        /// </summary>
        /// <param name="equities">The equities.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="from">Start date.</param>
        /// <param name="to">End date.</param>
        /// <param name="userfields">Fields that you want to add manually.</param>
        /// <returns>The GUID of the request.</returns>
        abstract public Guid RequestHistory(IList<string> equities, IList<HistoryField> fields, IList<string> userfields, DateTime from, DateTime to);

        /// <summary>
        /// Creates a request with the specified fields and equities.
        /// </summary>
        /// <param name="equities">The equities.</param>
        /// <param name="fields">The fields.</param>
        /// <returns>The GUID of the request.</returns>
        virtual public Guid RequestHistory(IList<string> equities, IList<string> fields, DateTime from, DateTime to)
        {
            IList<string> pUserfields = new List<string>();
            IList<HistoryField> pFields = new List<HistoryField>();
            foreach (string field in fields)
            {
                if (Enum.IsDefined(typeof(HistoryField), field))
                    pFields.Add((HistoryField)Enum.Parse(typeof(HistoryField), field));
                else
                    pUserfields.Add(field);
            }
            return RequestHistory(equities, pFields, pUserfields, from, to);
        }

        /// <summary>
        /// Creates a history request with the specified field, equities during the given period.
        /// </summary>
        /// <param name="equity">The equity.</param>
        /// <param name="field">The field.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns></returns>
        virtual public Guid RequestHistory(string equity, HistoryField field, DateTime from, DateTime to)
        {
            IList<string> equities = new List<string>(1);
            IList<HistoryField> fields = new List<HistoryField>(1);
            equities.Add(equity);
            fields.Add(field);
            return RequestHistory(equities, fields, null, from, to);
        }

        /// <summary>
        /// Creates a history request with the specified field, equities during the given period.
        /// </summary>
        /// <param name="equity">The equity.</param>
        /// <param name="field">The field.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns></returns>
        virtual public Guid RequestHistory(string equity, IList<string> fields, DateTime from, DateTime to)
        {
            IList<string> equities = new List<string>(1);
            equities.Add(equity);
            return RequestHistory(equities, fields, from, to);
        }

        /// <summary>
        /// Creates a history request with the specified field, equity during the given period.
        /// </summary>
        /// <param name="equity">The equity.</param>
        /// <param name="field">The field.</param>
        /// <param name="userfield">The userfield.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns></returns>
        private Guid RequestHistory(string equity, string field, DateTime from, DateTime to)
        {
            IList<string> equities = new List<string>(1);
            equities.Add(equity);
            if (Enum.IsDefined(typeof(Field), field))
            {
                List<HistoryField> fields = new List<HistoryField>(1);
                fields.Add((HistoryField)Enum.Parse(typeof(HistoryField), field));
                return RequestHistory(equities, fields, null, from, to);
            }
            else
            {
                IList<string> userfields = new List<string>(1);
                userfields.Add(field);
                return RequestHistory(equities, null, userfields, from, to);
            }
        }
        #endregion

        #region Request Realtime
        /// <summary>
        /// Starts a realtime subscription with the specified fields and equities.
        /// </summary>
        /// <param name="equities">The equities.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="userfields">Fields that you want to add manually.</param>
        /// <returns>The GUID of the request.</returns>
        abstract public Guid RequestRealtime(IList<string> equities, IList<Field> fields, IList<string> userfields);

        /// <summary>
        /// Starts a realtime subscription with the specified fields and equity.
        /// </summary>
        /// <param name="equities">The equity.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="userfields">Fields that you want to add manually.</param>
        /// <returns>The GUID of the request.</returns>
        virtual public Guid RequestRealtime(string equity, IList<Field> fields, IList<string> userfields)
        {
            IList<string> equities = new List<string>(1);
            equities.Add(equity);
            return RequestRealtime(equities, fields, userfields);
        }

        /// <summary>
        /// Creates a request with the specified fields and equities.
        /// </summary>
        /// <param name="equities">The equities.</param>
        /// <param name="fields">The fields.</param>
        /// <returns>The GUID of the request.</returns>
        virtual public Guid RequestRealtime(IList<string> equities, IList<string> fields)
        {
            IList<string> pUserfields = new List<string>();
            IList<Field> pFields = new List<Field>();
            foreach (string field in fields)
            {
                if (Enum.IsDefined(typeof(Field), field))
                    pFields.Add((Field)Enum.Parse(typeof(Field), field));
                else
                    pUserfields.Add(field);
            }
            return RequestRealtime(equities, pFields, pUserfields);
        }

        /// <summary>
        /// Starts a realtime subscription with the specified fields and equity.
        /// </summary>
        /// <param name="equity">The equity.</param>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        public Guid RequestRealtime(string equity, Field field)
        {
            IList<string> equities = new List<string>(1);
            IList<Field> fields = new List<Field>(1);
            equities.Add(equity);
            fields.Add(field);
            return RequestRealtime(equities, fields, null);
        }

        /// <summary>
        /// Starts a realtime subscription with the specified fields and equity.
        /// </summary>
        /// <param name="equity">The equity.</param>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        private Guid RequestRealtime(string equity, IList<Field> fields)
        {
            IList<string> equities = new List<string>(1);
            equities.Add(equity);
            return RequestRealtime(equities, fields, null);
        }

        /// <summary>
        /// Starts a realtime subscription with the specified fields and equity.
        /// </summary>
        /// <param name="equity">The equity.</param>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        private Guid RequestRealtime(string equity, IList<string> fields)
        {
            IList<string> equities = new List<string>(1);
            equities.Add(equity);
            return RequestRealtime(equities, fields);
        }

        /// <summary>
        /// Starts a realtime subscription with the specified fields and equity.
        /// </summary>
        /// <param name="equity">The equity.</param>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        public Guid RequestRealtime(string equity, IList<object> fields)
        {
            if (fields.Count < 1)
                throw new Exception("RequestRealtime needs fields.");
            if (fields[0] is Field)
                return RequestRealtime(equity, (IList<Field>)fields);
            else if (fields[0] is string)
            {
                List<string> lst = new List<string>();
                foreach (object o in fields)
                    lst.Add(o as string);
                return RequestRealtime(equity, lst);
            }
            else
                throw new Exception("RequestRealtime needs String or Field (enum) types for fields.");
        }

        /// <summary>
        /// Starts a realtime subscription with the specified fields and equity.
        /// </summary>
        /// <param name="equity">The equity.</param>
        /// <param name="field">The field.</param>
        /// <param name="userfield">The userfield.</param>
        /// <returns></returns>
        virtual public Guid RequestRealtime(string equity, string field)
        {
            IList<string> equities = new List<string>(1);
            equities.Add(equity);
            if (Enum.IsDefined(typeof(Field), field))
            {
                IList<Field> fields = new List<Field>(1);
                fields.Add((Field)Enum.Parse(typeof(Field), field));
                return RequestRealtime(equities, fields, null);
            }
            else
            {
                IList<string> userfields = new List<string>(1);
                userfields.Add(field);
                return RequestRealtime(equities, null, userfields);
            }
        }
        #endregion

        #region Special Request
        abstract public Guid GetIndexComposition(string equity);
        #endregion
    }
}
