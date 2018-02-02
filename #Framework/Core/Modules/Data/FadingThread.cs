using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Finance.Framework.Core
{
    [DebuggerStepThrough]
    public class FadingThread
    {
        #region FadingType enum

        public enum FadingType
        {
            Red,
            Green,
            Blue,
            Yellow,
            Purple
        }

        #endregion

        private readonly Control _ctrl;
        private readonly FadingType _type;

        public FadingThread(Control ctrl, FadingType type)
        {
            _ctrl = ctrl;
            _type = type;
        }

        public void FadingThreadFunc(object o)
        {
            for (int c = 0; c < 255; c += 17)
            {
                int c1 = c;
                switch (_type)
                {
                    case FadingType.Red:
                        _ctrl.BackColor = Color.FromArgb(255 - c1, 0, 0);
                        break;
                    case FadingType.Blue:
                        _ctrl.BackColor = Color.FromArgb(0, 0, 255 - c1);
                        break;
                    case FadingType.Green:
                        _ctrl.BackColor = Color.FromArgb(0, 255 - c1, 0);
                        break;
                    case FadingType.Yellow:
                        _ctrl.BackColor = Color.FromArgb(0, 255 - c1, 255 - c1);
                        break;
                    case FadingType.Purple:
                        _ctrl.BackColor = Color.FromArgb(255 - c1, 255 - c1, 0);
                        break;
                }
                Thread.Sleep(10);
            }
        }
    }
}