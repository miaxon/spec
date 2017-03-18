using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AdvancedDataGridView;
using libspec.ViewEvent;
using libspec.ViewItem;
using libspec.Objects;
namespace libspec
{
    public partial class SpecViewTree : UserControl
    {
        #region events
        public event EventHandler<NodeClickEventArgs> NodeClickEvent;
        public event EventHandler<ExpandEventArgs> ExpandEvent;
        public event EventHandler<ButtonActionEventArgs> ButtonActionEvent;
        #endregion
        private TreeGridNode m_nodeToFill;
        public SpecViewTree()
        {
            InitializeComponent();
            treeView.ImageList = Utils.ImageList;
            statusStrip.ImageList = Utils.ImageList;
        }

        private void tgView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            m_nodeToFill = treeView.CurrentNode;
            if (m_nodeToFill != null && m_nodeToFill.Nodes.Count == 0 && NodeClickEvent != null)
            {
                if (m_nodeToFill.Tag is BaseObject)
                    NodeClickEvent(this, new NodeClickEventArgs(m_nodeToFill.Tag as BaseObject));
                if (m_nodeToFill.Tag is PozObject)
                    ExpandEvent(this, new ExpandEventArgs(m_nodeToFill.Tag as PozObject));
            }
        }
        public void AddObject(BaseObject o)
        {
            TreeGridNode node = null;
            if (m_nodeToFill == null)
            { 
                node = treeView.Nodes.Add(o.obozn);
            }
            else
            {
                node = m_nodeToFill.Nodes.Add(o.obozn);
                m_nodeToFill.Expand();
            }
            node.Image = Utils.GetNodeImage(o);
            node.Cells[1].Value = o.naimen;
            if(o is DocObject)
                node.Cells[2].Value = (o as DocObject).num_kol;
            node.Cells[8].Value = o.descr;
            node.Tag = o;
        }
        public void FillTree(List<ProjectObject> list)
        {
            treeView.Nodes.Clear();
            foreach (ProjectObject o in list)
            {
                TreeGridNode node = treeView.Nodes.Add(o.obozn);
                node.Image = Utils.GetNodeImage(o);
                node.Cells[1].Value = o.naimen;
                node.Cells[8].Value = o.descr;
                node.Tag = o;
            }
            stlblNumChilds.Text = "элементов: " + list.Count;
        }
        internal void FillProject(List<GroupObject> list)
        {
            m_nodeToFill.Nodes.Clear();
            foreach (GroupObject o in list)
            {
                TreeGridNode node = m_nodeToFill.Nodes.Add(o.obozn);
                node.Image = Utils.GetNodeImage(o);
                node.Cells[1].Value = o.naimen;
                node.Cells[8].Value = o.descr;
                node.Tag = o;
            }
            m_nodeToFill.Expand();
            stlblNumChilds.Text = "элементов: " + list.Count;
        }
        internal void FillGroup(List<DocObject> list)
        {
            m_nodeToFill.Nodes.Clear();
            foreach (DocObject o in list)
            {
                TreeGridNode node = m_nodeToFill.Nodes.Add(o.obozn);
                node.Image = Utils.GetNodeImage(o);
                node.Cells[1].Value = o.naimen;
                node.Cells[2].Value = (o as DocObject).num_kol;
                node.Cells[8].Value = o.descr;
                node.Tag = o;
            }
            m_nodeToFill.Expand();
            stlblNumChilds.Text = "элементов: " + list.Count;
        }
        
        internal void FillPoz(List<PozObject> list)
        {
            m_nodeToFill.Nodes.Clear();
            foreach (PozObject o in list)
            {
                TreeGridNode node = m_nodeToFill.Nodes.Add(o.obozn);
                node.Image = Utils.GetPozImage(o.num_kod);
                node.Cells[0].ToolTipText = Utils.NumKodString(o.num_kod);
                node.Cells[1].Value = o.naimen;
                node.Cells[2].Value = o.num_kol;
                node.Cells[3].Value = o.gost;
                node.Cells[4].Value = o.marka;
                node.Cells[5].Value = o.kei;
                node.Cells[6].Value = o.num_kfr;
                node.Cells[7].Value = o.num_knr;
                node.Cells[8].Value = o.descr;
                node.Tag = o;
            }
            m_nodeToFill.Expand();
            stlblNumChilds.Text = "элементов: " + list.Count;
        }

        private void treeView_SelectionChanged(object sender, EventArgs e)
        {
            if (treeView.CurrentNode != null)
                stlblNumChilds.Text = "элементов: " + treeView.CurrentNode.Nodes.Count;
        }

        private void tbtnAddProject_Click(object sender, EventArgs e)
        {
            m_nodeToFill = null;
            if (ButtonActionEvent != null)
                ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.AddProject));
        }

        private void tbtnAddGroup_Click(object sender, EventArgs e)
        {
            m_nodeToFill = treeView.CurrentNode;
            if (m_nodeToFill != null && ButtonActionEvent != null)
                ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.AddGroup, treeView.CurrentNode.Tag as BaseObject));
        }
    }
}
