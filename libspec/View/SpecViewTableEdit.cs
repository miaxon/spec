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
                stlblNum.Text = "элементов: " + treeView.CurrentNode.Nodes.Count;
        }

        internal void FillTree(List<ProjectObject> m_projects)
        {
            
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
                    stlblNum.Text = "элементов: " + treeView.CurrentNode.Nodes.Count;
                }
                if (o is MidObject)
                {
                   
                }
            }

        }
        private void treeView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            m_cellCurrent = treeView.CurrentCell;
            m_oldValue = treeView.CurrentCell.Value;
            m_nodeCurrent = treeView.CurrentNode;
        }
        public void RollBack()
        {
            if (m_cellCurrent != null)
                m_cellCurrent.Value = m_oldValue;
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
        
        internal void RemoveNode(TreeGridNode target)
        {
            
        }

        internal void AddNode(BaseObject o)
        {
            throw new NotImplementedException();
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
                stlblNum.Text = "Найдено элементов: <недостаточное количество символов>";

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
                stlblNum.Text = "Найдено элементов: <недостаточное количество символов>";
        }
    }
}