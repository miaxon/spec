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
using libspec.Objects;
using libspec.Dialogs;
using System.IO;
using System.Diagnostics;

namespace libspec
{
    public enum CPAction
    {
        Copy,
        Cut,
        None
    }
    public partial class SpecViewTree : UserControl
    {
        #region events
        public event EventHandler<NodeClickEventArgs> NodeClickEvent;
        public event EventHandler<ExpandEventArgs> ExpandEvent;
        public event EventHandler<ButtonActionEventArgs> ButtonActionEvent;
        public event EventHandler<SearchEventArgs> SearchEvent;
        public event EventHandler<AddPozEventArgs> AddPozEvent;
        public event EventHandler<MovePozEventArgs> MovePozEvent;
        public event EventHandler<AddDocEventArgs> AddDocEvent;
        public event EventHandler<MoveDocEventArgs> MoveDocEvent;
        #endregion
        private TreeGridNode m_nodeCurrent;
        private TreeGridNode m_nodeAction;
        private CPAction m_action;
        public SpecViewTree()
        {
            InitializeComponent();
            treeView.ImageList = Utils.ImageList;
            statusStrip.ImageList = Utils.ImageList;
            stlblAction.Text = "";
        }
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
        private void treeView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Fill();
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
            m_nodeCurrent.Nodes.Clear();
            foreach (GroupObject o in list)
            {
                TreeGridNode node = m_nodeCurrent.Nodes.Add(o.obozn);
                UpdateNode(node, o);
            }
            m_nodeCurrent.Expand();
            stlblNumChilds.Text = "элементов: " + list.Count;
        }
        internal void FillGroup(List<DocObject> list)
        {
            m_nodeCurrent.Nodes.Clear();
            foreach (DocObject o in list)
            {
                TreeGridNode node = m_nodeCurrent.Nodes.Add(o.obozn);
                UpdateNode(node, o);
            }
            m_nodeCurrent.Expand();
            stlblNumChilds.Text = "элементов: " + list.Count;
        }

        internal void FillPoz(List<PozObject> list)
        {
            m_nodeCurrent.Nodes.Clear();
            foreach (PozObject o in list)
            {
                TreeGridNode node = m_nodeCurrent.Nodes.Add(o.obozn);
                UpdateNode(node, o);
            }
            m_nodeCurrent.Expand();
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
            if (m_nodeCurrent == null)
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
                    TreeGridNode node = m_nodeCurrent.Nodes.Add(o.obozn);
                    node.Image = Utils.GetNodeImage(o);
                    node.Cells[1].Value = o.naimen;
                    node.Cells[8].Value = o.descr;
                    node.Tag = o;
                }
                if (obj is DocObject)
                {
                    DocObject o = obj as DocObject;
                    TreeGridNode node = m_nodeCurrent.Nodes.Add(o.obozn);
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
                dlg.ShowDialog();
            }
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


                if (m_action == CPAction.Cut) // перемещение
                {
                    if (MovePozEvent != null)
                    {
                        TreeGridNode parentNode = m_nodeAction.Parent;
                        if (!parentNode.Equals(m_nodeCurrent))
                        {
                            MoveDocEvent(this, new MoveDocEventArgs(doc, grp));
                            ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyUpdate, m_nodeCurrent));
                            parentNode.Nodes.Remove(m_nodeAction);
                        }
                    }
                }
                if (m_action == CPAction.Copy) // копирование
                {
                    if (AddPozEvent != null)
                    {
                        AddDocEvent(this, new AddDocEventArgs(doc, grp));
                        ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyUpdate, m_nodeCurrent));
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
                            ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyUpdate, m_nodeCurrent));
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
                        ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyUpdate, m_nodeCurrent));
                    }
                }

            }
            EndAction();

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


    }
}
