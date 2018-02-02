using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Finance.MarketAccess;
using ZedGraph;
using Finance.Framework.Core.Serialization;
using Finance.Framework.Types;
using System.ComponentModel;

namespace Finance.Framework.Core
{
    [ToolboxBitmap(typeof(EmbeddedResourceFinder), "Finance.Framework.Core.Resources.chart_curve.png")]
    public partial class MarketGraph : UserControl, IGfxModule
    {
        [Browsable(true), Description("ID of this Market Control."), Category("Myaki"), DefaultValue(null)]
        public Guid ID { get { return _ID; } set { _ID = value; } }

        [Browsable(false)]
        public SerializableDictionary<string, Color> Colors { get { return _dicColors; } }

        [Browsable(true), Description("Relative performance in %."), Category("Myaki"), DefaultValue(false)]
        public bool PerformanceMode
        {
            get { return mnuPerfMode.Checked; }
            set { mnuPerfMode_Click(null, null); }
        }

        [Browsable(true), Description("Show menubar."), Category("Myaki"), DefaultValue(true)]
        public bool ShowMenuBar { get { return toolMenu.Visible; } set { toolMenu.Visible = value; zedGraph.Location = toolMenu.Visible ? new Point(0, 0) : new Point(0, 25); } }

        [Browsable(true), Description("Colors for the lines."), Category("Myaki")]
        public Color[] DefaultColors = new Color[] { Color.Yellow, Color.Purple, Color.Green, Color.Pink, Color.PowderBlue };

        [Browsable(true), Description("Colors for the line 1."), Category("Myaki")]
        public Color Color_1 { get { return DefaultColors[0]; } set { DefaultColors[0] = value; } }

        [Browsable(true), Description("Colors for the line 2."), Category("Myaki")]
        public Color Color_2 { get { return DefaultColors[1]; } set { DefaultColors[1] = value; } }

        [Browsable(true), Description("Colors for the line 3."), Category("Myaki")]
        public Color Color_3 { get { return DefaultColors[2]; } set { DefaultColors[2] = value; } }

        [Browsable(true), Description("Colors for the line 4."), Category("Myaki")]
        public Color Color_4 { get { return DefaultColors[3]; } set { DefaultColors[3] = value; } }

        [Browsable(true), Description("Colors for the line 5."), Category("Myaki")]
        public Color Color_5 { get { return DefaultColors[4]; } set { DefaultColors[4] = value; } }

        [Browsable(false), Description("Graphic control."), Category("Myaki")]
        public ZedGraphControl Graph { get { return zedGraph; } }

        public event MouseEventHandler MoveRequest;
        public event UnlinkedEventHandler UnlinkedEvent;

        private Guid _ID;
        private bool isReset = true;
        private readonly GraphPane _gfxPane;
        private readonly IDictionary<Guid, Dictionary<string, LineItem>> _dicGuid = new Dictionary<Guid, Dictionary<string, LineItem>>();
        private readonly IDictionary<object, List<Guid>> _dicSources = new Dictionary<object, List<Guid>>();
        private readonly IDictionary<string, double> _dicPerfFirst = new Dictionary<string, double>();
        private readonly MarketCtrlHelper _helper;
        private readonly SerializableDictionary<string, Color> _dicColors;
        private Dictionary<string, LineItem> _dicLinesforGuid;
        private Dictionary<string, LineItem> _dicLinesforIdentifier = new Dictionary<string, LineItem>();

        #region Instance & Dispose
        public MarketGraph()
        {
            InitializeComponent();
            ID = Guid.NewGuid();
            _dicColors = new SerializableDictionary<string, Color>();
            _gfxPane = zedGraph.GraphPane;
            _helper = new MarketCtrlHelper(this, zedGraph, toolMenu);
            toolOut.MouseDown += _helper.StartDrag;
            _helper.MessageEvent += NewMessageEvent;
            _helper.UnlinkEvent += Helper_UnlinkedEvent;
            Disposed += MarketGraph_Disposed;
            Reset();
        }

        public MarketGraph(MWFBase mwf)
            : this()
        {
            ID = mwf.ID;
            MWFGraph mwfData = mwf as MWFGraph;
            Dictionary<string, int>.Enumerator e = mwfData.Colors.GetEnumerator();
            while (e.MoveNext())
                _dicColors.Add(e.Current.Key, Color.FromArgb(e.Current.Value));
            if (mwfData.PerformanceMode)
                mnuPerfMode.Checked = true;
            if (mwfData == null)
                throw new NotSupportedException();
        }

