using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using libspec.Objects;

namespace libspec.ViewItem
{
    public class PozViewItem:ListViewItem
    {
        private PozObject m_o;
        public PozObject Object { get { return m_o; } set { m_o = value; if (m_o != null) Update(); } }

        public PozViewItem(PozObject o, int n)
            : base(n.ToString())
        {
            m_o = o;            
            Update();
        }

        private void Update()
        {            
            SubItems.Add(new ListViewItem.ListViewSubItem(this, m_o.obozn));
            SubItems.Add(new ListViewItem.ListViewSubItem(this, m_o.gost));
            SubItems.Add(new ListViewItem.ListViewSubItem(this, m_o.naimen));
            SubItems.Add(new ListViewItem.ListViewSubItem(this, m_o.marka));
            SubItems.Add(new ListViewItem.ListViewSubItem(this, m_o.kei));
            SubItems.Add(new ListViewItem.ListViewSubItem(this, m_o.num_kol.ToString()));
            SubItems.Add(new ListViewItem.ListViewSubItem(this, m_o.num_kfr.ToString()));
            SubItems.Add(new ListViewItem.ListViewSubItem(this, m_o.num_knr.ToString()));
            ImageKey = Utils.GetPozImageKey(m_o.num_kod);            
        }
    }
}
