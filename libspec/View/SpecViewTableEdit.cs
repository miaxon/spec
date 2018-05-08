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

namespace libspec.View
{
    public partial class SpecViewTable : UserControl
    {
        private object m_oldValue;
        private DataGridViewCell m_cellCurrent;
        private int m_minChars;
        private void treeView_SelectionChanged(object sender, EventArgs e)
        {
            if (treeView.CurrentNode != null)
            {
                stlblEdit.Text = "элементов: " + treeView.CurrentNode.Nodes.Count;
                m_nodeCurrent = treeView.CurrentNode;
                if (dlg != null)
                {
                    if (m_nodeCurrent.Level == 1 && m_num_kod < 9) // no mid object
                    {
                        dlg.SetEditObject(treeView.CurrentNode);
                    }
                    else
                    {
                        dlg.SetEditObject(null);
                        //dlg.Close();
                    }
                }
            }

        }
        private void treeView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            m_nodeCurrent = treeView.CurrentNode;
            if (m_nodeCurrent != null)
            {
                object o = m_nodeCurrent.Tag;
                if (o is PozObject)
                {
                    ExpandEvent(this, new ExpandEventArgs(o as PozObject));
                    stlblEdit.Text = "элементов: " + treeView.CurrentNode.Nodes.Count;
                }
            }

        }
        private void treeView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (treeView.CurrentNode.Level > 2)
            {
                e.Cancel = true;
                return;
            }
            m_cellCurrent = treeView.CurrentCell;
            m_oldValue = treeView.CurrentCell.Value;
            m_nodeCurrent = treeView.CurrentNode;
            stlblAction.Text = "";
        }
        public void EditResult(string text)
        {
            stlblAction.Text = text;
        }
        public void RollBack()
        {
            if (m_cellCurrent != null)
                m_cellCurrent.Value = m_oldValue;
            stlblAction.Text = "";
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
            object parent = null;
            if (m_nodeCurrent.Parent != null && m_nodeCurrent.Parent.Tag != null)
                parent = m_nodeCurrent.Parent.Tag;
            if (NodeEditEvent != null)
                NodeEditEvent(this, new NodeEditEventArgs(m_nodeCurrent.Tag, field, value, m_oldValue, parent));
        }
        private void treeView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox tx = e.Control as TextBox;
            if (tx == null)
                return;
            if (treeView.CurrentCell.OwningColumn.Name.StartsWith("num_"))
            {
                if (tx.Tag == null)
                {
                    tx.KeyPress += new KeyPressEventHandler(tx_KeyPress);
                    tx.Tag = true;
                }
            }
            else
            {
                if (tx.Tag != null)
                {
                    tx.KeyPress -= new KeyPressEventHandler(tx_KeyPress);
                    tx.Tag = null;
                }
            }

        }
        private void tx_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
            }

        }
        internal void RemoveCurrentNode()
        {
            if (m_nodeCurrent == null)
                return;
            if (m_nodeCurrent.Parent == null)
                treeView.Nodes.Remove(m_nodeCurrent);
            else
                m_nodeCurrent.Parent.Nodes.Remove(m_nodeCurrent);
        }

        internal void AddRootNode(PozObject o)
        {
            TreeGridNode node = treeView.Nodes.Add(o.obozn);
            UpdatePozNode(o, node);
            node.Selected = true;
        }
        internal TreeGridNode AddMidNode(MidObject o)
        {
            TreeGridNode node = treeView.Nodes.Add(o.obozn);
            UpdateMidNode(o, node);
            node.Selected = true;
            return node;
        }
        internal void AddPozNode(PozObject o)
        {
            if (m_nodeCurrent == null)
                return;
            TreeGridNode node = m_nodeCurrent.Nodes.Add(o.obozn);
            UpdatePozNode(o, node);
            //node.Selected = true;
        }
        private void tbtnSearchGost_Click(object sender, EventArgs e)
        {
            SearchGost();
        }
        private void tbtnSearchObozn_Click(object sender, EventArgs e)
        {
            SearchObozn();
        }
        private void SearchObozn()
        {
            string str = ttxtObozn.Text;
            if (str.Length > m_minChars)
            {
                m_nodeCurrent = null;
                SearchEvent(this, new SearchEventArgs("obozn", str, m_num_kod));
            }
            else
                stlblEdit.Text = "Найдено элементов: <недостаточное количество символов>";

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
                stlblEdit.Text = "Найдено элементов: <недостаточное количество символов>";
        }
    }
}