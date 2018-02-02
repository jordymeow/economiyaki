using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Finance.Framework;
using Finance.Framework.Core;
using Finance.Jumbler.Properties;

namespace Finance.Jumbler
{
    public partial class FrmContainer : Form
    {
        /// <summary>
        /// Gets the module.
        /// </summary>
        /// <value>The module.</value>
        public IGfxModule Module { get { return _module; } }

        private readonly IGfxModule _module;
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
        private const int WM_NCHITTEST = 0x0084;
        private const int HTTOP = 12;
        private const int HTRIGHT = 11;
        private const int HTLEFT = 10;
        private const int HTBOTTOM = 0x000F;
        private const int HTBOTTOMRIGHT = 17;
        private const int HTBOTTOMLEFT = 16;
        private const int HTTOPLEFT = 13;
        private const int HTTOPRIGHT = 14;
        private const int MOUSEAWARE_BORDERWIDTH = 8;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public FrmContainer(IGfxModule module)
        {
            _module = module;
            ((UserControl)module).Dock = DockStyle.Fill;
            InitializeComponent();
            if (_module is MarketData)
            {
                Controls.Remove(picGrow);
                lblTitle.Text = "Data";
            }
            else
                lblTitle.Text = module.MyakiName;
            Text = lblTitle.Text;
            pnlControlContainer.Controls.Add(((UserControl)module));
            Size = module.MyakiSize;
            Activated += FrmGfxContainer_Activated;
            Deactivate += FrmGfxContainer_Deactivate;
            lblTitle.MouseDown += lblTitle_MouseDown;
            SizeChanged += FrmGfxContainer_SizeChanged;
            FormClosing += FrmGfxContainer_FormClosing;
            module.MoveRequest += module_MoveRequest;
            ControlBox = false;
        }

        void module_MoveRequest(object sender, MouseEventArgs e)
        {
            lblTitle_MouseDown(sender, e);
        }

        void FrmGfxContainer_Deactivate(object sender, EventArgs e)
        {
            BackColor = Color.FromKnownColor(KnownColor.InactiveCaption);
            picClose.BackColor = Color.FromKnownColor(KnownColor.InactiveCaption);
            picGrow.BackColor = Color.FromKnownColor(KnownColor.InactiveCaption);
            lblTitle.BackColor = Color.FromKnownColor(KnownColor.InactiveCaption);
            lblTitle.ForeColor = Color.FromKnownColor(KnownColor.InactiveCaptionText);
        }

        void FrmGfxContainer_Activated(object sender, EventArgs e)
        {
            BackColor = Color.FromKnownColor(KnownColor.ActiveCaption);
            picClose.BackColor = Color.FromKnownColor(KnownColor.ActiveCaption);
            picGrow.BackColor = Color.FromKnownColor(KnownColor.ActiveCaption);
            lblTitle.BackColor = Color.FromKnownColor(KnownColor.ActiveCaption);
            lblTitle.ForeColor = Color.FromKnownColor(KnownColor.ActiveCaptionText);
        }

        void FrmGfxContainer_SizeChanged(object sender, EventArgs e)
        {
            ControlBox = false;
        }

        void lblTitle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 2 && e.Button == MouseButtons.Left)
                WindowState = FormWindowState.Minimized;
            else if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, (IntPtr)HT_CAPTION, (IntPtr)0);
            }
        }

        void FrmGfxContainer_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((UserControl)_module).Dispose();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            ((UserControl)_module).Visible = false;
            Close();
        }

        private void picClose_MouseEnter(object sender, EventArgs e)
        {
            picClose.Image = Resources.cross_hilight;
        }

        private void picClose_MouseLeave(object sender, EventArgs e)
        {
            picClose.Image = Resources.cross;
        }

        private void picGrow_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                picGrow.Image = Resources.magnifier_zoom_in;
                WindowState = FormWindowState.Normal;
            }
            else
            {
                picGrow.Image = Resources.magnifier_zoom_out;
                WindowState = FormWindowState.Maximized;
            }
        }

        [DebuggerStepThrough]
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (_module.MyakiBorderStyle != GraphicBorderStyle.Sizable || m.Msg != WM_NCHITTEST) return;
            int pos = m.LParam.ToInt32();
            short yPos = (short)(pos >> 16);
            short xPos = (short)pos;
            Point pt = PointToClient(new Point(xPos, yPos));
            if (pt.Y < MOUSEAWARE_BORDERWIDTH && pt.X < MOUSEAWARE_BORDERWIDTH)
                m.Result = new IntPtr(HTTOPLEFT);
            else if (pt.Y < MOUSEAWARE_BORDERWIDTH && pt.X > Width - MOUSEAWARE_BORDERWIDTH)
                m.Result = new IntPtr(HTTOPRIGHT);
            else if (pt.Y > Height - MOUSEAWARE_BORDERWIDTH && pt.X < MOUSEAWARE_BORDERWIDTH)
                m.Result = new IntPtr(HTBOTTOMLEFT);
            else if (pt.Y > Height - MOUSEAWARE_BORDERWIDTH && pt.X > Width - MOUSEAWARE_BORDERWIDTH)
                m.Result = new IntPtr(HTBOTTOMRIGHT);
            else if (pt.X < MOUSEAWARE_BORDERWIDTH)
                m.Result = new IntPtr(HTLEFT);
            else if (pt.X > Width - MOUSEAWARE_BORDERWIDTH)
                m.Result = new IntPtr(HTRIGHT);
            else if (pt.Y > Height - MOUSEAWARE_BORDERWIDTH)
                m.Result = new IntPtr(HTBOTTOM);
            else if (pt.Y < MOUSEAWARE_BORDERWIDTH)
                m.Result = new IntPtr(HTTOP);
        }

        private void picGrow_MouseEnter(object sender, EventArgs e)
        {
            switch (WindowState)
            {
                case FormWindowState.Normal:
                    picGrow.Image = Resources.magnifier_zoom_in_hilight;
                    break;
                case FormWindowState.Maximized:
                    picGrow.Image = Resources.magnifier_zoom_out_hilight;
                    break;
            }
        }

        private void picGrow_MouseLeave(object sender, EventArgs e)
        {
            switch (WindowState)
            {
                case FormWindowState.Normal:
                    picGrow.Image = Resources.magnifier_zoom_in;
                    break;
                case FormWindowState.Maximized:
                    picGrow.Image = Resources.magnifier_zoom_out;
                    break;
            }
        }
    }
}