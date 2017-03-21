using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libspec.Objects;

namespace libspec.ViewEvent
{
    public class MovePozEventArgs : System.EventArgs
    {
        public readonly PozObject poz;
        public readonly DocObject doc;
        public MovePozEventArgs(PozObject p, DocObject d)
        {
            poz = p;
            doc = d;
        }
    }
}
