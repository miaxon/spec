using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using libspec.View.Objects;
using libspec.View.Dialogs;
using libspec.View.Data;
using System.Reflection;
using System.Globalization;
using libspec.View.ViewEvent;

namespace libspec.View
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
            m_view.NodeEditEvent += new EventHandler<ViewEvent.NodeEditEventArgs>(m_view_NodeEditEvent);
        }
        void m_view_NodeEditEvent(object sender, ViewEvent.NodeEditEventArgs e)
        {
            if (sender is SearchPozDialog)
            {
                EditSearchPoz(sender as SearchPozDialog, e);
            }
            if (sender is SpecViewTree)
            {
                EditView(e);
            }

        }
        private void EditView(NodeEditEventArgs e)
        {
            string value = e.Value.ToString();
            bool noError = true;
            string query = "";
            string query_val = "'" + value + "'";
            if (e.Object is ProjectObject)
            {
                ProjectObject o = e.Object as ProjectObject;
                switch (e.Field)
                {
                    case "obozn":
                        if (noError = !m_da.ProjectExists(value))
                            o.obozn = value;
                        break;
                    case "naimen":
                        o.naimen = value;
                        break;
                    case "descr":
                        o.descr = value;
                        break;
                }
                query = string.Format("update _pid set {0}={1} where id={2}", e.Field, query_val, o.id);
            }
            if (e.Object is GroupObject)
            {
                GroupObject o = e.Object as GroupObject;
                switch (e.Field)
                {
                    case "obozn":                        
                            o.obozn = value;
                        break;
                    case "naimen":
                        o.naimen = value;
                        break;
                    case "descr":
                        o.descr = value;
                        break;
                }
                query = string.Format("update _gid set {0}={1} where id={2}", e.Field, query_val, o.id);
            }
            if (e.Object is DocObject)
            {
                DocObject o = e.Object as DocObject;
                switch (e.Field)
                {
                    case "obozn":
                        if (noError = !m_da.DocExists(value))
                            o.obozn = value;
                        break;
                    case "naimen":
                        o.naimen = value;
                        break;
                    case "descr":
                        o.descr = value;
                        break;
                    case "num_kol":
                        {
                            query_val = value;
                            UInt16 c = 0;
                            if (noError = UInt16.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out c))
                                o.num_kol = c;
                        }
                        break;
                }
                query = string.Format("update lid set {0}={1} where id={2}", e.Field, query_val, o.refid);
            }
            if (e.Object is PozObject)
            {
                PozObject o = e.Object as PozObject;
                switch (e.Field)
                {
                    case "descr":
                        if (noError = !m_da.PozExists(value, o.num_kod))
                            o.obozn = value;
                        break;
                    case "num_kol":
                        {
                            query_val = value;
                            UInt16 c = 0;
                            if (noError = UInt16.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out c))
                                o.num_kol = c;
                        }
                        break;
                    case "num_kfr":
                        {
                            query_val = value;
                            double c = 0;
                            if (noError = double.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out c))
                                o.num_kfr = c;
                        }
                        break;
                }
                query = string.Format("update lid_old set {0}={1} where id={2}", e.Field, query_val, o.id);
            }

            if (!noError)
            {
                m_view.RollBack();
                return;
            }
            if (!m_da.ExecQuery(query))
            {
                m_view.RollBack();
                return;
            }
            m_view.UpdateNode(e.Object);
            string msg = string.Format("Сохранено: {0}", value);
            m_view.EditResult(msg);
            if (e.Object is ProjectObject)
            {
                ProjectObject pEdit = e.Object as ProjectObject;
                ProjectObject pSaved = m_projects.Find(o => o.id == pEdit.id);
                if (pSaved != null)
                    m_projects.Remove(pSaved);
                m_projects.Add(pEdit);
                m_projects = m_projects.OrderBy(o => o.obozn).ToList();
                Utils.SaveProjectList(m_projects);
            }
        }
        private void EditSearchPoz(SearchPozDialog view, NodeEditEventArgs e)
        {
            string value = e.Value.ToString();
            bool noError = true;
            string query = "";
            string query_val = "'" + value + "'";
            PozObject o = e.Object as PozObject;            
            if (o == null)
            {
                return;
            }
            string table = Utils.GetTable(o.num_kod);
            if(string.IsNullOrEmpty(table))
            {
                view.RollBack();
                return;
            }
            switch (e.Field)
            {
                case "obozn":
                    if (noError = m_da.PozExists(value, o.num_kod))
                        o.obozn = value;
                    break;
                case "naimen":
                    o.naimen = value;
                    break;
                case "num_kol":
                    {
                        query_val = value;
                        UInt16 c = 0;
                        if (noError = UInt16.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out c))
                            o.num_kol = c;
                    }
                    break;
                case "gost":
                    o.gost = value;
                    break;
                case "marka":
                    o.marka = value;
                    break;
                case "kei":
                    {
                        // if value is kei obozn number string
                        string kei_naimen = Utils.GetKeiNaimen(value);
                        if (noError = !(kei_naimen == value))
                        {
                            o.kei = value;
                            break;
                        }
                        // if value is kei naimen string
                        string kei_obozn = Utils.GetKeiObozn(value);
                        if (noError = !(kei_obozn == value))
                        {
                            o.kei = value = kei_obozn;
                            query_val = "'" + value + "'";
                        }
                    }
                    break;
                case "num_kfr":
                    {
                        query_val = value;
                        double c = 0;
                        if (noError = double.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out c))
                            o.num_kfr = c;
                    }
                    break;
                case "descr":
                    o.descr = value;
                    break;
            }
            if (!noError)
            {
                view.RollBack();
                return;
            }            
            query = string.Format("update {0} set {1}={2} where id={3}", table, e.Field, query_val, o.id);
            if (!m_da.ExecQuery(query))
            {
                view.RollBack();
                return;
            }
            view.UpdateNode(o);
            string msg = string.Format("Сохранено: {0}", value);
            view.EditResult(msg);
        }
        private void m_view_MoveDocEvent(object sender, ViewEvent.MoveDocEventArgs e)
        {
            m_da.MoveDoc(e.doc, e.grp.id);
        }        
        private void m_view_AddDocEvent(object sender, ViewEvent.AddDocEventArgs e)
        {
            string name = string.Format("{0}-{1}-", e.Project.obozn, e.Group.obozn);
        dlg: DocObject o = NewObject(ViewEvent.ButtonAction.AddDoc, name) as DocObject;
            if (o == null)
                return;
            if (!m_da.DocExists(o.obozn))
            {

                e.Doc.obozn = o.obozn;
                e.Doc.naimen = o.naimen;
                e.Doc.descr = o.descr;
                m_da.AddDoc(e.Doc, e.Group.id);
                return;
            }
            else
                goto dlg;

        }
        private void m_view_MovePozEvent(object sender, ViewEvent.MovePozEventArgs e)
        {
            if (e.dst is DocObject)
            {
                DocObject dst = e.dst as DocObject;
                m_da.MovePoz(e.src, dst);
            }
        }
        private void m_view_AddPozEvent(object sender, ViewEvent.AddPozEventArgs e)
        {
            if (e.src is PozObject && e.dst is DocObject)
            {
                DocObject doc = e.dst as DocObject;
                PozObject poz = e.src as PozObject;
                m_da.AddPoz(poz, doc);
            }
        }
        private void m_view_ButtonActionEvent(object sender, ViewEvent.ButtonActionEventArgs e)
        {
            switch (e.Action)
            {
                case ViewEvent.ButtonAction.SelectProject:
                    {
                        SelectProjectDialog dlg = new SelectProjectDialog(m_da.GetProjectList());
                        dlg.DelProjectEvent += new EventHandler<ButtonActionEventArgs>(dlg_DelProjectEvent);
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
                        if (e.Target is ProjectObject)
                        {
                            m_projects.Remove(e.Target as ProjectObject);
                            Utils.SaveProjectList(m_projects);
                            m_view.RemoveCurrentNode();
                        }
                        if (e.Target is GroupObject)
                        {
                            GroupObject o = e.Target as GroupObject;
                            if (Utils.Warning("Удалить группу " + o.obozn + "?"))
                            {
                                if (m_da.DeleteGroup(o))
                                {
                                    m_view.RemoveCurrentNode();
                                }
                            }
                        }
                        if (e.Target is DocObject)
                        {
                            DocObject o = e.Target as DocObject;
                            if (Utils.Warning("Удалить докуимент " + o.obozn + "?"))
                            {
                                if (m_da.DeleteDoc(o))
                                {
                                    m_view.RemoveCurrentNode();
                                }
                            }
                        }
                        if (e.Target is PozObject)
                        {
                            PozObject o = e.Target as PozObject;
                            if (Utils.Warning("Удалить позицию " + o.obozn + "?"))
                            {
                                if (m_da.DeletePoz("lid_old", o.id))
                                {
                                    m_view.RemoveCurrentNode();
                                }
                            }
                        }
                    }
                    break;

                case ViewEvent.ButtonAction.AddProject:
                    {
                    pdlg: ProjectObject o = (NewObject(e.Action) as ProjectObject);
                        if (o == null)
                            break;
                        if (!m_da.ProjectExists(o.obozn))
                        {
                            o = m_da.AddProject(o);
                            if (!m_projects.Contains(o))
                                m_projects.Add(o);
                            m_projects = m_projects.OrderBy(p => p.obozn).ToList();
                            Utils.SaveProjectList(m_projects);
                            if (o != null && o.id > 0)
                                m_view.AddNode(o);
                        }
                        else
                            goto pdlg;


                    }
                    break;
                case ViewEvent.ButtonAction.AddGroup:
                    {
                    gdlg: GroupObject o = (NewObject(e.Action) as GroupObject);
                        if (o == null)
                            break;
                        UInt32 parent = (e.Target as ProjectObject).id;
                        if (!m_da.GroupExists(o.obozn, parent))
                        {
                            o = m_da.AddGroup(o, parent);
                            if (o != null && o.id > 0)
                                m_view.AddNode(o);
                        }
                        else
                            goto gdlg;

                    }
                    break;
                case ViewEvent.ButtonAction.AddDoc:
                    {

                        string name = e.Data as string;
                    ddlg: DocObject o = (NewObject(e.Action, name) as DocObject);
                        if (o == null)
                            return;
                        if (!m_da.DocExists(o.obozn))
                        {
                            o = m_da.AddDoc(o, (e.Target as GroupObject).id);
                            if (o != null && o.id > 0)
                                m_view.AddNode(o);
                        }
                        else
                            goto ddlg;
                    }
                    break;
                case ViewEvent.ButtonAction.KeyClose:
                    {
                        if (e.Target is GroupObject)
                        {
                            GroupObject o = e.Target as GroupObject;
                            if (o.status != Closed.Y)
                            {
                                if (m_da.SetStatusGroup(ref o, Closed.Y))
                                {
                                    m_view.UpdateNode(o);
                                    UpdateChilds(e.Target);
                                }
                            }
                        }
                        if (e.Target is DocObject)
                        {
                            DocObject o = e.Target as DocObject;
                            if (o.status != Closed.Y)
                            {
                                if (m_da.SetStatusDoc(ref o, Closed.Y))
                                {
                                    m_view.UpdateNode(o);
                                    UpdateChilds(e.Target);
                                }
                            }
                        }
                    }
                    break;
                case ViewEvent.ButtonAction.KeyOpen:
                    {
                        if (e.Target is GroupObject)
                        {
                            GroupObject o = e.Target as GroupObject;
                            if (o.status != Closed.N)
                            {
                                if (m_da.SetStatusGroup(ref o, Closed.N))
                                {
                                    m_view.UpdateNode(o);
                                    UpdateChilds(e.Target);
                                }
                            }
                        }
                        if (e.Target is DocObject)
                        {
                            DocObject o = e.Target as DocObject;
                            if (o.status != Closed.N)
                            {
                                if (m_da.SetStatusDoc(ref o, Closed.N))
                                {
                                    m_view.UpdateNode(o);
                                    UpdateChilds(e.Target);
                                }
                            }
                        }
                    }
                    break;
                case ViewEvent.ButtonAction.KeyUpdate:
                    {
                        UpdateChilds(e.Target);
                    }
                    break;
            }
        }

        void dlg_DelProjectEvent(object sender, ButtonActionEventArgs e)
        {
            ProjectObject o = e.Target as ProjectObject;
            if (o == null)
                return;
            if(!Utils.Warning("Удалить проект " + o.obozn + "?"))
                return;
            if (m_da.DeleteProject(o))
            {
                SelectProjectDialog view = sender as SelectProjectDialog;
                view.DeleteCurrentNode();
            }
        }        
        private void m_view_SearchEvent(object sender, ViewEvent.SearchEventArgs e)
        {
            string table = Utils.GetTable(e.num_kod);
            if (string.IsNullOrEmpty(table))
                return;
            if (sender is SearchPozDialog)
            {                
                SearchPozDialog view = sender as SearchPozDialog;
                if (e.search_field == "gost")
                    table = "mid2";
                if (e.search_string.Length == 11)
                    table = "mid3";
                List<PozObject> list = m_da.SearchPoz(table, e.search_field, e.search_string);                
                view.FillPoz(list);
            }            
        }
        private void m_view_ExpandEvent(object sender, ViewEvent.ExpandEventArgs e)
        {
            UInt32 refid = e.Object.refid == 0 ? e.Object.id : e.Object.refid;
            string table = Utils.GetChildTable(e.Object.num_kod);
            if (string.IsNullOrEmpty(table))
            {
                if (e.Object.num_kod == 92)
                    table = "mid3";
                else
                    return;
            }
            List<PozObject> list = m_da.GetPozList(table, refid);
            if (sender is SpecViewTree)
                m_view.FillPoz(list);
            if(sender is SearchPozDialog)
            {
                SearchPozDialog view = sender as SearchPozDialog;
                view.FillPoz(list);
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
        private void UpdateChilds(object obj)
        {
            if (obj is ProjectObject)
            {
                ProjectObject o = obj as ProjectObject;
                List<GroupObject> list = m_da.GetGroupList(o.id);
                m_view.FillProject(list);
            }
            if (obj is GroupObject)
            {
                GroupObject o = obj as GroupObject;
                List<DocObject> list = m_da.GetDocList(o.id);
                m_view.FillGroup(list);
            }
            if (obj is DocObject)
            {
                DocObject o = obj as DocObject;
                List<PozObject> list = m_da.GetPozList("lid_old", o.refid);
                m_view.FillPoz(list);
            }
        }
        private BaseObject NewObject(ViewEvent.ButtonAction action, string name = "")
        {
            AddObjectDialog dlg = new AddObjectDialog(action, name);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                return dlg.Object;
            }
            return null;
        }
    }
}
