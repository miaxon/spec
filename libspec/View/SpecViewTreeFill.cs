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
        private void Fill()
        {
            m_nodeCurrent = treeView.CurrentNode;
            if (m_nodeCurrent != null && m_nodeCurrent.Nodes.Count == 0 && NodeClickEvent != null)
            {
                if (m_nodeCurrent.Tag is BaseObject)
                    NodeClickEvent(this, new NodeClickEventArgs(m_nodeCurrent.Tag as BaseObject));
                if (m_nodeCurrent.Tag is PozObject)
                    ExpandEvent(this, new ExpandEventArgs(m_nodeCurrent.Tag as PozObject));
            }
        }     
        public void FillTree(List<ProjectObject> list)
        {
            treeView.Nodes.Clear();
            foreach (ProjectObject o in list)
            {
                TreeGridNode node = treeView.Nodes.Add(o.obozn);
                UpdateNode(o, node);
            }
            stlblNumChilds.Text = "элементов: " + list.Count;
        }
        public void FillProject(List<GroupObject> list)
        {
            m_nodeCurrent.Nodes.Clear();
            foreach (GroupObject o in list)
            {
                TreeGridNode node = m_nodeCurrent.Nodes.Add(o.obozn);
                UpdateNode(o, node);
            }
            m_nodeCurrent.Expand();
            stlblNumChilds.Text = "элементов: " + list.Count;
        }
        public void FillGroup(List<DocObject> list)
        {
            m_nodeCurrent.Nodes.Clear();
            foreach (DocObject o in list)
            {
                TreeGridNode node = m_nodeCurrent.Nodes.Add(o.obozn);
                UpdateNode(o, node);
            }
            m_nodeCurrent.Expand();
            stlblNumChilds.Text = "элементов: " + list.Count;
        }
        public void FillPoz(List<PozObject> list)
        {
            if (m_nodeCurrent == null)
                return;
            m_nodeCurrent.Nodes.Clear();
            foreach (PozObject o in list)
            {
                TreeGridNode node = m_nodeCurrent.Nodes.Add(o.obozn);
                UpdateNode(o, node);
            }
            m_nodeCurrent.Expand();
            stlblNumChilds.Text = "элементов: " + list.Count;
        }        
    }
}