using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using libspec.Objects;

namespace libspec.ViewItem
{
    public class DocNode : BaseNode
    {
        new public DocObject Object { get { return m_o as DocObject; } set { m_o = value; if (value != null) Update(); } }
        public DocNode(DocObject o) : base(o) { }

    }
}
