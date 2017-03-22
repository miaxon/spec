using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using libspec.View.Objects;
using libspec.View.Data;
namespace libspec.View
{
    public class SpecMain
    {
        private SpecDataAdapter m_da;
        //private SpecView se;
        private SpecViewTree se;
        private SpecModel m_tree_model;
        public SpecMain(Control site)
        {
            if (site == null)
                return;
            if (Utils.CheckAccess())
            {   
                m_da = new SpecDataAdapter();
                Utils.InitMaps();

                se = new SpecViewTree();
                se.Dock = DockStyle.Fill;
                site.Controls.Add(se);
                //
                
                //
                m_tree_model = new SpecModel(se, m_da);
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
