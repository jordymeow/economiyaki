using System;
using System.Collections.Generic;
using System.Reflection;
using Finance.MarketAccess;
using IronPython.Compiler;
using IronPython.Hosting;
using Finance.Framework.Types;
using Finance.Framework.DataAccess.Bloomberg;
using Finance.Framework.DataAccess.Reuters;
using IronPython.Runtime.Exceptions;
using System.Windows.Forms;

namespace Finance.Framework.Core
{
    public class PythonExecuter : IDisposable
    {
        /// <summary>
        /// Gets the core engine (singleton).
        /// </summary>
        /// <value>The core engine.</value>
        static public PythonEngine CoreEngine
        {
            get
            {
                if (_pythonEngineInstance == null)
                {
                    Options.DebugMode = false;
                    Options.WarningOnIndentationInconsistency = true;
                    Options.PrivateBinding = true;
                    _pythonEngineInstance = new PythonEngine();
                }
                return _pythonEngineInstance;
            }
        }

        public delegate void ErrOutEventHandler(object sender, string message, int line);
        public event ErrOutEventHandler ErrOut;

        public delegate void StdOutEventHandler(object sender, string message);
        public event StdOutEventHandler StdOut;

        public bool Started { get { return _Started; } }
        public SerializableDictionary<string, object> Constants;

        private bool _Started;
        private readonly DynamicStream _dynStdStream = new DynamicStream();
        private readonly DynamicStream _dynErrStream = new DynamicStream();
        private CompiledCode _compiledCode;
        private EngineModule _currentModule;
        private readonly Guid _guidForData = Guid.NewGuid();
        private readonly Guid _guidForDataItem = Guid.NewGuid();
        private readonly Guid _guidForHistoryData = Guid.NewGuid();
        const string _staticScript = "";
        static private PythonEngine _pythonEngineInstance;
        private MarketPython _marketPython;

        #region Instance & Dispose
        public PythonExecuter(MarketPython marketPython)
        {
            _marketPython = marketPython;
            InitializeIronPython(true);
        }

        delegate void RegisterDataAccessProviderHandler(GenericAccess dataAccess);
        delegate GenericAccess InitiateGenericAccessHandler();

        void dataAccess_RealtimeMarketDataEvent(object sender, Data data)
        {
            _marketPython.PushInputMessage(new GfxMessage(GfxType.RealtimeData, data));
        }

        void dataAccess_StaticMarketDataEvent(object sender, Data data)
        {
            _marketPython.PushInputMessage(new GfxMessage(GfxType.StaticData, data));
        }

        void dataAccess_MiscellaneousEvent(object sender, MiscData data)
        {
            if (data.Type == MiscEventType.Error)
                ErrOut.Invoke(sender, data.Message, -1);
        }

        void dataAccess_HistoryMarketDataEvent(object sender, HistoryData data)
        {
            _marketPython.PushInputMessage(new GfxMessage(GfxType.HistoryData, data));
        }

        private void InitializeIronPython(bool isFirst)
        {
            if (isFirst)
            {
                _dynStdStream.TextReceivedEvent += dynStream_StdOutEvent;
                _dynErrStream.TextReceivedEvent += dynStream_ErrOutEvent;
                CoreEngine.SetStandardError(_dynErrStream);
                CoreEngine.SetStandardOutput(_dynStdStream);
                CoreEngine.AddToPath(AppDomain.CurrentDomain.BaseDirectory);
                CoreEngine.LoadAssembly(Assembly.GetAssembly(typeof(Data)));
                CoreEngine.LoadAssembly(Assembly.GetAssembly(typeof(GfxMessage)));
                CoreEngine.LoadAssembly(Assembly.GetAssembly(typeof(ReutersAccess)));
                CoreEngine.LoadAssembly(Assembly.GetAssembly(typeof(BloombergAccess)));
            }
            _currentModule = CoreEngine.CreateModule("main", false);
            _currentModule.Globals.Add("RegisterDataAccess", new RegisterDataAccessProviderHandler(RegisterDataAccess));
            _currentModule.Globals.Add("InitiateReuters", new InitiateGenericAccessHandler(InitiateReuters));
            _currentModule.Globals.Add("InitiateBloomberg", new InitiateGenericAccessHandler(InitiateBloomberg));
            _currentModule.Globals.Add("i", null);
            _currentModule.Globals.Add("o", null);
            if (Constants == null)
                Constants = new SerializableDictionary<string, object>();
            foreach (string key in Constants.Keys)
                _currentModule.Globals.Add(key, Constants[key]);
        }

