using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using libspec.View.Objects;

namespace libspec.View.ViewEvent
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
