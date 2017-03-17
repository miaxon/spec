using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using libspec.Objects;
using libspec.ViewItem;
using libspec.Dialogs;

namespace libspec
{
    public class SpecModel
    {
        private SpecDataAdapter m_da;
        private SpecViewTree m_view;
        
        public SpecModel(SpecViewTree view, SpecDataAdapter da)
        {
            m_da = da;
            m_view = view;
            m_view.FillTree(m_da.GetProjectList());
            AddListeners();
        }
        
        private void AddListeners()
        {
            m_view.NodeClickEvent += new EventHandler<ViewEvent.NodeClickEventArgs>(m_view_NodeClick);
            m_view.ExpandEvent += new EventHandler<ViewEvent.ExpandEventArgs>(m_view_ExpandEvent);
            m_view.ButtonActionEvent += new EventHandler<ViewEvent.ButtonActionEventArgs>(m_view_ButtonActionEvent);
            
        }

        private void m_view_ButtonActionEvent(object sender, ViewEvent.ButtonActionEventArgs e)
        {            
            AddObjectDialog dlg = new AddObjectDialog(e.Action);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                BaseObject o = dlg.Object;
                switch(e.Action)
                {
                    case ViewEvent.ButtonAction.AddProject:
                        o = m_da.AddProject(o);                        
                        break;
                    case ViewEvent.ButtonAction.AddGroup:
                        break;
                    case ViewEvent.ButtonAction.AddDoc:
                        break;
                }
                if (o!= null && o.id > 0)
                    m_view.AddObject(o);
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
            //m_view_tree.FillPozView(list);
        }

        void m_view_ExpandEvent(object sender, ViewEvent.ExpandEventArgs e)
        {
            UInt32 refid = e.Object.refid == 0 ? e.Object.id : e.Object.refid;
            List<PozObject> list = m_da.GetPozList(Utils.GetChildTable(e.Object.num_kod), refid);
            m_view.FillPoz(list);

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
