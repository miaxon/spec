using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libspec.Objects;

namespace libspec.ViewEvent
{
    public class MoveDocEventArgs : System.EventArgs
    {
        public readonly DocObject doc;
        public readonly GroupObject grp;
        public MoveDocEventArgs(DocObject d, GroupObject g)
        {
            doc = d;
            grp = g;
        }
    }
}
