using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libspec.View.ViewEvent
{
    public class FillBadEventArgs:System.EventArgs
    {
        public readonly int num_kod;
        public FillBadEventArgs(int kod)
        {
            num_kod = kod;
        }
    }
}
