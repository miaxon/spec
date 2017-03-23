using libspec.View.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace libspec.View
{
    public class SpecMainBase
    {
        private SpecDataAdapter m_da;
        private SpecViewTable m_view;
        private SpecModelBase m_model;
        public SpecMainBase(Control site)
        {
            if (site == null)
                return;
            if (Utils.CheckAccess())
            {
                m_da = new SpecDataAdapter();
                Utils.InitMaps();
                m_view = new SpecViewTable();
                m_view.Dock = DockStyle.Fill;
                site.Controls.Add(m_view);
                m_model = new SpecModelBase(m_view, m_da);
            }
            else
            {
                Label l = new Label();
                site.Controls.Add(l);
                l.Dock = DockStyle.Fill;
                l.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                l.Font = new System.Drawing.Font("Arial", 34);
                l.Text = "Доступ не разрешен.";

            }

        }
    }
}
