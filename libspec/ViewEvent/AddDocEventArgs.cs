using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libspec.View.Objects;

namespace libspec.View.ViewEvent
{
    public class AddDocEventArgs : System.EventArgs
    {
        public readonly DocObject doc;
        public readonly GroupObject grp;
        public AddDocEventArgs(DocObject d, GroupObject g)
        {
            doc = d;
            grp = g;
        }
    }
}
