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
using System.IO;
using System.Diagnostics;
using libspec.View.Dialogs;

namespace libspec.View
{
    public partial class SpecViewTable : UserControl
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

            if (m_nodeCurrent.Level == 2) // only root childs, no mid object
            {
                stlblAction.Text = "Копировать: " + treeView.CurrentNode.Cells[0].Value;
                tbtnCopy.Checked = true;
                m_nodeAction = m_nodeCurrent;
                m_action = CPAction.Copy;
            }
            if (m_nodeCurrent.Level == 1) // root (duplicate)
            {                
                if (AddPozEvent != null)
                {
                    AddPozEvent(this, new AddPozEventArgs(m_nodeCurrent.Tag, null));
                    ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyUpdate, m_nodeCurrent));
                }
            }

        }
        private void Cut()
        {
            EndAction();        
            m_nodeCurrent = treeView.CurrentNode;
            if (m_nodeCurrent == null)
                return;
            if (m_num_kod < 9) // no mid object
            {
                if (m_nodeCurrent.Level == 2)// only root childs
                {
                    stlblAction.Text = "Вырезать: " + treeView.CurrentNode.Cells[0].Value;
                    tbtnCut.Checked = true;
                    m_nodeAction = m_nodeCurrent;
                    m_nodeAction.DefaultCellStyle.ForeColor = Color.Gray;
                    m_action = CPAction.Cut;
                }
            }

        }
        private void Paste()
        {
            m_nodeCurrent = treeView.CurrentNode;
            if (m_nodeCurrent == null)
                return;
            if (m_nodeAction == null)
                return;           
            stlblAction.Text = "Вставить: " + m_nodeAction.Cells[0].Value;
            if (m_nodeCurrent.Level == 1) //вставка позиции в корневой документ 
            {
                PozObject src = m_nodeAction.Tag as PozObject;
                PozObject dst = m_nodeCurrent.Tag as PozObject;

                if (m_action == CPAction.Cut) // перемещение
                {
                    if (MovePozEvent != null)
                    {
                        TreeGridNode parentNode = m_nodeAction.Parent;
                        if (!parentNode.Equals(m_nodeCurrent))
                        {
                            MovePozEvent(this, new MovePozEventArgs(src, dst));
                            ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyUpdate, m_nodeCurrent));
                            parentNode.Nodes.Remove(m_nodeAction);
                        }
                    }
                }
                if (m_action == CPAction.Copy) // копирование
                {
                    if (AddPozEvent != null)
                    {
                        AddPozEvent(this, new AddPozEventArgs(src, dst));
                        ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyUpdate, m_nodeCurrent));
                    }
                }
            }
            EndAction();

        }
    }
}