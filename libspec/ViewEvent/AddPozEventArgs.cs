using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libspec.View.Objects;

namespace libspec.View.ViewEvent
{
    public class AddPozEventArgs : System.EventArgs
    {
        public readonly PozObject poz;
        public readonly object target;

        public AddPozEventArgs(PozObject p, object d)
        {
            poz = p;
            target = d;
        }
    }
}
