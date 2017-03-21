using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using libspec.Objects;
using libspec.Dialogs;
using libspec.Data;
namespace libspec
{
    public class SpecModel
    {
        private SpecDataAdapter m_da;
        private SpecViewTree m_view;
        private List<ProjectObject> m_projects;
        public SpecModel(SpecViewTree view, SpecDataAdapter da)
        {
            m_da = da;
            m_view = view;
            m_projects = Utils.LoadProjectList();
            m_view.FillTree(m_projects);
            AddListeners();
        }

        private void AddListeners()
        {
            m_view.NodeClickEvent += new EventHandler<ViewEvent.NodeClickEventArgs>(m_view_NodeClick);
            m_view.ExpandEvent += new EventHandler<ViewEvent.ExpandEventArgs>(m_view_ExpandEvent);
            m_view.ButtonActionEvent += new EventHandler<ViewEvent.ButtonActionEventArgs>(m_view_ButtonActionEvent);
            m_view.SearchEvent += new EventHandler<ViewEvent.SearchEventArgs>(m_view_SearchEvent);
            m_view.AddPozEvent += new EventHandler<ViewEvent.AddPozEventArgs>(m_view_AddPozEvent);
            m_view.MovePozEvent += new EventHandler<ViewEvent.MovePozEventArgs>(m_view_MovePozEvent);
            m_view.AddDocEvent += new EventHandler<ViewEvent.AddDocEventArgs>(m_view_AddDocEvent);
            m_view.MoveDocEvent += new EventHandler<ViewEvent.MoveDocEventArgs>(m_view_MoveDocEvent);
        }

        void m_view_MoveDocEvent(object sender, ViewEvent.MoveDocEventArgs e)
        {
            m_da.MoveDoc(e.doc, e.grp.id);
        }

        void m_view_AddDocEvent(object sender, ViewEvent.AddDocEventArgs e)
        {
            m_da.AddDoc(e.doc, e.grp.id, true);
        }

        void m_view_MovePozEvent(object sender, ViewEvent.MovePozEventArgs e)
        {
            m_da.MovePoz(e.poz, e.doc);
        }

        void m_view_AddPozEvent(object sender, ViewEvent.AddPozEventArgs e)
        {
            m_da.AddPoz(e.doc, e.poz);
        }

