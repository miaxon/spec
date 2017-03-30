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
    public enum CPAction
    {
        Copy,
        Cut,
        None
    }
    public partial class SpecViewTree : UserControl
    {

        private TreeGridNode m_nodeAction;
        private CPAction m_action;
        private void EndAction()
        {
            stlblAction.Text = "";
            if (m_nodeAction != null)
                m_nodeAction.DefaultCellStyle.ForeColor = Color.Black;
            m_nodeAction = null;
            m_action = CPAction.None;
            tbtnCopy.Checked = false;
            tbtnCut.Checked = false;
        }
        private void Copy()
        {
            EndAction();
            m_nodeCurrent = treeView.CurrentNode;
            if (m_nodeCurrent == null)
                return;

            if (m_nodeCurrent.Level == 3 || m_nodeCurrent.Level == 4)
            {
                stlblAction.Text = "Копировать: " + treeView.CurrentNode.Cells[0].Value;
                tbtnCopy.Checked = true;
                m_nodeAction = m_nodeCurrent;
                m_action = CPAction.Copy;
            }

        }
        private void Cut()
        {
            EndAction();
            m_nodeCurrent = treeView.CurrentNode;
            if (m_nodeCurrent == null)
                return;

            if (m_nodeCurrent.Level == 3 || m_nodeCurrent.Level == 4)
            {
                stlblAction.Text = "Вырезать: " + treeView.CurrentNode.Cells[0].Value;
                tbtnCut.Checked = true;
                m_nodeAction = m_nodeCurrent;
                m_nodeAction.DefaultCellStyle.ForeColor = Color.Gray;
                m_action = CPAction.Cut;
            }

        }
        private void Paste()
        {
            m_nodeCurrent = treeView.CurrentNode;
            if (m_nodeCurrent == null)
                return;
            if (m_nodeAction == null)
                return;
            if ((m_nodeAction.Level - m_nodeCurrent.Level) > 1 || m_nodeAction.Level == m_nodeCurrent.Level)
                return;
            stlblAction.Text = "Вставить: " + m_nodeAction.Cells[0].Value;
            if (m_nodeCurrent.Level == 2 || m_nodeAction.Level == 3) //вставка документа в группу 
            {
                DocObject doc = m_nodeAction.Tag as DocObject;
                GroupObject grp = m_nodeCurrent.Tag as GroupObject;
                ProjectObject prj = m_nodeCurrent.Parent.Tag as ProjectObject;

                if (m_action == CPAction.Cut) // перемещение
                {
                    if (MoveDocEvent != null)
                    {
                        TreeGridNode parentNode = m_nodeAction.Parent;
                        if (!parentNode.Equals(m_nodeCurrent))
                        {
                            MoveDocEvent(this, new MoveDocEventArgs(doc, grp));
                            ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyUpdate, m_nodeCurrent.Tag));
                            parentNode.Nodes.Remove(m_nodeAction);
                        }
                    }
                }
                if (m_action == CPAction.Copy) // копирование
                {
                    if (AddDocEvent != null)
                    {
                        AddDocEvent(this, new AddDocEventArgs(doc, grp, prj));
                        ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyUpdate, m_nodeCurrent.Tag));
                    }
                }
            }
            if (m_nodeCurrent.Level == 3 || m_nodeAction.Level == 4) //вставка позиции в документ 
            {
                PozObject poz = m_nodeAction.Tag as PozObject;
                DocObject doc = m_nodeCurrent.Tag as DocObject;

                if (m_action == CPAction.Cut) // перемещение
                {
                    if (MovePozEvent != null)
                    {
                        TreeGridNode parentNode = m_nodeAction.Parent;
                        if (!parentNode.Equals(m_nodeCurrent))
                        {
                            MovePozEvent(this, new MovePozEventArgs(poz, doc));
                            ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyUpdate, m_nodeCurrent.Tag));
                            parentNode.Nodes.Remove(m_nodeAction);
                        }
                    }
                }
                if (m_action == CPAction.Copy) // копирование
                {
                    if (AddPozEvent != null)
                    {
                        PozObject o = poz.Clone();
                        AddPozEvent(this, new AddPozEventArgs(o, doc));
                        ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyUpdate, m_nodeCurrent.Tag));
                    }
                }

            }
            EndAction();

        }        
    }
}