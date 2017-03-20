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
using libspec.Dialogs;
namespace libspec
{
    public partial class SpecViewTree : UserControl
    {
        #region events
        public event EventHandler<NodeClickEventArgs> NodeClickEvent;
        public event EventHandler<ExpandEventArgs> ExpandEvent;
        public event EventHandler<ButtonActionEventArgs> ButtonActionEvent;
        public event EventHandler<SearchEventArgs> SearchEvent;
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

        public void FillTree(List<ProjectObject> list)
        {
            treeView.Nodes.Clear();
            foreach (ProjectObject o in list)
            {
                TreeGridNode node = treeView.Nodes.Add(o.obozn);
                UpdateNode(node, o);
            }
            stlblNumChilds.Text = "элементов: " + list.Count;
        }
        internal void FillProject(List<GroupObject> list)
        {
            m_nodeToFill.Nodes.Clear();
            foreach (GroupObject o in list)
            {
                TreeGridNode node = m_nodeToFill.Nodes.Add(o.obozn);
                UpdateNode(node, o);
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
                UpdateNode(node, o);
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
                UpdateNode(node, o);
            }
            m_nodeToFill.Expand();
            stlblNumChilds.Text = "элементов: " + list.Count;
        }
        public void UpdateNode(TreeGridNode node, object obj)
        {
            if (obj is ProjectObject)
            {
                ProjectObject o = obj as ProjectObject;
                node.Image = Utils.GetNodeImage(o);
                node.Cells[1].Value = o.naimen;
                node.Cells[8].Value = o.descr;
                node.Tag = o;                
            }
            if (obj is GroupObject)
            {
                GroupObject o = obj as GroupObject;
                node.Image = Utils.GetNodeImage(o);
                node.Cells[1].Value = o.naimen;
                node.Cells[8].Value = o.descr;
                node.Tag = o;
                if (o.status == Closed.Y)
                    node.DefaultCellStyle.ForeColor = Color.Gray;
                else
                    node.DefaultCellStyle.ForeColor = Color.Black;
            }
            if (obj is DocObject)
            {
                DocObject o = obj as DocObject;
                node.Image = Utils.GetNodeImage(o);
                node.Cells[1].Value = o.naimen;
                node.Cells[2].Value = o.num_kol;
                node.Cells[8].Value = o.descr;
                node.Tag = o;
                if (o.status == Closed.Y)
                    node.DefaultCellStyle.ForeColor = Color.Gray;
                else
                    node.DefaultCellStyle.ForeColor = Color.Black;
            }
            if (obj is PozObject)
            {
                PozObject o = obj as PozObject;
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
        }
        public void RemoveNode(TreeGridNode node)
        {
            if (node.Parent == null)
                treeView.Nodes.Remove(node);
            else
                node.Parent.Nodes.Remove(node);
        }
        public void AddNode(object obj)
        {
            if (m_nodeToFill == null)
            {
                ProjectObject o = obj as ProjectObject;
                TreeGridNode node = treeView.Nodes.Add(o.obozn);
                node.Image = Utils.GetNodeImage(o);
                node.Cells[1].Value = o.naimen;
                node.Cells[8].Value = o.descr;
                node.Tag = o;

            }
            else
            {
                if (obj is GroupObject)
                {
                    GroupObject o = obj as GroupObject;
                    TreeGridNode node = m_nodeToFill.Nodes.Add(o.obozn);
                    node.Image = Utils.GetNodeImage(o);
                    node.Cells[1].Value = o.naimen;
                    node.Cells[8].Value = o.descr;
                    node.Tag = o;
                }
                if (obj is DocObject)
                {
                    DocObject o = obj as DocObject;
                    TreeGridNode node = m_nodeToFill.Nodes.Add(o.obozn);
                    node.Image = Utils.GetNodeImage(o);
                    node.Cells[1].Value = o.naimen;
                    node.Cells[2].Value = o.num_kol;
                    node.Cells[8].Value = o.descr;
                    node.Tag = o;
                }
            }
        }
        private void treeView_SelectionChanged(object sender, EventArgs e)
        {
            if (treeView.CurrentNode != null)
                stlblNumChilds.Text = "элементов: " + treeView.CurrentNode.Nodes.Count;
        }

        private void treeView_KeyDown(object sender, KeyEventArgs e)
        {
            m_nodeToFill = treeView.CurrentNode;
            if (m_nodeToFill == null || ButtonActionEvent == null)
                return;
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyDelete, treeView.CurrentNode));
                    break;
                case Keys.Add:
                    if (m_nodeToFill.Level == 2 || m_nodeToFill.Level == 3)
                        ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyOpen, treeView.CurrentNode));
                    break;
                case Keys.Subtract:
                    if (m_nodeToFill.Level == 2 || m_nodeToFill.Level == 3)
                        ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyClose, treeView.CurrentNode));
                    break;
                case Keys.F3:
                    if (m_nodeToFill.Level <= 3)
                        ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyUpdate, treeView.CurrentNode));
                    break;
            }

        }

        private void tbtnSelectProject_Click(object sender, EventArgs e)
        {
            m_nodeToFill = null;
            if (ButtonActionEvent != null)
                ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.SelectProject));
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
            if (m_nodeToFill.Level != 1)
                return;
            if (m_nodeToFill != null && ButtonActionEvent != null)
                ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.AddGroup, treeView.CurrentNode));
        }

        private void tbtnAddDoc_Click(object sender, EventArgs e)
        {
            m_nodeToFill = treeView.CurrentNode;
            if (m_nodeToFill.Level != 2)
                return;
            if (m_nodeToFill != null && ButtonActionEvent != null)
                ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.AddDoc, treeView.CurrentNode));
        }

        private void tbtnDelObject_Click(object sender, EventArgs e)
        {
            m_nodeToFill = treeView.CurrentNode;
            if (m_nodeToFill != null && ButtonActionEvent != null)
                ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyDelete, treeView.CurrentNode));
        }

        private void tbtnClose_Click(object sender, EventArgs e)
        {
            m_nodeToFill = treeView.CurrentNode;
            if (m_nodeToFill == null || ButtonActionEvent == null)
                return;
            if (m_nodeToFill.Level == 2 || m_nodeToFill.Level == 3)
            {
                ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyClose, treeView.CurrentNode));
            }
        }

        private void tbtnOpen_Click(object sender, EventArgs e)
        {
            m_nodeToFill = treeView.CurrentNode;
            if (m_nodeToFill == null || ButtonActionEvent == null)
                return;
            if (m_nodeToFill.Level == 2 || m_nodeToFill.Level == 3)
            {
                ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyOpen, treeView.CurrentNode));
            }

        }

        private void tbntUpdate_Click(object sender, EventArgs e)
        {
            m_nodeToFill = treeView.CurrentNode;
            if (m_nodeToFill == null || ButtonActionEvent == null)
                return;
            if (m_nodeToFill.Level <= 3)
            {
                ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyUpdate, treeView.CurrentNode));
            }
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

        private void tbtnAddPoz_Click(object sender, EventArgs e)
        {
            m_nodeToFill = treeView.CurrentNode;
            if (m_nodeToFill == null)
                return;
            if (m_nodeToFill.Level == 3)
            {
                SearchPozDialog dlg = new SearchPozDialog(m_nodeToFill);
                dlg.SearchEvent += new EventHandler<SearchEventArgs>(dlg_SearchEvent);
                dlg.ExpandEvent += new EventHandler<ExpandEventArgs>(dlg_ExpandEvent);
                dlg.ShowDialog();
            }
        }
    }
}
