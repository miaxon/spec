using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using libspec.View.Objects;
using libspec.View.ViewEvent;
using AdvancedDataGridView;

namespace libspec.View.Dialogs
{
    public partial class SelectMidParentDialog : Form
    {
        public event EventHandler<SearchEventArgs> SearchEvent;
        private int m_num_kod;
        public SelectMidParentDialog(int num_kod, string obozn)
        {
            InitializeComponent();
            treeView.ImageList = Utils.ImageList;
            m_num_kod = Utils.GetParentNumKod(num_kod);
            lblChild.Text = obozn;
        }
        private void SelectMidParentDialog_Load(object sender, EventArgs e)
        {
            lblTable.Text = Utils.NumKodString(m_num_kod);
            txtSearch.Enabled = btnSearch.Enabled = m_num_kod != 94;
            if (m_num_kod == 94)
                SearchMid0();
        }
        public void FillMid(List<MidObject> list)
        {
            treeView.Nodes.Clear();
            foreach (MidObject o in list)
            {
                TreeGridNode node = treeView.Nodes.Add(o.obozn);
                node.Image = Utils.GetMidImage(o);
                node.Cells[1].Value = o.naimen;
                node.Tag = o;
            }
        }
        public void FillPoz(List<PozObject> list)
        {
            treeView.Nodes.Clear();
            foreach (PozObject o in list)
            {
                TreeGridNode node = treeView.Nodes.Add(o.obozn);
                node.Image = Utils.GetPozImage(o.num_kod);
                node.Cells[1].Value = o.naimen;
                node.Tag = o;
            }
        }
        private void SearchMid()
        {
            string srch = txtSearch.Text;
            int len = Utils.SearchOboznLength(m_num_kod);
            if (len > srch.Length)
            {
                Utils.Error("Малое количество символов.");
                return;
            }
            if (SearchEvent != null)
                SearchEvent(this, new SearchEventArgs("obozn", srch, m_num_kod));
        }
        private void SearchMid0()
        {
            if (SearchEvent != null)
                SearchEvent(this, new SearchEventArgs("", "", m_num_kod));
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchMid();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SearchMid();
        }
        public UInt32 Id
        {
            get
            {
                if (treeView.CurrentNode != null)
                {
                    if (treeView.CurrentNode.Tag is MidObject)
                    {
                        MidObject o = treeView.CurrentNode.Tag as MidObject;
                        return o.id;                        
                    }
                    if (treeView.CurrentNode.Tag is PozObject)
                    {
                        PozObject o = treeView.CurrentNode.Tag as PozObject;
                        return o.id;
                    }
                    return 0;
                }
                else
                    return 0;
            }
        }

        
    }
}