        private void m_view_ButtonActionEvent(object sender, ViewEvent.ButtonActionEventArgs e)
        {            
            switch (e.Action)
            {
                case ViewEvent.ButtonAction.SelectProject:
                    {
                        SelectProjectDialog dlg = new SelectProjectDialog(m_da.GetProjectList());
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            foreach (ProjectObject o in dlg.SelectedObjects)
                            {
                                if (!m_projects.Contains(o))
                                    m_projects.Add(o);
                            }
                            m_projects = m_projects.OrderBy(o => o.obozn).ToList();
                            Utils.SaveProjectList(m_projects);
                            m_view.FillTree(m_projects);
                        }
                    }
                    break;
                case ViewEvent.ButtonAction.KeyDelete:
                    {
                        if (e.Target.Tag is ProjectObject)
                        {
                            m_projects.Remove(e.Target.Tag as ProjectObject);
                            Utils.SaveProjectList(m_projects);
                            m_view.RemoveNode(e.Target);
                        }
                        if (e.Target.Tag is GroupObject)
                        {
                            GroupObject o = e.Target.Tag as GroupObject;
                            if (MessageBox.Show("Удалить группу " + o.obozn + "?", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                            {
                                if (m_da.DeleteGroup(o))
                                {
                                    m_view.RemoveNode(e.Target);
                                }
                            }
                        }
                        if (e.Target.Tag is DocObject)
                        {
                            DocObject o = e.Target.Tag as DocObject;
                            if (MessageBox.Show("Удалить документ " +  o.obozn + "?", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                            {
                                if (m_da.DeleteDoc(o))
                                {
                                    m_view.RemoveNode(e.Target);
                                }
                            }
                        }
                        if (e.Target.Tag is PozObject)
                        {
                            PozObject o = e.Target.Tag as PozObject;
                            if (MessageBox.Show("Удалить позицию " + o.obozn + "?", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                            {
                                if (m_da.DeletePoz(o))
                                {
                                    m_view.RemoveNode(e.Target);
                                }
                            }
                        }
                    }
                    break;

                case ViewEvent.ButtonAction.AddProject:
                    {
                        AddObjectDialog dlg = new AddObjectDialog(e.Action);
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            ProjectObject o = m_da.AddProject(dlg.Object as ProjectObject);
                            if (o != null && o.id > 0)
                                m_view.AddNode(o);
                        }
                        
                    }
                    break;
                case ViewEvent.ButtonAction.AddGroup:
                    {
                        AddObjectDialog dlg = new AddObjectDialog(e.Action);
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            GroupObject o = m_da.AddGroup(dlg.Object as GroupObject, (e.Target.Tag as ProjectObject).id);
                            if (o != null && o.id > 0)
                                m_view.AddNode(o);
                        }
                        
                    }
                    break;
                case ViewEvent.ButtonAction.AddDoc:
                    {
                        string name = string.Format("{0}-{1}-", e.Target.Parent.Cells[0].Value, e.Target.Cells[0].Value);
                        AddObjectDialog dlg = new AddObjectDialog(e.Action, name);
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            DocObject o = m_da.AddDoc(dlg.Object as DocObject, (e.Target.Tag as GroupObject).id);
                            if (o != null && o.id > 0)
                                m_view.AddNode(o);
                        }
                    }
                    break;
                case ViewEvent.ButtonAction.KeyClose:
                    {
                        if (e.Target.Tag is GroupObject)
                        {
                            GroupObject o = e.Target.Tag as GroupObject;
                            if (o.status != Closed.Y)
                            {
                                if (m_da.SetStatusGroup(ref o, Closed.Y))
                                    m_view.UpdateNode(e.Target, o);
                            }
                        }
                        if (e.Target.Tag is DocObject)
                        {
                            DocObject o = e.Target.Tag as DocObject;
                            if (o.status != Closed.Y)
                            {
                                if (m_da.SetStatusDoc(ref o, Closed.Y))
                                    m_view.UpdateNode(e.Target, o);
                            }
                        }
                    }
                    break;
                case ViewEvent.ButtonAction.KeyOpen:
                    {
                        if (e.Target.Tag is GroupObject)
                        {
                            GroupObject o = e.Target.Tag as GroupObject;
                            if (o.status != Closed.N)
                            {
                                if (m_da.SetStatusGroup(ref o, Closed.N))
                                    m_view.UpdateNode(e.Target, o);
                            }
                        }
                        if (e.Target.Tag is DocObject)
                        {
                            DocObject o = e.Target.Tag as DocObject;
                            if (o.status != Closed.N)
                            {
                                if (m_da.SetStatusDoc(ref o, Closed.N))
                                    m_view.UpdateNode(e.Target, o);
                            }
                        }
                    }
                    break;
                case ViewEvent.ButtonAction.KeyUpdate:
                    {
                        if (e.Target.Tag is ProjectObject)
                        {
                            ProjectObject o = e.Target.Tag as ProjectObject;
                            List<GroupObject> list = m_da.GetGroupList(o.id);
                            m_view.FillProject(list);
                        }
                        if (e.Target.Tag is GroupObject)
                        {
                            GroupObject o = e.Target.Tag as GroupObject;
                            List<DocObject> list = m_da.GetDocList(o.id);
                            m_view.FillGroup(list);
                        }
                        if (e.Target.Tag is DocObject)
                        {
                            DocObject o = e.Target.Tag as DocObject;
                            List<PozObject> list = m_da.GetPozList("lid_old", o.refid);
                            m_view.FillPoz(list);
                        }
                    }
                    break;
            }
        }

        void m_view_SearchEvent(object sender, ViewEvent.SearchEventArgs e)
        {
            string table = Utils.GetTable(e.num_kod);
            if (e.search_field == "gost")
                table = "mid2";
            if (e.search_string.Length == 11)
                table = "mid3";
            List<PozObject> list = m_da.SearchPoz(table, e.search_field, e.search_string);
            SearchPozDialog search_view = sender as SearchPozDialog;
            search_view.FillPoz(list);
        }

        void m_view_ExpandEvent(object sender, ViewEvent.ExpandEventArgs e)
        {
            UInt32 refid = e.Object.refid == 0 ? e.Object.id : e.Object.refid;
            List<PozObject> list = m_da.GetPozList(Utils.GetChildTable(e.Object.num_kod), refid);            
            if (sender.Equals(m_view))
                m_view.FillPoz(list);
            else
            {
                SearchPozDialog search_view = sender as SearchPozDialog;
                search_view.FillPoz(list);
            }

        }

        private void m_view_NodeClick(object sender, ViewEvent.NodeClickEventArgs e)
        {
            if (e.Object is ProjectObject)
            {
                m_view.FillProject(m_da.GetGroupList(e.Object.id));
                return;
            }
            if (e.Object is GroupObject)
            {
                m_view.FillGroup(m_da.GetDocList(e.Object.id));
                return;
            }
            if (e.Object is DocObject)
            {
                m_view.FillPoz(m_da.GetPozList("lid_old", e.Object.refid));
                return;
            }
        }


    }
}
