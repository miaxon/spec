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
using libspec.View;

namespace libspec.View
{
    public partial class SpecViewTree : UserControl
    {
        private void tbtnSelectProject_Click(object sender, EventArgs e)
        {
            m_nodeCurrent = null;
            if (ButtonActionEvent != null)
                ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.SelectProject));
        }
        private void tbtnAddProject_Click(object sender, EventArgs e)
        {
            m_nodeCurrent = null;
            if (ButtonActionEvent != null)
                ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.AddProject));
        }
        private void tbtnAddGroup_Click(object sender, EventArgs e)
        {
            m_nodeCurrent = treeView.CurrentNode;
            if (m_nodeCurrent.Level != 1)
                return;
            if (m_nodeCurrent != null && ButtonActionEvent != null)
                ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.AddGroup, treeView.CurrentNode));
        }
        private void tbtnAddDoc_Click(object sender, EventArgs e)
        {
            m_nodeCurrent = treeView.CurrentNode;
            if (m_nodeCurrent.Level != 2)
                return;
            if (m_nodeCurrent != null && ButtonActionEvent != null)
                ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.AddDoc, treeView.CurrentNode));
        }
        private void tbtnDelObject_Click(object sender, EventArgs e)
        {
            m_nodeCurrent = treeView.CurrentNode;
            if (m_nodeCurrent != null && treeView.CurrentNode.Level < 5 && ButtonActionEvent != null)
                ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyDelete, treeView.CurrentNode));
        }
        private void tbtnClose_Click(object sender, EventArgs e)
        {
            m_nodeCurrent = treeView.CurrentNode;
            if (m_nodeCurrent == null || ButtonActionEvent == null)
                return;
            if (m_nodeCurrent.Level == 2 || m_nodeCurrent.Level == 3)
            {
                ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyClose, treeView.CurrentNode));
            }
        }
        private void tbtnOpen_Click(object sender, EventArgs e)
        {
            m_nodeCurrent = treeView.CurrentNode;
            if (m_nodeCurrent == null || ButtonActionEvent == null)
                return;
            if (m_nodeCurrent.Level == 2 || m_nodeCurrent.Level == 3)
            {
                ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyOpen, treeView.CurrentNode));
            }

        }
        private void tbntUpdate_Click(object sender, EventArgs e)
        {
            m_nodeCurrent = treeView.CurrentNode;
            if (m_nodeCurrent == null || ButtonActionEvent == null)
                return;
            if (m_nodeCurrent.Level <= 3)
            {
                ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyUpdate, treeView.CurrentNode));
            }
        }        
        private void tbtnAddPoz_Click(object sender, EventArgs e)
        {
            m_nodeCurrent = treeView.CurrentNode;
            if (m_nodeCurrent == null)
                return;
            if (m_nodeCurrent.Level == 3)
            {
                SearchPozDialog dlg = new SearchPozDialog(m_nodeCurrent);
                dlg.SearchEvent += new EventHandler<SearchEventArgs>(dlg_SearchEvent);
                dlg.ExpandEvent += new EventHandler<ExpandEventArgs>(dlg_ExpandEvent);
                dlg.AddPozEvent += new EventHandler<AddPozEventArgs>(dlg_AddPozEvent);
                dlg.NodeEditEvent += new EventHandler<NodeEditEventArgs>(dlg_NodeEditEvent);
                dlg.ShowDialog();
            }
        }
        private void tbtnEditTab_Click(object sender, EventArgs e)
        {
            string p = Directory.GetCurrentDirectory() + @"\spbase.exe";
            if (!File.Exists(p))
            {
                MessageBox.Show("Не найден файл программы расчета.");
                return;
            }
            Process.Start(p);
        }        
        
        void dlg_NodeEditEvent(object sender, NodeEditEventArgs e)
        {
            if (NodeEditEvent != null)
                NodeEditEvent(sender, e);
        }
        private void tbtnCalc_Click(object sender, EventArgs e)
        {
            string p = Directory.GetCurrentDirectory() + @"\transformer.exe";
            if (!File.Exists(p))
            {
                MessageBox.Show("Не найден файл программы расчета.");
                return;
            }
            m_nodeCurrent = treeView.CurrentNode;
            if (m_nodeCurrent == null)
                return;
            if (m_nodeCurrent.Level > 3) // расчет для документов, групп и проектов
                return;
            string cmdLine = "";
            BaseObject o = m_nodeCurrent.Tag as BaseObject;
            if (o is DocObject)
                cmdLine = "document " + o.id;
            if (o is GroupObject)
                cmdLine = "group " + o.id;
            if (o is ProjectObject)
                cmdLine = "project " + o.id;

            Process.Start(p, cmdLine);
        }
        private void tbtnCopy_Click(object sender, EventArgs e)
        {
            Copy();
        }
        private void tbtnCut_Click(object sender, EventArgs e)
        {
            Cut();
        }
        private void tbtnPaste_Click(object sender, EventArgs e)
        {
            Paste();
        }
        void dlg_SearchEvent(object sender, SearchEventArgs e)
        {
            if (SearchEvent != null)
            {
                SearchEvent(sender, e);
            }
        }
        void dlg_ExpandEvent(object sender, ExpandEventArgs e)
        {
            if (ExpandEvent != null)
            {
                ExpandEvent(sender, e);
            }
        }
        void dlg_AddPozEvent(object sender, AddPozEventArgs e)
        {
            if (AddPozEvent != null)
            {
                AddPozEvent(this, e);
                ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyUpdate, treeView.CurrentNode));
            }
        }
    }
}