        void MarketGraph_Disposed(object sender, EventArgs e)
        {
            if (UnlinkedEvent != null)
                UnlinkedEvent.Invoke(this, this);
        }
        #endregion

        #region User Events
        private void mnuClear_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void mnuPerfMode_Click(object sender, EventArgs e)
        {
            if (PerformanceMode)
            {
                foreach (CurveItem curve in _gfxPane.CurveList)
                {
                    double first = _dicPerfFirst[curve.Label.Text];
                    for (int c = 0; c < curve.Points.Count; c++)
                        curve.Points[c].Y = first + (first * curve.Points[c].Y / 100);
                }
                _dicPerfFirst.Clear();
                mnuPerfMode.Checked = false;
            }
            else
            {
                foreach (CurveItem curve in _gfxPane.CurveList)
                {
                    double first = 0;
                    for (int c = 0; c < curve.Points.Count; c++)
                    {
                        if (c == 0)
                        {
                            _dicPerfFirst.Add(curve.Label.Text, curve.Points[c].Y);
                            first = curve.Points[c].Y;
                            curve.Points[c].Y = 0;
                        }
                        else
                            curve.Points[c].Y = (curve.Points[c].Y * 100 / first) - 100;
                    }
                }
                mnuPerfMode.Checked = true;
            }
            double max = _gfxPane.XAxis.Scale.Max;
            double min = _gfxPane.XAxis.Scale.Min;
            zedGraph.RestoreScale(_gfxPane);
            _gfxPane.XAxis.Scale.Max = max;
            _gfxPane.XAxis.Scale.Min = min;
            zedGraph.AxisChange();
            zedGraph.Refresh();
        }


        void Helper_UnlinkedEvent(object source, IGfxModule module)
        {
            RemoveModuleLines(module);
        }

        private void toolStrip_MouseDown(object sender, MouseEventArgs e)
        {
            if (MoveRequest != null)
                MoveRequest.Invoke(sender, e);
        }
        #endregion

        #region IGfxModule
        public void AddMessage(GfxMessage message)
        {
            NewMessageEvent(this, message, null);
        }

        public void AddMessages(IList<GfxMessage> messages)
        {
            NewMessageEvent(this, null, messages);
        }

        [Browsable(false)]
        public string MyakiName
        {
            get { return "Graph"; }
        }

        [Browsable(false)]
        public Size MyakiSize
        {
            get { return new Size(500, 240); }
        }

        [Browsable(false)]
        public GraphicBorderStyle MyakiBorderStyle
        {
            get { return GraphicBorderStyle.Sizable; }
        }

        [Browsable(false)]
        public IList<Guid> InputModules
        {
            get { return _helper.InputModules; }
        }

        public void AddInputModule(IGfxModule module)
        {
            _helper.InputModuleLink(module);
        }
        #endregion

        #region Message Handlers
        public event MessageEventHandler MessageEvent;

        void AddGuidToSourcesDictionary(IGfxModule module, Guid id)
        {
            if (_dicSources.ContainsKey(module))
            {
                if (!_dicSources[module].Contains(id))
                    _dicSources[module].Add(id);
            }
            else
            {
                List<Guid> lst = new List<Guid>();
                lst.Add(id);
                _dicSources.Add(module, lst);
            }
        }

        bool NewMessageEvent(object source, GfxMessage data)
        {
            bool hasChanged = false;
            // HISTORY
            if (data.ContentType == typeof(HistoryData))
            {
                HistoryData d = data.Content as HistoryData;
                if (d != null)
                {
                    AddGuidToSourcesDictionary(source as IGfxModule, d.Guid);
                    hasChanged = GfxHistoryMarketDataEvent(d);
                }
                else
                    throw new NullReferenceException("HistoryData is null!");
            }
            else if (data.ContentType == typeof(MultiHistoryData))
            {
                if (data.Content != null)
                    foreach (HistoryData d in (MultiHistoryData)data.Content)
                    {
                        AddGuidToSourcesDictionary(source as IGfxModule, d.Guid);
                        hasChanged = GfxHistoryMarketDataEvent(d);
                    }
                else
                    throw new NullReferenceException("Data is null!");
            }
            // STATIC / REALTIME
            else if (data.ContentType == typeof(Data))
            {
                Data d = data.Content as Data;
                if (d != null)
                {
                    AddGuidToSourcesDictionary(source as IGfxModule, d.Guid);
                    hasChanged = GfxRealtimeMarketDataEvent(d);
                }
                else
                    throw new NullReferenceException("Data is null!");
            }
            else if (data.ContentType == typeof(MultiData))
            {

                MultiData d = data.Content as MultiData;
                if (d != null)
                {
                    foreach (Data item in d)
                    {
                        AddGuidToSourcesDictionary(source as IGfxModule, item.Guid);
                        hasChanged |= GfxRealtimeMarketDataEvent(item);
                    }
                }
                else
                    throw new NullReferenceException("Data is null!");
            }
            if (MessageEvent != null && MessageEvent.GetInvocationList().Length > 0)
                MessageEvent.Invoke(source, data, null);
            return hasChanged;
        }

