using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libspec.View.Objects;

namespace libspec.View.ViewEvent
{
    public class ExpandMidEventArgs : System.EventArgs
    {
        public readonly MidObject Object;
        public readonly int num_kod;
        public ExpandMidEventArgs(MidObject o, int  kod)
        {
            Object = o;
            num_kod = kod;
        }
    }
}
