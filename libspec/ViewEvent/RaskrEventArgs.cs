using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using libspec.ViewItem;
using libspec.Objects;

namespace libspec.ViewEvent
{
    public class RaskrEventArgs : System.EventArgs
    {
        public readonly PozObject Object;
        public RaskrEventArgs(PozObject o)
        {
            Object = o;
        }
    }
}
