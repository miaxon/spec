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
        public SearchPozDialog()
        {
            InitializeComponent();
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
            string str = ttxtObozn.Text;
            if (str.Length > 5)
            {

                m_nodeToFill = null;
                SearchEvent(this, new SearchEventArgs("obozn", str, m_num_kod));
            }
        }

        private void tbtn_num_kod_Click(object sender, EventArgs e)
        {
            m_num_kod = Convert.ToInt32((sender as ToolStripButton).Tag);
            Text = "Поиск позиций: " + Utils.NumKodString(m_num_kod);
        }

        
    }
}
