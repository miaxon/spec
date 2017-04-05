using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using libspec.View.ViewEvent;
using AdvancedDataGridView;
using libspec.View.Objects;

namespace libspec.View.Dialogs
{
    public partial class SearchPozDialog : Form
    {
        #region events
        public event EventHandler<SearchEventArgs> SearchEvent;
        public event EventHandler<ExpandEventArgs> ExpandEvent;
        public event EventHandler<AddPozEventArgs> AddPozEvent;
        public event EventHandler<NodeEditEventArgs> NodeEditEvent;
        #endregion
        private int m_num_kod;
        private TreeGridNode m_nodeCurrent;
        private TreeGridNode m_targetNode;
        private ToolStripButton m_btnChecked;
        private object m_oldValue;
        private DataGridViewCell m_cellCurrent;
        public SearchPozDialog(TreeGridNode target)
        {
            InitializeComponent();
            tbtn_lid.Checked = true;
            m_btnChecked = tbtn_lid;
            m_num_kod = Convert.ToInt32(tbtn_lid.Tag);
            Text = "Поиск позиций: " + Utils.NumKodString(m_num_kod);            
            ttxtGost.Enabled = tbtnSearchGost.Enabled = false;            
            stlblAction.Alignment = ToolStripItemAlignment.Right;
            stlblAction.Text = "";
            SetEditObject(target);
        }
        public void SetEditObject(TreeGridNode target)
        {
            if (target != null)
            {
                m_targetNode = target;
                stlblEdit.Text = "Редактируется документ: " + m_targetNode.Cells[0].Value.ToString();
            }
        }
        public void EditResult(string text)
        {
            stlblEdit.Text = text;
        }
        public void RollBack()
        {
            if (m_cellCurrent != null)
                m_cellCurrent.Value = m_oldValue;
            stlblEdit.Text = "";
        }
        public void FillPoz(List<Objects.PozObject> list)
        {
            if (m_nodeCurrent != null)
            {
                m_nodeCurrent.Nodes.Clear();
                foreach (PozObject o in list)
                {
                    TreeGridNode node = m_nodeCurrent.Nodes.Add(o.obozn);
                    UpdateNode(o, node);
                }
                m_nodeCurrent.Expand();
            }
            else
            {
                treeView.Nodes.Clear();
                foreach (PozObject o in list)
                {
                    TreeGridNode node = treeView.Nodes.Add(o.obozn);
                    UpdateNode(o, node);
                }
                stlblAction.Text = "Найдено элементов: " + list.Count;
            }
        }
        public void UpdateNode(PozObject o, TreeGridNode node = null)
        {
            if (node == null)
            {
                node = m_nodeCurrent;
            }
            node.Image = Utils.GetPozImage(o.num_kod);
            node.Cells[0].ToolTipText = Utils.NumKodString(o.num_kod);
            node.Cells[1].Value = o.naimen;
            node.Cells[2].Value = o.num_kol;
            node.Cells[3].Value = o.gost;
            node.Cells[4].Value = o.marka;
            node.Cells[5].Value = o.kei;
            node.Cells[6].Value = o.num_kfr;
            node.Cells[7].Value = o.num_knr;
            node.Cells[8].Value = o.num_kod;
            node.Cells[9].Value = o.descr;
            node.Tag = o;
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
                m_nodeCurrent = null;
                SearchEvent(this, new SearchEventArgs("obozn", str, m_num_kod));
            }
            else
                stlblAction.Text = "Найдено элементов: <недостаточное количество символов>";
            
        }
        private void SearchGost()
        {
            string str = ttxtGost.Text;
            if (str.Length > 5)
            {
                m_nodeCurrent = null;
                SearchEvent(this, new SearchEventArgs("gost", str, m_num_kod));
            }
            else
                stlblAction.Text = "Найдено элементов: <недостаточное количество символов>";
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
            stlblAction.Text = "";
        }
        private void AddPoz()
        {
            m_nodeCurrent = treeView.CurrentNode;
            if (m_nodeCurrent != null && m_targetNode != null)
            {
                PozObject poz = m_nodeCurrent.Tag as PozObject;
                if (poz == null)
                    return;
                if (poz.num_kod != m_num_kod)
                    return;
                PozObject o = poz.Clone();
                if (AddPozEvent != null)
                    AddPozEvent(this, new AddPozEventArgs(o, m_targetNode.Tag));
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
        private void treeView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            m_cellCurrent = treeView.CurrentCell;
            m_oldValue = treeView.CurrentCell.Value;
            stlblEdit.Text = "";
        }
        private void treeView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            m_nodeCurrent = treeView.CurrentNode;
            if (m_nodeCurrent == null)
                return;
            object value = treeView.CurrentCell.Value;
            if (value.Equals(m_oldValue))
                return;
            string field = treeView.CurrentCell.OwningColumn.Name;
            if (NodeEditEvent != null)
                NodeEditEvent(this, new NodeEditEventArgs(m_nodeCurrent.Tag, field, value, m_oldValue));
        }
        private void treeView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            m_nodeCurrent = treeView.CurrentNode;
            if (m_nodeCurrent == null)
                return;
            ExpandEvent(this, new ExpandEventArgs(m_nodeCurrent.Tag as PozObject));
        }
    }
}
