using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using libspec.Objects;
using libspec.ViewItem;

namespace libspec
{
    public class SpecModel
    {
        private SpecDataAdapter m_da;
        private SpecView m_view;
        
        public SpecModel(SpecView view, SpecDataAdapter da)
        {
            m_da = da;
            m_view = view;
            m_view.FillTree(m_da.GetProjectList());
            AddListeners();
        }

        private void AddListeners()
        {
            m_view.NodeClickEvent +=new EventHandler<ViewEvent.TreeNodeClickEventArgs>(m_view_NodeClick);
            m_view.RaskrEvent += new EventHandler<ViewEvent.RaskrEventArgs>(m_view_RaskrEvent);
            m_view.SearchEvent += new EventHandler<ViewEvent.SearchEventArgs>(m_view_SearchEvent);
        }

        void m_view_SearchEvent(object sender, ViewEvent.SearchEventArgs e)
        {
            string table = Utils.GetTable(e.num_kod);
            if (e.search_field == "gost")
                table = "mid2";
            if (e.search_string.Length == 11)
                table = "mid3";
            List<PozObject> list = m_da.SearchPoz(table, e.search_field, e.search_string);
            m_view.FillPozView(list);
        }

        void m_view_RaskrEvent(object sender, ViewEvent.RaskrEventArgs e)
        {
            List<PozObject> list = m_da.GetPozList(Utils.GetChildTable(e.Object.num_kod), e.Object.refid);
            m_view.FillPozView(list);

        }

        private void m_view_NodeClick(object sender, ViewEvent.TreeNodeClickEventArgs e)
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
