using libspec.View.Data;
using libspec.View.Dialogs;
using libspec.View.Objects;
using libspec.View.ViewEvent;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
namespace libspec.View
{
    public class SpecModelBase
    {
        private SpecDataAdapter m_da;
        private SpecViewTable m_view;
        public SpecModelBase(SpecViewTable view, SpecDataAdapter da)
        {
            m_da = da;
            m_view = view;
            AddListeners();
        }
        private void AddListeners()
        {
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
            if (sender is SpecViewTable)
            {
                EditView(sender as SpecViewTable, e);
            }
        }
        private void EditSearchPoz(SearchPozDialog view, NodeEditEventArgs e)
        {
            string value = e.Value.ToString();
            bool noError = true;
            string query = "";
            string query_val = "'" + value + "'";
            PozObject o = e.Object as PozObject;
            string table = Utils.GetTable(o.num_kod);
            if (o == null)
            {
                return;
            }
            switch (e.Field)
            {
                case "obozn":
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
            if (MessageBox.Show("Сохранить внесенные изменения?", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
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
        }
        private void EditView(SpecViewTable view, NodeEditEventArgs e)
        {
            string value = e.Value.ToString();
            
            string query = "";
            string query_val = "'" + value + "'";
            if (e.Object is PozObject)
            {
                PozObject o = e.Object as PozObject;
                string table = Utils.GetTable(o.num_kod);
                if (string.IsNullOrEmpty(table))
                    return;
                bool noError = true;
                switch (e.Field)
                {
                    case "obozn":
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
                if (MessageBox.Show("Сохранить внесенные изменения?", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
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
            }
            if (e.Object is MidObject)
            {
                MidObject o = e.Object as MidObject;
                string table = Utils.GetTable(o.num_kod);
                if (string.IsNullOrEmpty(table))
                    return;
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
                if (MessageBox.Show("Сохранить внесенные изменения?", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
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
            }
        }
        private void m_view_MoveDocEvent(object sender, ViewEvent.MoveDocEventArgs e)
        {
            m_da.MoveDoc(e.doc, e.grp.id);
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
            m_da.MovePoz(e.poz, e.doc);
        }
        private void m_view_AddPozEvent(object sender, ViewEvent.AddPozEventArgs e)
        {
            if (e.target is PozObject)
            {
                PozObject o = e.target as PozObject;
                if (m_da.AddPoz(o, e.poz))
                {
                    
                }
            }
        }
        private void m_view_ButtonActionEvent(object sender, ViewEvent.ButtonActionEventArgs e)
        {
            switch (e.Action)
            {

                case ViewEvent.ButtonAction.KeyDelete:
                    {
                        if (e.Target.Tag is PozObject)
                        {
                            PozObject parent = e.Target.Parent.Tag as PozObject;
                            PozObject o = e.Target.Tag as PozObject;
                            if (parent != null)
                            {                                
                                string table = Utils.GetChildTable(parent.num_kod);
                                if (string.IsNullOrEmpty(table))
                                    return;
                                if (MessageBox.Show("Удалить позицию " + o.obozn + "?", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                                {
                                    if (m_da.DeletePoz(table, o))
                                    {
                                        m_view.RemoveNode(e.Target);
                                    }
                                }
                            }
                            else
                            {
                                string table = Utils.GetTable(o.num_kod);
                                if (string.IsNullOrEmpty(table))
                                    return;
                                if (MessageBox.Show("Удалить позицию " + o.obozn + "?", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                                {
                                    if (m_da.DeletePozRoot(table, o))
                                    {
                                        m_view.RemoveNode(e.Target);
                                    }
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
                        UInt32 parent = (e.Target.Tag as ProjectObject).id;
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
                        string name = string.Format("{0}-{1}-", e.Target.Parent.Cells[0].Value, e.Target.Cells[0].Value);
                    ddlg: DocObject o = (NewObject(e.Action, name) as DocObject);
                        if (o == null)
                            return;
                        if (!m_da.DocExists(o.obozn))
                        {
                            o = m_da.AddDoc(o, (e.Target.Tag as GroupObject).id);
                            if (o != null && o.id > 0)
                                m_view.AddNode(o);
                        }
                        else
                            goto ddlg;
                    }
                    break;

                case ViewEvent.ButtonAction.KeyUpdate:
                    {
                        UpdateFill(e.Target.Tag);
                    }
                    break;
            }
        }
        private void UpdateFill(object obj)
        {
            if (obj is PozObject)
            {
                PozObject o = obj as PozObject;
                UInt32 refid = o.refid == 0 ? o.id : o.refid;
                string table = Utils.GetChildTable(o.num_kod);
                if (string.IsNullOrEmpty(table))
                    return;
                List<PozObject> list = m_da.GetPozList(table, refid);
                m_view.FillPoz(list);
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
            if (sender is SpecViewTable)
            {
                SpecViewTable view = sender as SpecViewTable;
                if (table == "mid0")
                {
                    List<MidObject> mid0 = m_da.SearchMid0();
                    view.FillMid(mid0);
                    return;
                }
                if (table == "mid1")
                {
                    List<MidObject> mid1 = m_da.SearchMid1(e.search_string);
                    view.FillMid(mid1);
                    return;
                }
                List<PozObject> list = m_da.SearchPoz(table, e.search_field, e.search_string);
                view.FillPoz(list);
            }
        }
        private void m_view_ExpandEvent(object sender, ViewEvent.ExpandEventArgs e)
        {
            UInt32 refid = e.Object.refid == 0 ? e.Object.id : e.Object.refid;
            string table = Utils.GetChildTable(e.Object.num_kod);
            if (string.IsNullOrEmpty(table))
                return;
            List<PozObject> list = m_da.GetPozList(table, refid);
            if (sender is SpecViewTable)
            {
                SpecViewTable view = sender as SpecViewTable;
                view.FillPoz(list);
            }
            if (sender is SearchPozDialog)
            {
                SearchPozDialog view = sender as SearchPozDialog;
                view.FillPoz(list);
            }
            
        }


    }
}
