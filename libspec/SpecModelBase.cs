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
            m_view.NodeEditEvent += new EventHandler<ViewEvent.NodeEditEventArgs>(m_view_NodeEditEvent);
            m_view.AddRootPozEvent += new EventHandler<AddRootPozEventArgs>(m_view_AddRootPozEvent);
            m_view.FillBadEvent += new EventHandler<FillBadEventArgs>(m_view_FillBadEvent);
        }
        void m_view_FillBadEvent(object sender, FillBadEventArgs e)
        {
            switch (e.num_kod)
            {
                case 9: // mid3
                    {
                        List<PozObject> list = m_da.SearchMid3Bad();
                        m_view.FillPoz(list);
                    }
                    break;
                case 92: // mid2
                    {
                        List<PozObject> list = m_da.SearchMid2Bad();
                        m_view.FillPoz(list);
                    }
                    break;
                case 93: // mid1
                    {
                        List<MidObject> list = m_da.SearchMid1Bad();
                        m_view.FillMid(list);
                    }
                    break;
            }
        }
        void m_view_AddRootPozEvent(object sender, AddRootPozEventArgs e)
        {
            string name = "";
            dlg: PozObject o = NewObject(ViewEvent.ButtonAction.AddRootPoz, e.num_kod, name);
            if (o == null)
                return;
            name = o.obozn;
            o.num_kod = e.num_kod;
            if (!m_da.PozExists(o.obozn, o.num_kod))
            {
                if (e.num_kod < 9 || e.num_kod == 100)
                {
                    PozObject poz = m_da.AddRootPoz(o);
                    if (poz != null)
                        m_view.AddRootNode(poz);
                    return;
                }
                else
                {
                    MidObject p = new MidObject(o);
                    if (p.num_kod != 94) // mid0
                    {
                        p.parent = m_da.GetMidParent(o.obozn, o.num_kod);
                        if (p.parent == 0)
                            p.parent = SelectParent(o.obozn, o.num_kod);
                        if (p.parent == 0)
                            return;
                    }
                    MidObject poz = m_da.AddRootMid(p);
                    if (poz != null)
                        m_view.AddMidNode(poz);
                    return;
                }
            }
            else
                goto dlg;

        }
        private UInt32 SelectParent(string obozn, int num_kod)
        {
            SelectMidParentDialog dlg = new SelectMidParentDialog(num_kod, obozn);
            dlg.SearchEvent += new EventHandler<SearchEventArgs>(m_view_SearchEvent);
            if (dlg.ShowDialog() == DialogResult.Cancel)
                return 0;
            return dlg.Id;
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
            if (o == null)
            {
                return;
            }
            string table = Utils.GetTable(o.num_kod);
            if (string.IsNullOrEmpty(table))
            {
                view.RollBack();
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
        private void EditView(SpecViewTable view, NodeEditEventArgs e)
        {
            string value = e.Value.ToString();
            bool noError = true;
            string query = "";
            string query_val = "'" + value + "'";
            if (e.Object is PozObject)
            {
                PozObject o = e.Object as PozObject;
                if (e.Parent == null) // root node
                {                    
                    string table = Utils.GetTable(o.num_kod);
                    if (string.IsNullOrEmpty(table))
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
                        case "descr":
                            o.descr = value;
                            break;
                        default:
                            noError = false;
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
                    view.UpdatePozNode(o);
                    string msg = string.Format("Сохранено: {0}", value);
                    m_view.EditResult(msg);
                }
                else
                {
                    PozObject parent = e.Parent as PozObject;
                    if(parent == null)
                    {
                        view.RollBack();
                        return;
                    }
                    string table = Utils.GetChildTable(parent.num_kod);
                    if (string.IsNullOrEmpty(table))
                    {
                        view.RollBack();
                        return;
                    }
                    switch (e.Field)
                    {                        
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
                        case "descr":
                            {
                                o.descr = value;
                            }
                            break;
                        default:
                            noError = false;
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
                    view.UpdatePozNode(o);
                    string msg = string.Format("Сохранено: {0}", value);
                    m_view.EditResult(msg);
                }
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
                        if (noError = !m_da.PozExists(value, o.num_kod))
                            o.obozn = value;
                        break;
                    case "naimen":
                        o.naimen = value;
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
                view.UpdateMidNode(o);
                string msg = string.Format("Сохранено: {0}", value);
                m_view.EditResult(msg);
            }
        }
        private void m_view_MovePozEvent(object sender, ViewEvent.MovePozEventArgs e)
        {
            if (e.dst is PozObject)
            {
                PozObject dst = e.dst as PozObject;
                m_da.MovePoz(e.src, dst);
            }
        }
        private void m_view_AddPozEvent(object sender, ViewEvent.AddPozEventArgs e)
        {
            if (e.src is PozObject)
            {
                PozObject src = e.src as PozObject;
                if (e.dst is PozObject)
                {
                    PozObject dst = e.dst as PozObject;
                    if (m_da.AddPoz(src, dst))
                        m_view.AddPozNode(src);
                    return;
                }
                if (e.dst == null)
                {
                    dlg: PozObject o = NewObject(ViewEvent.ButtonAction.AddPoz, src.obozn) as PozObject;
                    if (o == null)
                        return;
                    PozObject p = src.Clone();
                    p.obozn = o.obozn;
                    p.naimen = string.IsNullOrEmpty(o.naimen) ? src.naimen : o.naimen;
                    p.descr = o.descr;
                    if (!m_da.PozExists(p.obozn, p.num_kod))
                    {
                        PozObject poz = m_da.AddRootPoz(p);
                        if (poz != null)
                            m_view.AddRootNode(poz);
                        return;
                    }
                    else
                        goto dlg;

                }
            }
            if (e.src is MidObject && e.dst == null)
            {
                MidObject p = (e.src as MidObject).Clone();
                dlg: PozObject o = NewObject(ViewEvent.ButtonAction.AddRootPoz, p.obozn) as PozObject;
                if (o == null)
                    return;
                p.obozn = o.obozn;
                p.naimen = string.IsNullOrEmpty(o.naimen) ? p.naimen : o.naimen;
                p.descr = o.descr;
                if (!m_da.PozExists(p.obozn, p.num_kod))
                {
                    MidObject poz = m_da.AddRootMid(p);
                    if (poz != null)
                        m_view.AddMidNode(poz);
                    return;
                }
                else
                    goto dlg;
            }
        }
        private void m_view_ButtonActionEvent(object sender, ViewEvent.ButtonActionEventArgs e)
        {
            switch (e.Action)
            {

                case ViewEvent.ButtonAction.KeyDelete:
                    {
                        if (e.Target is PozObject)
                        {
                            int num_kod = Convert.ToInt32(e.Data);
                            PozObject o = e.Target as PozObject;
                            if (num_kod > 0)
                            {
                                string table = Utils.GetChildTable(num_kod);
                                if (string.IsNullOrEmpty(table))
                                    return;
                                if (Utils.Warning("Удалить позицию " + o.obozn + "?"))
                                {
                                    if (m_da.DeletePoz(table, o.id))
                                    {
                                        m_view.RemoveCurrentNode();
                                    }
                                }
                            }
                            else
                            {
                                if (Utils.Warning("Удалить позицию " + o.obozn + "?"))
                                {
                                    if (m_da.DeletePozRoot(o))
                                    {
                                        m_view.RemoveCurrentNode();
                                    }
                                }
                            }
                        }
                        if (e.Target is MidObject)
                        {
                            MidObject o = e.Target as MidObject;
                            if (Utils.Warning("Удалить позицию " + o.obozn + "?"))
                            {
                                if (m_da.DeleteMidRoot(o))
                                {
                                    m_view.RemoveCurrentNode();
                                }
                            }
                        }
                    }
                    break;
                case ViewEvent.ButtonAction.KeyUpdate:
                    {
                        UpdateFill(e.Target);
                    }
                    break;
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
            if (sender is SelectMidParentDialog)
            {
                SelectMidParentDialog view = sender as SelectMidParentDialog;
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
        private PozObject NewObject(ViewEvent.ButtonAction action, string name = "")
        {
            AddObjectDialog dlg = new AddObjectDialog(action, name);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                return dlg.PozObject;
            }
            return null;
        }
        private PozObject NewObject(ViewEvent.ButtonAction action, int num_kod, string name = "")
        {
            AddObjectDialog dlg = new AddObjectDialog(action, num_kod, name);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                return dlg.PozObject;
            }
            return null;
        }
    }
}
