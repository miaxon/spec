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
            {
                stlblNumChilds.Text = "элементов: " + treeView.CurrentNode.Nodes.Count;
                m_nodeCurrent = treeView.CurrentNode;
                if (dlg != null)
                {
                    if (treeView.CurrentNode.Level == 3) // no mid object
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
            Fill();
        }
        private void treeView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                Utils.Version();
            }
            m_nodeCurrent = treeView.CurrentNode;
            if (m_nodeCurrent == null || ButtonActionEvent == null)
                return;
            switch (e.KeyCode)
            {

                case Keys.Up:
                    {
                        if (e.Modifiers == Keys.Control)
                        {
                            if (m_nodeCurrent.Tag is BaseObject)
                            {
                                BaseObject o = m_nodeCurrent.Tag as BaseObject;
                                Utils.Info(o.id.ToString());
                                Clipboard.SetText(o.id.ToString());
                            }
                            if (m_nodeCurrent.Tag is PozObject)
                            {
                                PozObject o = m_nodeCurrent.Tag as PozObject;
                                Utils.Info(o.id.ToString());
                                Clipboard.SetText(o.id.ToString());
                            }
                        }
                    }
                    break;
                case Keys.Delete:
                    if (treeView.CurrentNode.Level < 5)
                        ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyDelete, m_nodeCurrent.Tag));
                    break;
                case Keys.Add:
                    if (m_nodeCurrent.Level == 2 || m_nodeCurrent.Level == 3)
                        ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyOpen, m_nodeCurrent.Tag));
                    break;
                case Keys.Subtract:
                    if (m_nodeCurrent.Level == 2 || m_nodeCurrent.Level == 3)
                        ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyClose, m_nodeCurrent.Tag));
                    break;
                case Keys.F3:
                    if (m_nodeCurrent.Level <= 3)
                        ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyUpdate, m_nodeCurrent.Tag));
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
                case Keys.Insert:
                    ShowAddPozDialog();
                    break;
            }

        }
        private void treeView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            m_cellCurrent = treeView.CurrentCell;
            m_oldValue = treeView.CurrentCell.Value;
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