        void dynStream_StdOutEvent(object sender, string txt)
        {
            StdOut.Invoke(sender, txt);
        }

        void dynStream_ErrOutEvent(object sender, string txt)
        {
            ErrOut.Invoke(sender, txt, -1);
        }

        public void Dispose()
        {
            Stop();
            CoreEngine.Dispose();

            _dynErrStream.TextReceivedEvent -= dynStream_ErrOutEvent;
            _dynStdStream.TextReceivedEvent -= dynStream_StdOutEvent;
        }
        #endregion

        #region Python Internal Shortcuts
        private void RegisterDataAccess(GenericAccess dataAccess)
        {
            dataAccess.HistoryMarketDataEvent += new GenericAccess.HistoryMarketDataHandler(dataAccess_HistoryMarketDataEvent);
            dataAccess.MiscellaneousEvent += new GenericAccess.MiscellaneousHandler(dataAccess_MiscellaneousEvent);
            dataAccess.RealtimeMarketDataEvent += new GenericAccess.RealtimeMarketDataHandler(dataAccess_RealtimeMarketDataEvent);
            dataAccess.StaticMarketDataEvent += new GenericAccess.StaticMarketDataHandler(dataAccess_StaticMarketDataEvent);
        }

        private GenericAccess InitiateReuters()
        {
            GenericAccess access = new ReutersAccess();
            access.Connect(false);
            RegisterDataAccess(access);
            return access;
        }

        private GenericAccess InitiateBloomberg()
        {
            GenericAccess access = new BloombergAccess();
            access.Connect(false);
            RegisterDataAccess(access);
            return access;
        }
        #endregion

        #region Constants
        public bool ConstantAdd(string cst, object value)
        {
            if (Constants.ContainsKey(cst))
                return false;
            Constants.Add(cst, value);
            _currentModule.Globals.Add(cst, value);
            return true;
        }

        public void ConstantRemove(string cst)
        {
            Constants.Remove(cst);
            _currentModule.Globals.Remove(cst);
        }

        public bool ConstantModify(string cst, object value)
        {
            if (!Constants.ContainsKey(cst)) return false;
            Constants[cst] = value;
            _currentModule.Globals[cst] = value;
            return true;
        }
        #endregion

        #region Start / Stop
        public void Start(string script)
        {
            try
            {
                InitializeIronPython(false);
                _compiledCode = CoreEngine.Compile(_staticScript + script);
                _compiledCode.Execute(_currentModule);
                _Started = true;
                if (_currentModule.Globals.Keys.Contains("RetrieveData"))
                    Execute(null);
            }
            catch (Exception ex)
            {
                ManagePythonException(ex);
            }
        }

        public void Stop()
        {
            foreach (string key in _currentModule.Globals.Keys)
            {
                object o = _currentModule.Globals[key];
                if (o is GenericAccess)
                {
                    ((GenericAccess)o).Disconnect();
                }
            }
            CoreEngine.Shutdown();
            _compiledCode = null;
            _Started = false;
        }
        #endregion