        void NewMessageEvent(object source, GfxMessage msg, IList<GfxMessage> lst)
        {
            if (InvokeRequired)
            {
                Invoke(new MessageEventHandler(NewMessageEvent), new object[] { source, msg, lst });
                return;
            }
            try
            {
                bool hasChanged = false;
                if (msg == null)
                    foreach (GfxMessage current in lst)
                        hasChanged = NewMessageEvent(source, current);
                else
                    hasChanged = NewMessageEvent(source, msg);
                if (hasChanged)
                    UpdateToScreen();
            }
            catch (Exception ex)
            {
                if (MessageEvent != null)
                    MessageEvent.Invoke(this, new GfxMessage(GfxType.Information, new MiscData(MiscEventType.Info, ex.Message, ex)), null);
            }
        }
        #endregion

        #region Graph Methods
        public void Reset()
        {
            if (zedGraph.BeenDisposed)
                return;
            while (_gfxPane.CurveList.Count > 0)
                _gfxPane.CurveList.RemoveAt(0);
            foreach (Dictionary<string, LineItem> current in _dicGuid.Values)
                current.Clear();
            mnuColors.DropDownItems.Clear();
            _dicPerfFirst.Clear();
            _dicLinesforIdentifier.Clear();
            if (_dicLinesforGuid != null)
                _dicLinesforGuid.Clear();
            _dicGuid.Clear();
            _dicSources.Clear();
            _gfxPane.Fill = new Fill(Color.Black);
            _gfxPane.Title.IsVisible = false;
            _gfxPane.XAxis.Type = AxisType.Date;
            _gfxPane.XAxis.Title.IsVisible = false;
            _gfxPane.YAxis.Title.IsVisible = false;
            _gfxPane.Chart.Fill = new Fill(Color.FromArgb(34, 50, 60), Color.FromArgb(0, 0, 0), 90F);
            _gfxPane.Legend.Fill = new Fill(Color.Black);
            _gfxPane.Legend.FontSpec.FontColor = Color.White;
            _gfxPane.XAxis.MajorGrid.DashOn = 1.0F;
            _gfxPane.YAxis.MajorGrid.DashOff = 0.25F;
            _gfxPane.XAxis.MajorGrid.IsVisible = true;
            _gfxPane.YAxis.MajorGrid.DashOn = 0.25F;
            _gfxPane.YAxis.MajorGrid.DashOff = 0.5F;
            _gfxPane.YAxis.MajorGrid.IsVisible = true;
            _gfxPane.XAxis.Color = Color.White;
            _gfxPane.YAxis.Color = Color.White;
            _gfxPane.XAxis.Scale.FontSpec.FontColor = Color.White;
            _gfxPane.YAxis.Scale.FontSpec.FontColor = Color.White;
            _gfxPane.XAxis.Title.FontSpec.FontColor = Color.White;
            _gfxPane.YAxis.Title.FontSpec.FontColor = Color.White;
            _gfxPane.Chart.Border.Color = Color.White;
            _gfxPane.XAxis.MajorGrid.Color = Color.Gray;
            _gfxPane.YAxis.MajorGrid.Color = Color.Gray;
            zedGraph.RestoreScale(_gfxPane);
            UpdateToScreen();
            isReset = true;
        }

        /// <summary>
        /// Checks whether the entry for this GUID exist
        /// </summary>
        /// <param name="id"></param>
        Dictionary<string, LineItem> GetDictionaryForGuid(Guid id)
        {
            Dictionary<string, LineItem> dicLinesforGuid;
            if (_dicGuid.ContainsKey(id))
                dicLinesforGuid = _dicGuid[id];
            else
            {
                dicLinesforGuid = new Dictionary<string, LineItem>();
                _dicGuid.Add(id, dicLinesforGuid);
            }
            return dicLinesforGuid;
        }

