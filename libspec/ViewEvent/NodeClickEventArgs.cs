using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using libspec.View.Objects;

namespace libspec.View.ViewEvent
{
    public class NodeClickEventArgs : System.EventArgs
    {
        public readonly BaseObject Object;       
        public NodeClickEventArgs(BaseObject o)
        {
            Object = o;
        }
    }
}
