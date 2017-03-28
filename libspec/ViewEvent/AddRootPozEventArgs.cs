using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libspec.View.ViewEvent
{
    public class AddRootPozEventArgs : System.EventArgs
    {
        public readonly int num_kod;
        public AddRootPozEventArgs(int kod)
        {
            num_kod = kod;
        }
    }
}