        /// <summary>
        /// Trims the curves.
        /// </summary>
        private void TrimCurves()
        {
            // Reduction algorithm
            double minDiff = (_gfxPane.XAxis.Scale.Max - _gfxPane.XAxis.Scale.Min) / 300;
            foreach (CurveItem curve in _gfxPane.CurveList)
            {
                if (curve.Points.Count < 300)
                    continue;
                double precOne = -1;
                for (int c = curve.Points.Count - 1; c > 0; c--)
                {
                    if (precOne == -1)
                    {
                        precOne = curve.Points[c].X;
                        continue;
                    }
                    if (precOne - curve.Points[c].X < minDiff)
                        curve.RemovePoint(c);
                    else
                        precOne = curve.Points[c].X;
                }
            }
        }

        bool GfxRealtimeMarketDataEvent(Data data)
        {
            XDate date = new XDate(data.Time);
            bool hasChanged = false;
            _dicLinesforGuid = GetDictionaryForGuid(data.Guid);
            if (isReset && _gfxPane.CurveList.Count > 0)
            {
                _gfxPane.XAxis.Scale.Max = double.MinValue;
                _gfxPane.XAxis.Scale.Min = double.MaxValue;
                foreach (LineItem curve in _gfxPane.CurveList)
                    for (int c = 0; c < curve.Points.Count; c++)
                    {
                        if (curve.Points[c].X > _gfxPane.XAxis.Scale.Max)
                            _gfxPane.XAxis.Scale.Max = curve.Points[c].X;
                        if (curve.Points[c].X < _gfxPane.XAxis.Scale.Min)
                            _gfxPane.XAxis.Scale.Min = curve.Points[c].X;
                    }

                isReset = false;
            }
            foreach (DataFieldItem fieldItem in data)
            {
                double value;
                if (fieldItem.Value is decimal)
                    value = Convert.ToDouble(fieldItem.Value);
                else
                    continue;
                if (date > _gfxPane.XAxis.Scale.Max)
                {
                    XDate newDate = new XDate(date);
                    newDate.AddDays((_gfxPane.XAxis.Scale.Max - _gfxPane.XAxis.Scale.Min) * 0.05);
                    _gfxPane.XAxis.Scale.Max = newDate;
                    TrimCurves();
                }
                if (date < _gfxPane.XAxis.Scale.Min)
                    _gfxPane.XAxis.Scale.Min = date;
                string identifier = data.Security + " [" + fieldItem.Field + "]";
                // Check whether the entry for this IDENTIFIER (EQUITY/FIELD) exist
                LineItem currentCurve = _dicLinesforIdentifier.ContainsKey(identifier) ? _dicLinesforIdentifier[identifier] : AddNewCurve(identifier, data.Security, fieldItem);
                if (!PerformanceMode)
                    currentCurve.AddPoint(date, value);
                else
                {
                    if (_dicPerfFirst.ContainsKey(identifier))
                    {
                        double val = _dicPerfFirst[identifier];
                        currentCurve.AddPoint(date, (value * 100 / val) - 100);
                    }
                    else
                    {
                        _dicPerfFirst.Add(identifier, value);
                        currentCurve.AddPoint(date, 0);
                    }

                }
                if (currentCurve.Points.Count > 300)
                    TrimCurves();
                hasChanged = true;
            }
            return hasChanged;
        }

        /// <summary>
        /// Adds the new curve.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <param name="security">The security.</param>
        /// <param name="fieldItem">The field item.</param>
        /// <returns></returns>
        LineItem AddNewCurve(string identifier, string security, DataFieldItem fieldItem)
        {
            LineItem currentCurve;
            try
            {
                if (_gfxPane.CurveList.Count < DefaultColors.Length)
                    currentCurve = _gfxPane.AddCurve(identifier, null,
                                                     DefaultColors[_gfxPane.CurveList.Count], SymbolType.None);
                else
                {
                    currentCurve = _gfxPane.AddCurve(identifier, null, Color.White);
                    _gfxPane.CurveList[0].MakeUnique(ColorSymbolRotator.StaticInstance);
                }
                currentCurve.Line.Width = 1.8F;
                currentCurve.Line.IsAntiAlias = true;
                _dicLinesforIdentifier.Add(identifier, currentCurve);
                _dicLinesforGuid.Add(identifier, currentCurve);
                if (_dicColors.ContainsKey(identifier))
                    currentCurve.Color = _dicColors[identifier];
                else
                    _dicColors.Add(identifier, currentCurve.Color);
                ToolStripItem item = mnuColors.DropDownItems.Add(identifier);
                item.Image = GenerateColorSquare(currentCurve.Color);
                item.Click += mnuColors_Click;
                return currentCurve;
            }
            catch (Exception ex)
            {
                if (_dicLinesforIdentifier.ContainsKey(identifier))
                    _dicLinesforIdentifier.Remove(identifier);
                if (_dicLinesforGuid.ContainsKey(identifier))
                    _dicLinesforGuid.Remove(identifier);
                MessageBox.Show("Error: " + ex.Message);
                throw;
            }
        }