        #region Code execution
        /// <summary>
        /// Prepares the data (i for input / o for ouput) for the Python script execution.
        /// </summary>
        /// <param name="message">The message.</param>
        private void PrepareData(GfxMessage message)
        {
            if (message == null)
                return;
            _currentModule.Globals["i"] = message.Content;
            if (message.Content is Data)
            {
                Data content = (Data)message.Content;
                _currentModule.Globals["o"] = new Data(_guidForDataItem, content.Security, content.Time);
            }
            else if (message.Content is HistoryDataItem)
            {
                HistoryData content = (HistoryData)message.Content;
                _currentModule.Globals["o"] = new HistoryData(_guidForHistoryData, content.Security, content.Time);
            }
            else if (message.Content is MultiData)
            {
                MultiData content = (MultiData)message.Content;
                _currentModule.Globals["o"] = new MultiData(_guidForData, content.Time);
            }
            else if (message.Content is MultiHistoryData)
            {
                MultiHistoryData content = (MultiHistoryData)message.Content;
                _currentModule.Globals["o"] = new MultiHistoryData(_guidForData, content.Time);
            }
            else
                ErrOut.Invoke(this, String.Format("{0} input type can't be processed.", message.Content.GetType()), -1);
        }

        /// <summary>
        /// Start the script, and return the output.
        /// </summary>
        /// <param name="message">The message.</param>
        private object ProcessMessage(GfxMessage message)
        {
            try
            {
                object data = null;
                if (message == null)
                {
                    ErrOut.Invoke(this, "A null message had been received.", -1);
                    return null;
                }

                switch (message.MessageType)
                {
                    case GfxType.RealtimeData:
                    case GfxType.StaticData:
                        if (_currentModule.Globals.Keys.Contains("Single"))
                            data = CoreEngine.Evaluate("Single()", _currentModule);
                        else
                        {
                            ErrOut.Invoke(this, "Realtime/Static data need to call the 'def Single()' function.", -1);
                            return null;
                        }
                        break;
                    case GfxType.RealtimeDataMulti:
                    case GfxType.StaticDataMulti:
                        if (_currentModule.Globals.Keys.Contains("Multi"))
                            data = CoreEngine.Evaluate("Multi()", _currentModule);
                        else
                        {
                            ErrOut.Invoke(this, "Multi-stocks data need to call the 'def Multi()' function.", -1);
                            return null;
                        }
                        break;
                    case GfxType.HistoryData:
                        if (_currentModule.Globals.Keys.Contains("History"))
                            data = CoreEngine.Evaluate("History()", _currentModule);
                        else
                        {
                            ErrOut.Invoke(this, "History data need to call the 'def History()' function.", -1);
                            return null;
                        }
                        break;
                    case GfxType.HistoryDataMulti:
                        if (_currentModule.Globals.Keys.Contains("MultiHistory"))
                            data = CoreEngine.Evaluate("MultiHistory()", _currentModule);
                        else
                        {
                            ErrOut.Invoke(this, "MultiHistory data need to call the 'def MultiHistory()' function.", -1);
                            return null;
                        }
                        break;
                }
                // Get the 'o' variable if the script function didn't return anything.
                if (data == null)
                    data = _currentModule.Globals["o"];
                return data;
            }
            catch (Exception ex)
            {
                ManagePythonException(ex);
                return null;
            }
        }

        /// <summary>
        /// Executes the Python code, with the given message as Input Data.
        /// </summary>
        /// <param name="script">The script.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public GfxMessage Execute(GfxMessage message)
        {
            try
            {
                if (message != null)
                    PrepareData(message);
                object data = ProcessMessage(message);
                if (data != null)
                {
                    if (data is HistoryData)
                    {
                        if (((HistoryData)data).Count != 0)
                            return new GfxMessage(message.MessageType, data);
                    }
                    if (data is Data)
                    {
                        if (((Data)data).Count != 0)
                            return new GfxMessage(message.MessageType, data);
                    }
                    if (data is MultiData)
                    {
                        if (((MultiData)data).Count != 0)
                            return new GfxMessage(message.MessageType, data);
                    }
                }
                if (message != null)
                    ErrOut.Invoke(this, "The output message wasn't created (try Debug mode).", -1);
                return null;
            }
            catch (Exception ex)
            {
                ErrOut.Invoke(this, ex.Message, -1);
                return null;
            }

        }

