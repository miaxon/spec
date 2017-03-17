using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using libspec.ViewItem;
using libspec.Objects;

namespace libspec.ViewEvent
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
