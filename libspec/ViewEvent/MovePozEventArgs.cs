using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libspec.View.Objects;

namespace libspec.View.ViewEvent
{
    public class MovePozEventArgs : System.EventArgs
    {
        public readonly PozObject src;
        public readonly object dst;
        public MovePozEventArgs(PozObject src, object dst)
        {
            this.src = src;
            this.dst = dst;
        }
    }
}
