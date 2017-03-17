using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using libspec.ViewItem;
using libspec.Objects;

namespace libspec.ViewEvent
{
    public class TreeNodeClickEventArgs : System.EventArgs
    {
        public readonly BaseObject Object;
        public TreeNodeClickEventArgs(TreeNode targetNode)
        {
            Object = (targetNode as BaseNode).Object;
        }
        public TreeNodeClickEventArgs(BaseObject o)
        {
            Object = o;
        }
    }
}
