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
        public readonly DocObject doc;

        public AddPozEventArgs(PozObject p, DocObject d)
        {
            poz = p;
            doc = d;
        }
    }
}
