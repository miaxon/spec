using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using libspec.ViewEvent;
using AdvancedDataGridView;
using libspec.Objects;

namespace libspec.Dialogs
{
    public partial class SearchPozDialog : Form
    {
        #region events
        public event EventHandler<SearchEventArgs> SearchEvent;
        public event EventHandler<ExpandEventArgs> ExpandEvent;
        #endregion
        private int m_num_kod;
        private TreeGridNode m_nodeToFill;
        private TreeGridNode m_nodeDoc;
        private ToolStripButton m_btnChecked;
        public SearchPozDialog(TreeGridNode nodeDoc)
        {
            InitializeComponent();
            tbtn_lid.Checked = true;
            m_btnChecked = tbtn_lid;
            m_num_kod = Convert.ToInt32(tbtn_lid.Tag);
            Text = "Поиск позиций: " + Utils.NumKodString(m_num_kod);
            m_nodeDoc = nodeDoc;
            ttxtGost.Enabled = tbtnSearchGost.Enabled = false;
        }


        public void FillPoz(List<Objects.PozObject> list)
        {
            if (m_nodeToFill != null)
            {
                m_nodeToFill.Nodes.Clear();
                foreach (PozObject o in list)
                {
                    TreeGridNode node = m_nodeToFill.Nodes.Add(o.obozn);
                    UpdateNode(node, o);
                }
                m_nodeToFill.Expand();
            }
            else
            {
                treeView.Nodes.Clear();
                foreach (PozObject o in list)
                {
                    TreeGridNode node = treeView.Nodes.Add(o.obozn);
                    UpdateNode(node, o);
                }
            }
        }

        private void UpdateNode(TreeGridNode node, PozObject o)
        {
            node.Image = Utils.GetPozImage(o.num_kod);
            node.Cells[0].ToolTipText = Utils.NumKodString(o.num_kod);
            node.Cells[1].Value = o.naimen;
            node.Cells[2].Value = o.num_kol;
            node.Cells[3].Value = o.gost;
            node.Cells[4].Value = o.marka;
            node.Cells[5].Value = o.kei;
            node.Cells[6].Value = o.num_kfr;
            node.Cells[7].Value = o.num_knr;
            node.Cells[8].Value = o.descr;
            node.Tag = o;
        }


        private void treeView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            m_nodeToFill = treeView.CurrentNode;
            if (m_nodeToFill != null && ExpandEvent != null)
            {
                ExpandEvent(this, new ExpandEventArgs(m_nodeToFill.Tag as PozObject));
            }

        }

        private void tbtnSearchObozn_Click(object sender, EventArgs e)
        {
            SearchObozn();
        }

        private void SearchObozn()
        {
            string str = ttxtObozn.Text;
            if (str.Length > 5)
            {

                m_nodeToFill = null;
                SearchEvent(this, new SearchEventArgs("obozn", str, m_num_kod));
            }
        }
        private void SearchGost()
        {
            string str = ttxtGost.Text;
            if (str.Length > 5)
            {

                m_nodeToFill = null;
                SearchEvent(this, new SearchEventArgs("gost", str, m_num_kod));
            }
        }
        private void tbtn_num_kod_Click(object sender, EventArgs e)
        {
            ToolStripButton btn = sender as ToolStripButton;
            if (btn.CheckState == CheckState.Checked)
                return;
            Clear();
            btn.Checked = true;
            m_btnChecked.Checked = false;
            m_btnChecked = btn;
            m_num_kod = Convert.ToInt32(btn.Tag);
            Text = "Поиск позиций: " + Utils.NumKodString(m_num_kod);
            ttxtGost.Enabled = tbtnSearchGost.Enabled = btn.Equals(tbtn_mid);

        }

        private void treeView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Space)
            {
                AddPoz();
            }
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Z)
            {
                Clear();
            }
        }
        private void Clear()
        {
            treeView.Nodes.Clear();
            ttxtGost.Text = "";
            ttxtObozn.Text = "";
        }
        private void AddPoz()
        {
            m_nodeToFill = treeView.CurrentNode;
            if (m_nodeToFill != null && m_nodeDoc != null)
            {
                PozObject poz = m_nodeToFill.Tag as PozObject;
                if (poz.num_kod != m_num_kod)
                    return;
                DocObject doc = m_nodeDoc.Tag as DocObject;
                PozObject o = poz.Clone(doc.id);
                TreeGridNode node = m_nodeDoc.Nodes.Add(o.obozn);
                UpdateNode(node, o);
            }
        }

        private void tbtnIsert_Click(object sender, EventArgs e)
        {
            AddPoz();
        }

        private void tbtnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void ttxtObozn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SearchObozn();
        }

        private void ttxtGost_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SearchGost();
        }

        private void tbtnSearchGost_Click(object sender, EventArgs e)
        {
            SearchGost();
        }
    }
}