        /// <summary>
        /// Executes the Python code for one step -> e.g the next message.
        /// </summary>
        /// <param name="script">The script.</param>
        /// <returns></returns>
        public GfxMessage StepByStep(string script, GfxMessage message)
        {
            return DebugCode(script, true, message);
        }

        /// <summary>
        /// Debugs the Python code, but doesn't excute the messaging process.
        /// </summary>
        /// <param name="script">The script.</param>
        /// <param name="message">The message.</param>
        public void DebugCode(string script, GfxMessage message)
        {
            DebugCode(script, false, message);
        }

        /// <summary>
        /// Starts a debug of the script.
        /// </summary>
        /// <param name="script">The script.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        private GfxMessage DebugCode(string script, bool stepByStep, GfxMessage message)
        {
            try
            {
                object data = null;
                InitializeIronPython(false);
                _compiledCode = CoreEngine.Compile(_staticScript + script);
                PrepareData(message);
                if (message == null)
                    StdOut.Invoke(this, "No input messages -> only the 'main' code will be executed.");
                CoreEngine.Execute(script, _currentModule);
                if (stepByStep)
                    data = Execute(message);
                else if (message != null)
                    data = ProcessMessage(message);
                Stop();
                if (stepByStep)
                    return (GfxMessage)data;
                if (data == null)
                    return null;
                else if (data is MultiData)
                {
                    MultiData typedData = data as MultiData;
                    if (typedData.Count != 0)
                    {
                        StdOut.Invoke(this, String.Format("A MultiData (Static/Realtime) message was created with {0} stock(s).", typedData.Count));
                        return null;
                    }
                }
                else if (data is HistoryData)
                {
                    HistoryData typedData = data as HistoryData;
                    if (typedData.Count != 0)
                    {
                        StdOut.Invoke(this, String.Format("A HistoryData (History) message was created with {0} date(s).", typedData.Count));
                        return null;
                    }
                }
                else if (data is Data)
                {
                    Data typedData = data as Data;
                    if (typedData.Count != 0)
                    {
                        StdOut.Invoke(this, String.Format("A Data (Static/Realtime) message was created with {0} field(s).", typedData.Count));
                        return null;
                    }
                }
                else
                {
                    StdOut.Invoke(this, String.Format("A unknown message type was created : {0}.", data.GetType()));
                    return null;
                }
                ErrOut.Invoke(this, "The output message was not created (or empty). Modify the 'o' (that is equal to 'i', without the stocks/fields information) object or create a new one (MultiData, HistoryData or Data) and return it.", -1);

            }
            catch (Exception ex)
            {
                ManagePythonException(ex);
                Stop();

            }
            return null;
        }
        #endregion

