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
        private object m_oldValue;
        private DataGridViewCell m_cellCurrent;
        private void treeView_SelectionChanged(object sender, EventArgs e)
        {
            if (treeView.CurrentNode != null)
                stlblNum.Text = "элементов: " + treeView.CurrentNode.Nodes.Count;
        }
        private void treeView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            m_nodeCurrent = treeView.CurrentNode;
            if (m_nodeCurrent != null && ExpandEvent != null)
            {
                ExpandEvent(this, new ExpandEventArgs(m_nodeCurrent.Tag as PozObject));
                stlblNum.Text = "элементов: " + treeView.CurrentNode.Nodes.Count;
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
    }
}