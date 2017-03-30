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
        #region events
        public event EventHandler<ExpandEventArgs> ExpandEvent;
        public event EventHandler<ButtonActionEventArgs> ButtonActionEvent;
        public event EventHandler<SearchEventArgs> SearchEvent;
        public event EventHandler<AddPozEventArgs> AddPozEvent;
        public event EventHandler<MovePozEventArgs> MovePozEvent;
        public event EventHandler<NodeEditEventArgs> NodeEditEvent;
        public event EventHandler<AddRootPozEventArgs> AddRootPozEvent;
        public event EventHandler<FillBadEventArgs> FillBadEvent;
        #endregion
        private int m_num_kod;
        private TreeGridNode m_nodeCurrent;
        private ToolStripButton m_btnChecked;
        public SpecViewTable()
        {
            InitializeComponent();
            tbtn_lid.Checked = true;
            m_btnChecked = tbtn_lid;
            m_num_kod = Convert.ToInt32(tbtn_lid.Tag);
            ttxtGost.Enabled = tbtnSearchGost.Enabled = false;
            stlblEdit.Text = "";
            stlblAction.Alignment = ToolStripItemAlignment.Right;
            stlblAction.Text = "";
        }
        public void FillMid(List<MidObject> list)
        {
            if (m_nodeCurrent == null)
            {
                treeView.Nodes.Clear();
                foreach (MidObject o in list)
                {
                    TreeGridNode node = treeView.Nodes.Add(o.obozn);
                    UpdateMidNode(o, node);
                }
                stlblAction.Text = "Найдено элементов: " + list.Count;
            }
            else
            {
                m_nodeCurrent.Nodes.Clear();
                foreach (MidObject o in list)
                {
                    TreeGridNode node = m_nodeCurrent.Nodes.Add(o.obozn);
                    //UpdateNode(o, node);
                }
                stlblAction.Text = "Найдено элементов: " + list.Count;
            }
        }
        public void UpdateMidNode(object obj, TreeGridNode node = null)
        {
            if (node == null)
            {
                node = m_nodeCurrent;
            }
            if (obj is MidObject)
            {
                MidObject o = obj as MidObject;
                node.Image = Utils.GetMidImage(o);
                node.Cells[1].Value = o.naimen;
                node.Cells[2].Value = o.num_kol;
                node.Cells[9].Value = o.descr;
                node.Tag = o;
                node.Cells[0].ReadOnly = false;
                node.Cells[1].ReadOnly = false;
                node.Cells[2].ReadOnly = true;
                node.Cells[6].ReadOnly = true;
            }
        }
        public void FillPoz(List<Objects.PozObject> list)
        {
            if (m_nodeCurrent != null)
            {
                m_nodeCurrent.Nodes.Clear();
                foreach (PozObject o in list)
                {
                    TreeGridNode node = m_nodeCurrent.Nodes.Add(o.obozn);
                    UpdatePozNode(o, node);
                }
                m_nodeCurrent.Expand();
            }
            else
            {
                treeView.Nodes.Clear();
                foreach (PozObject o in list)
                {
                    TreeGridNode node = treeView.Nodes.Add(o.obozn);
                    UpdatePozNode(o, node);
                }
                stlblAction.Text = "Найдено элементов: " + list.Count;
            }
        }
        public void UpdatePozNode(PozObject o, TreeGridNode node = null)
        {
            if (node == null)
            {
                node = m_nodeCurrent;
            }
            node.Image = Utils.GetPozImage(o.num_kod);
            node.Cells[0].ToolTipText = Utils.NumKodString(o.num_kod);
            node.Cells[1].Value = o.naimen;
            node.Cells[2].Value = o.num_kol;
            node.Cells[3].Value = o.gost;
            node.Cells[4].Value = o.marka;
            node.Cells[5].Value = o.kei;
            node.Cells[6].Value = o.num_kfr;
            node.Cells[7].Value = o.num_knr;
            node.Cells[8].Value = o.num_kod;
            node.Cells[9].Value = o.descr;
            node.Tag = o;
        }
        private void tbtn_num_kod_Click(object sender, EventArgs e)
        {
            ToolStripButton btn = sender as ToolStripButton;
            if (btn.CheckState == CheckState.Checked)
                return;
            Clear();
            m_nodeCurrent = null;
            m_minChars = 5;
            btn.Checked = true;
            m_btnChecked.Checked = false;
            m_btnChecked = btn;
            m_num_kod = Convert.ToInt32(btn.Tag);
            (this.Parent as Form).Text = "Редактирование таблицы: " + Utils.NumKodString(m_num_kod);
            ttxtGost.Enabled = tbtnSearchGost.Enabled = btn.Equals(tbtn_mid3) || btn.Equals(tbtn_mid2);
            // correct view columns
            if (m_num_kod > 90)
                for (int i = 2; i < 9; i++)
                    treeView.Columns[i].Visible = false;
            if (m_num_kod == 94) //mid0            {
            {
                m_minChars = -1;
                treeView.Columns[2].Visible = true;
                SearchObozn();
                return;
            }
            if (m_num_kod == 93) // mid1
            {
                m_minChars = 2;
                treeView.Columns[2].Visible = true;
                return;
            }
            if (m_num_kod == 92) // mid2
            {
                treeView.Columns[3].Visible = true;
                treeView.Columns[2].Visible = true;
                return;
            }
            if (m_num_kod == 9) // mid3
            {
                treeView.Columns[0].Visible = true;
                treeView.Columns[1].Visible = true;
                treeView.Columns[2].Visible = false;
                treeView.Columns[3].Visible = true;
                treeView.Columns[4].Visible = true;
                treeView.Columns[5].Visible = true;
                treeView.Columns[6].Visible = true;
                treeView.Columns[7].Visible = false;
                treeView.Columns[8].Visible = true;

                return;
            }

            for (int i = 0; i < 10; i++)
                treeView.Columns[i].Visible = true;

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
                case Keys.Z:
                    if (e.Modifiers == Keys.Control)
                        Clear();
                    break;
                case Keys.F3:
                    if (m_nodeCurrent.Level < 3)
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
                case Keys.Delete:
                    if (m_nodeCurrent.Level < 3)
                        ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyDelete, m_nodeCurrent.Tag));
                    break;
            }
        }
        private void Clear()
        {
            treeView.Nodes.Clear();
            ttxtGost.Text = "";
            ttxtObozn.Text = "";
            stlblAction.Text = "";
            stlblEdit.Text = "";
            EndAction();
        }

        private void tbtnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        private void ttxtObozn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SearchObozn();
        }
        private void ttxtGost_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SearchGost();
        }
        private void tbtnEdit_Click(object sender, EventArgs e)
        {
            string p = Directory.GetCurrentDirectory() + @"\spform.exe";
            if (!File.Exists(p))
            {
                Utils.Error("Не найден файл программы заполнения спецификаций.");
                return;
            }
            Process.Start(p);
        }
        private void tbtnDelete_Click(object sender, EventArgs e)
        {
            m_nodeCurrent = treeView.CurrentNode;
            if (m_nodeCurrent != null && m_nodeCurrent.Level < 3 && ButtonActionEvent != null)
            {
                if (m_nodeCurrent.Level == 2)
                    ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyDelete, m_nodeCurrent.Tag, m_num_kod));
                if (m_nodeCurrent.Level == 1) // root
                    ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyDelete, m_nodeCurrent.Tag, -1));
            }
        }
        private void tbtnAddPoz_Click(object sender, EventArgs e)
        {
            m_nodeCurrent = treeView.CurrentNode;
            if (m_nodeCurrent == null)
                return;
            if (m_nodeCurrent.Level > 2)
                return;
            if (m_nodeCurrent.Tag is PozObject)
            {
                PozObject o = m_nodeCurrent.Tag as PozObject;
                if (m_num_kod < 9) // no mid object
                {
                    SearchPozDialog dlg = new SearchPozDialog(m_nodeCurrent);
                    dlg.SearchEvent += new EventHandler<SearchEventArgs>(dlg_SearchEvent);
                    dlg.ExpandEvent += new EventHandler<ExpandEventArgs>(dlg_ExpandEvent);
                    dlg.AddPozEvent += new EventHandler<AddPozEventArgs>(dlg_AddPozEvent);
                    dlg.NodeEditEvent += new EventHandler<NodeEditEventArgs>(dlg_NodeEditEvent);
                    dlg.ShowDialog();
                }
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
                ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyUpdate, m_nodeCurrent.Tag));
            }
        }
        void dlg_NodeEditEvent(object sender, NodeEditEventArgs e)
        {
            if (NodeEditEvent != null)
                NodeEditEvent(sender, e);
        }
        private void tbtnAdd_Click(object sender, EventArgs e)
        {
            if (AddRootPozEvent != null)
                AddRootPozEvent(this, new AddRootPozEventArgs(m_num_kod));
        }
        private void SpecViewTable_Load(object sender, EventArgs e)
        {
            (this.Parent as Form).Text = "Редактирование таблицы: " + Utils.NumKodString(m_num_kod);
        }
        private void tbtnUpdate_Click(object sender, EventArgs e)
        {
            m_nodeCurrent = treeView.CurrentNode;
            if (m_nodeCurrent == null || ButtonActionEvent == null)
                return;
            ButtonActionEvent(this, new ButtonActionEventArgs(ButtonAction.KeyUpdate, m_nodeCurrent.Tag));
        }
        private void tbtnPaste_Click(object sender, EventArgs e)
        {
            Paste();
        }
        private void tbtnCut_Click(object sender, EventArgs e)
        {
            Cut();
        }
        private void tbtnCopy_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void tbtnBadShow_Click(object sender, EventArgs e)
        {
            if (FillBadEvent != null)
                FillBadEvent(this, new FillBadEventArgs(m_num_kod));
        }
    }
}
