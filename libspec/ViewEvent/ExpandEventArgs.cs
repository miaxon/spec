using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using libspec.ViewItem;
using libspec.Objects;

namespace libspec.ViewEvent
{
    public class ExpandEventArgs : System.EventArgs
    {
        public readonly PozObject Object;
        public ExpandEventArgs(PozObject o)
        {
            Object = o;
        }
    }
}
