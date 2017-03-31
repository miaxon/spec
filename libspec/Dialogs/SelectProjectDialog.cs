using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AdvancedDataGridView;
using libspec.View.Objects;
using libspec.View.ViewEvent;

namespace libspec.View.Dialogs
{
    public partial class SelectProjectDialog : Form
    {
        public event EventHandler<ButtonActionEventArgs> DelProjectEvent;
        private List<ProjectObject> m_list;
        public SelectProjectDialog(List<ProjectObject> list)
        {
            InitializeComponent();
            m_list = list;
            treeView.ImageList = Utils.ImageList;
            FillTree(m_list);
        }
        public List<ProjectObject> SelectedObjects
        {
            get
            {
                List<ProjectObject> list = new List<ProjectObject>();
                foreach (DataGridViewRow row in treeView.SelectedRows)
                {
                    ProjectObject o = row.Tag as ProjectObject;
                    list.Add(o);
                }
                return list;
            }
        }
        private void FillTree(List<ProjectObject> list)
        {
            treeView.Nodes.Clear();
            foreach (ProjectObject o in list)
            {
                TreeGridNode node = treeView.Nodes.Add(o.obozn);
                node.Image = Utils.GetNodeImage(o);
                node.Cells[1].Value = o.naimen;
                node.Cells[2].Value = o.descr;
                node.Tag = o;
            }
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string str = txtSearch.Text;
            if(string.IsNullOrEmpty(str))
            {
                FillTree(m_list);
                return;
            }
            List<ProjectObject> list = m_list.FindAll(o => o.obozn.StartsWith(str) || o.naimen.Contains(str));
            FillTree(list);
        }

        private void treeView_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    {
                        if (treeView.CurrentNode !=null && DelProjectEvent != null)
                            DelProjectEvent(this, new ButtonActionEventArgs(ButtonAction.KeyDelete, treeView.CurrentNode.Tag));
                    }
                    break;
                case Keys.Enter:
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                    break;            
            }
        }
        public void DeleteCurrentNode()
        {
            if (treeView.CurrentNode != null)
                treeView.Nodes.Remove(treeView.CurrentNode);
        }

        private void treeView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
