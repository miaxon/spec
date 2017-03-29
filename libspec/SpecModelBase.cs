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
            if (e.num_kod < 9)
            {
                string name = "";
            dlg: PozObject o = NewObject(ViewEvent.ButtonAction.AddRootPoz, name) as PozObject;
                if (o == null)
                    return;
                o.num_kod = e.num_kod;
                name = o.obozn;
                if (!m_da.PozExists(o))
                {
                    PozObject poz = m_da.AddRootPoz(o);
                    if (poz != null)
                        m_view.AddRootNode(poz);
                    return;
                }
                else
                    goto dlg;
            }
            else
            {
                string name = "";
            dlg: PozObject o = NewObject(ViewEvent.ButtonAction.AddRootPoz, name) as PozObject;
                if (o == null)
                    return;
                MidObject p = new MidObject(e.num_kod);
                p.obozn = o.obozn;
                p.naimen = o.naimen;
                p.descr = o.descr;

                if (!m_da.MidExists(p))
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
                view.UpdateMidNode(o);
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
                view.UpdateMidNode(o);
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
                    m_da.AddPoz(src, dst);
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
                    if (!m_da.PozExists(p))
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
            if (e.src is MidObject)
            {
                MidObject src = e.src as MidObject;
                if (e.dst == null)
                {
                dlg: PozObject o = NewObject(ViewEvent.ButtonAction.AddRootPoz, src.obozn) as PozObject;
                    if (o == null)
                        return;
                    MidObject p = new MidObject(src.num_kod);
                    p.obozn = o.obozn;
                    p.naimen = string.IsNullOrEmpty(o.naimen) ? src.naimen : o.naimen;
                    p.descr = o.descr;

                    if (!m_da.MidExists(p))
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
                                if (MessageBox.Show("Удалить позицию " + o.obozn + "?", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                                {
                                    if (m_da.DeletePozRoot(o))
                                    {
                                        m_view.RemoveNode(e.Target);
                                    }
                                }

                            }
                        }
                        if (e.Target.Tag is MidObject)
                        {
                            MidObject o = e.Target.Tag as MidObject;
                            if (MessageBox.Show("Удалить позицию " + o.obozn + "?", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                            {
                                if (m_da.DeleteMidRoot(o))
                                {
                                    m_view.RemoveNode(e.Target);
                                }
                            }
                        }
                    }
                    break;
                case ViewEvent.ButtonAction.KeyUpdate:
                    {
                        UpdateFill(e.Target.Tag);
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
    }
}