        Image GenerateColorSquare(Color color)
        {
            Bitmap img = new Bitmap(16, 16);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(img);
            g.FillRectangle(new SolidBrush(color), 0, 0, 16, 16);
            return img;
        }

        void mnuColors_Click(object sender, EventArgs e)
        {
            ToolStripItem item = sender as ToolStripItem;
            if (item == null) return;
            CurveItem curve = _gfxPane.CurveList.Find(delegate(CurveItem x) { return x.Label.Text == item.Text; });
            if (curve == null) return;
            ColorDialog colorDial = new ColorDialog();
            colorDial.Color = curve.Color;
            if (colorDial.ShowDialog() == DialogResult.OK)
            {
                curve.Color = colorDial.Color;
                _dicColors[curve.Label.Text] = curve.Color;
                item.Image = GenerateColorSquare(colorDial.Color);
                zedGraph.Refresh();
            }
        }

        bool GfxHistoryMarketDataEvent(HistoryData data)
        {
            bool hasChanged = false;
            if (isReset)
            {
                _gfxPane.XAxis.Scale.Max = double.MinValue;
                _gfxPane.XAxis.Scale.Min = double.MaxValue;
                isReset = false;
            }
            foreach (HistoryDataItem dataItem in data)
            {
                XDate date = new XDate(dataItem.Time);
                if (date > _gfxPane.XAxis.Scale.Max)
                    _gfxPane.XAxis.Scale.Max = date;
                if (date < _gfxPane.XAxis.Scale.Min)
                    _gfxPane.XAxis.Scale.Min = date;
                _dicLinesforGuid = GetDictionaryForGuid(data.Guid);
                foreach (DataFieldItem fieldItem in dataItem)
                {
                    double value;
                    if (fieldItem.Value is decimal)
                        value = Convert.ToDouble(fieldItem.Value);
                    else
                        continue;

                    string identifier = data.Security + "|" + fieldItem.Field;
                    // Check whether the entry for this IDENTIFIER (EQUITY/FIELD) exist
                    LineItem currentCurve = _dicLinesforIdentifier.ContainsKey(identifier) ? _dicLinesforIdentifier[identifier] : AddNewCurve(identifier, data.Security, fieldItem);
                    if (!PerformanceMode)
                        currentCurve.AddPoint(date, value);
                    else
                    {
                        double val = _dicPerfFirst[identifier];
                        currentCurve.AddPoint(date, val + (val * value / 100));
                    }
                    hasChanged = true;
                }
            }
            return hasChanged;
        }

        private void UpdateToScreen()
        {
            zedGraph.AxisChange();
            zedGraph.Refresh();
        }

        /// <summary>
        /// Removes all lines for this module.
        /// </summary>
        /// <param name="module">The module.</param>
        void RemoveModuleLines(IGfxModule module)
        {
            if (!_dicSources.ContainsKey(module)) return;
            bool hasChanged = false;
            foreach (Guid id in _dicSources[module])
                if (_dicGuid.ContainsKey(id))
                {
                    foreach (LineItem currentLine in _dicGuid[id].Values)
                    {
                        hasChanged = true;
                        for (int c = mnuColors.DropDownItems.Count - 1; c >= 0; c--)
                        {
                            if (mnuColors.DropDownItems[c].Text == currentLine.Label.Text)
                            {
                                mnuColors.DropDownItems.Remove(mnuColors.DropDownItems[c]);
                                break;
                            }
                        }
                        if (_dicPerfFirst.ContainsKey(currentLine.Label.Text))
                            _dicPerfFirst.Remove(currentLine.Label.Text);
                        _dicColors.Remove(currentLine.Label.Text);
                        _dicLinesforIdentifier.Remove(currentLine.Label.Text);
                        _gfxPane.CurveList.Remove(currentLine);
                    }
                    _dicGuid.Remove(id);
                }
            _dicSources.Remove(module);
            if (_dicSources.Count == 0)
                Reset();
            else if (hasChanged)
                zedGraph.Refresh();
        }
        #endregion

        #region MWF Base
        public MWFBase GetMWF(string title, Size size, Point location, FormWindowState windowState)
        {
            return new MWFGraph(ID, title, size, location, windowState, this, _helper.InputModules);
        }
        #endregion
    }
}