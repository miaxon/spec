using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using libspec.Properties;
using libspec.ViewEvent;
using libspec.Objects;
using libspec.ViewItem;
using libspec.Contrlos;
namespace libspec
{
    public partial class SpecView : UserControl
    {
        #region events
        public event EventHandler<TreeNodeClickEventArgs> NodeClickEvent;
        public event EventHandler<RaskrEventArgs> RaskrEvent;
        public event EventHandler<SearchEventArgs> SearchEvent;
        #endregion
        private TreeNode m_nodeToFill;
        private SpecPozVew m_viewToFill;
        public SpecView()
        {
            InitializeComponent();
            treeView.ImageList = Utils.ImageList;
            statusStrip.ImageList = Utils.ImageList;

        }

        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            m_nodeToFill = e.Node;
            if (m_nodeToFill.Nodes.Count == 0)
                NodeClickEvent(this, new TreeNodeClickEventArgs(e.Node));
        }

        public void FillTree(List<ProjectObject> list)
        {
            treeView.Nodes.Clear();
            foreach (ProjectObject o in list)
                treeView.Nodes.Add(new ProjectNode(o));
        }
        public void FillProject(List<GroupObject> list)
        {
            m_nodeToFill.Nodes.Clear();
            foreach (GroupObject o in list)
                m_nodeToFill.Nodes.Add(new GroupNode(o));
        }

        public void FillGroup(List<DocObject> list)
        {
            m_nodeToFill.Nodes.Clear();
            foreach (DocObject o in list)
                m_nodeToFill.Nodes.Add(new DocNode(o));
        }

        public void FillPoz(List<PozObject> list)
        {
            pozView.Clear();
            foreach (PozObject o in list)
                pozView.AddPoz(o);
        }
        public void FillPozView(List<PozObject> list)
        {
            m_viewToFill.Clear();
            foreach (PozObject o in list)
                m_viewToFill.AddPoz(o);
        }
        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            BaseObject o = (e.Node as BaseNode).Object;
            tslblSelection.Text = string.Format("{0} {1}", o.obozn, o.naimen);
            tslblSelection.ImageKey = Utils.GetNodeImageKey(o);

        }

        private void pozView_RaskrEvent(object sender, RaskrEventArgs e)
        {
            SpecTabPage page = new SpecTabPage(e.Object.obozn);
            tabControl.TabPages.Add(page);
            tabControl.SelectedTab = page;
            m_viewToFill = page.View;
            page.View.RaskrEvent += new EventHandler<RaskrEventArgs>(pozView_RaskrEvent);
            if (RaskrEvent != null)
                RaskrEvent(sender, e);
        }        

        private void pozView_SearchEvent(object sender, SearchEventArgs e)
        {
            SpecTabPage page = new SpecTabPage(e.search_string);
            tabControl.TabPages.Add(page);
            tabControl.SelectedTab = page;
            m_viewToFill = page.View;
            page.View.RaskrEvent += new EventHandler<RaskrEventArgs>(pozView_RaskrEvent);
            if (SearchEvent != null)
                SearchEvent(this, e);
        }


    }
}
