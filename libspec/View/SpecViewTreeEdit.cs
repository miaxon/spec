using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AdvancedDataGridView;
using libspec.View.ViewEvent;
using libspec.View.Objects;
using libspec.View.Dialogs;
using System.IO;
using System.Diagnostics;

namespace libspec.View
{
    public partial class SpecViewTree : UserControl
    {
        private object m_oldValue;
        private DataGridViewCell m_cellCurrent;
        private void treeView_SelectionChanged(object sender, EventArgs e)
        {
            if (treeView.CurrentNode != null)
                stlblNumChilds.Text = "элементов: " + treeView.CurrentNode.Nodes.Count;
        }        
        private void treeView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Fill();
        }
        private void treeView_KeyDown(object sender, KeyEventArgs e)
        {
            m_nodeCurrent = treeView.CurrentNode;
            if (m_nodeCurrent == null || ButtonActionEvent == null)
                return;
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    if (treeView.CurrentNode.Level < 5)
                        ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyDelete, treeView.CurrentNode));
                    break;
                case Keys.Add:
                    if (m_nodeCurrent.Level == 2 || m_nodeCurrent.Level == 3)
                        ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyOpen, treeView.CurrentNode));
                    break;
                case Keys.Subtract:
                    if (m_nodeCurrent.Level == 2 || m_nodeCurrent.Level == 3)
                        ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyClose, treeView.CurrentNode));
                    break;
                case Keys.F3:
                    if (m_nodeCurrent.Level <= 3)
                        ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyUpdate, treeView.CurrentNode));
                    break;
                case Keys.Escape:
                    EndAction();
                    break;
                case Keys.C:
                    if (e.Modifiers == Keys.Control)
                        Copy();
                    break;
                case Keys.X:
                    if (e.Modifiers == Keys.Control)
                        Cut();
                    break;
                case Keys.V:
                    if (e.Modifiers == Keys.Control)
                        Paste();
                    break;
            }

        }
        private void treeView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            m_cellCurrent = treeView.CurrentCell;
            m_oldValue = treeView.CurrentCell.Value;
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