using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using libspec.Objects;

namespace libspec.ViewItem
{
    public class BaseNode : TreeNode
    {
        protected BaseObject m_o;
        public BaseObject Object { get { return m_o; } set { m_o = value; if (value != null) Update(); } }
        public BaseNode(BaseObject o)
            : base()
        {
            m_o = o;
            Update();
        }
        protected void Update()
        {
            Text = m_o.Text;
            ImageKey = SelectedImageKey = Utils.GetNodeImageKey(m_o);
        }
    }
}
