using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace libspec.Controls
{
    public class TreeViewFlickerFree : TreeView
    {
        private const int WM_PRINTCLIENT = 0x0318;
        private const int PRF_CLIENT = 4;

        public TreeViewFlickerFree()
            : base()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint))
            {
                Message m = new Message();
                m.HWnd = Handle;
                m.Msg = WM_PRINTCLIENT;
                m.WParam = e.Graphics.GetHdc();
                m.LParam = (IntPtr)PRF_CLIENT;
                DefWndProc(ref m);
                e.Graphics.ReleaseHdc(m.WParam);
            }
            base.OnPaint(e);
        }
    }
}