        #region Exception Management
        public void ManagePythonException(Exception ex)
        {
            if (ex is IronPython.Runtime.Exceptions.PythonAssertionErrorException)
                ErrOut.Invoke(this, "ArgumentTypeException" + ": " + ex.Message, -1);
            else if (ex is IronPython.Runtime.Exceptions.PythonImportErrorException)
                ErrOut.Invoke(this, "ArgumentTypeException" + ": " + ex.Message, -1);
            else if (ex is IronPython.Runtime.Exceptions.PythonDeprecationWarningException)
                ErrOut.Invoke(this, "PythonDeprecationWarningException" + ": " + ex.Message, -1);
            else if (ex is IronPython.Runtime.Exceptions.PythonEnvironmentErrorException)
                ErrOut.Invoke(this, "PythonEnvironmentErrorException" + ": " + ex.Message, -1);
            else if (ex is IronPython.Runtime.Exceptions.PythonFloatingPointErrorException)
                ErrOut.Invoke(this, "PythonFloatingPointErrorException" + ": " + ex.Message, -1);
            else if (ex is IronPython.Runtime.Exceptions.PythonFutureWarningException)
                ErrOut.Invoke(this, "PythonFutureWarningException" + ": " + ex.Message, -1);
            else if (ex is IronPython.Runtime.Exceptions.PythonImportErrorException)
                ErrOut.Invoke(this, "PythonImportErrorException" + ": " + ex.Message, -1);
            else if (ex is IronPython.Runtime.Exceptions.PythonImportWarningException)
                ErrOut.Invoke(this, "PythonImportWarningException" + ": " + ex.Message, -1);
            else if (ex is IronPython.Runtime.Exceptions.PythonKeyboardInterruptException)
                ErrOut.Invoke(this, "PythonKeyboardInterruptException" + ": " + ex.Message, -1);
            else if (ex is IronPython.Runtime.Exceptions.PythonLookupErrorException)
                ErrOut.Invoke(this, "PythonLookupErrorException" + ": " + ex.Message, -1);
            else if (ex is IronPython.Runtime.Exceptions.PythonNameErrorException)
                ErrOut.Invoke(this, "PythonNameErrorException" + ": " + ex.Message, -1);
            else if (ex is IronPython.Runtime.Exceptions.PythonOSErrorException)
                ErrOut.Invoke(this, "PythonOSErrorException" + ": " + ex.Message, -1);
            else if (ex is IronPython.Runtime.Exceptions.PythonOverflowWarningException)
                ErrOut.Invoke(this, "PythonOverflowWarningException" + ": " + ex.Message, -1);
            else if (ex is IronPython.Runtime.Exceptions.PythonPendingDeprecationWarningException)
                ErrOut.Invoke(this, "PythonPendingDeprecationWarningException" + ": " + ex.Message, -1);
            else if (ex is IronPython.Runtime.Exceptions.PythonReferenceErrorException)
                ErrOut.Invoke(this, "PythonReferenceErrorException" + ": " + ex.Message, -1);
            else if (ex is IronPython.Runtime.Exceptions.PythonRuntimeErrorException)
                ErrOut.Invoke(this, "PythonRuntimeErrorException" + ": " + ex.Message, -1);
            else if (ex is IronPython.Runtime.Exceptions.PythonRuntimeWarningException)
                ErrOut.Invoke(this, "PythonRuntimeWarningException" + ": " + ex.Message, -1);
            else if (ex is IronPython.Runtime.Exceptions.PythonSyntaxErrorException)
                ErrOut.Invoke(this, "PythonSyntaxErrorException" + ": " + ex.Message, ((PythonSyntaxErrorException)ex).Line);
            else if (ex is IronPython.Runtime.Exceptions.PythonSyntaxWarningException)
                ErrOut.Invoke(this, "PythonSyntaxWarningException" + ": " + ex.Message, -1);
            else if (ex is IronPython.Runtime.Exceptions.PythonSystemExitException)
                ErrOut.Invoke(this, "PythonSystemExitException" + ": " + ex.Message, -1);
            else if (ex is IronPython.Runtime.Exceptions.PythonUnboundLocalErrorException)
                ErrOut.Invoke(this, "PythonUnboundLocalErrorException" + ": " + ex.Message, -1);
            else if (ex is IronPython.Runtime.Exceptions.PythonUnicodeErrorException)
                ErrOut.Invoke(this, "PythonUnicodeErrorException" + ": " + ex.Message, -1);
            else if (ex is IronPython.Runtime.Exceptions.PythonUnicodeTranslateErrorException)
                ErrOut.Invoke(this, "PythonUnicodeTranslateErrorException" + ": " + ex.Message, -1);
            else if (ex is IronPython.Runtime.Exceptions.PythonUserWarningException)
                ErrOut.Invoke(this, "PythonUserWarningException" + ": " + ex.Message, -1);
            else if (ex is IronPython.Runtime.Exceptions.PythonWarningException)
                ErrOut.Invoke(this, "PythonWarningException" + ": " + ex.Message, -1);
            else
                ErrOut.Invoke(this, ex.GetType().ToString() + ": " + ex.Message, -1);
        }
        #endregion
    }
}
