﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using libspec.Objects;

namespace libspec
{
    public class SpecMain
    {
        private SpecDataAdapter m_da;
        private SpecView se;
        private SpecModel m_tree_model;
        public SpecMain(Control site)
        {
            if (site == null)
                return;
            Utils.InitMaps();
            se = new SpecView();
            se.Dock = DockStyle.Fill;
            site.Controls.Add(se);
            //
            m_da = new SpecDataAdapter();
            //
            m_tree_model = new SpecModel(se, m_da); 
            
            

        }
    }
}
