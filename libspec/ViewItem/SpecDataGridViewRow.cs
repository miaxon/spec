using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using libspec.Objects;

namespace libspec.ViewItem
{
    public class SpecDataGridViewRow : DataGridViewRow
    {
        private PozObject m_o;
        public PozObject Object { get { return m_o; } set { m_o = value; if (m_o != null) Update(); } }

        
        private DataGridViewImageCell img = new DataGridViewImageCell(false);
        private DataGridViewTextBoxCell poz = new DataGridViewTextBoxCell();
        private DataGridViewTextBoxCell obozn = new DataGridViewTextBoxCell();
        private DataGridViewTextBoxCell gost = new DataGridViewTextBoxCell();
        private DataGridViewTextBoxCell naimen = new DataGridViewTextBoxCell();
        private DataGridViewTextBoxCell marka = new DataGridViewTextBoxCell();
        private DataGridViewTextBoxCell num_kei = new DataGridViewTextBoxCell();
        private DataGridViewTextBoxCell num_kfr = new DataGridViewTextBoxCell();
        private DataGridViewTextBoxCell num_kol = new DataGridViewTextBoxCell();
        private DataGridViewTextBoxCell num_knr = new DataGridViewTextBoxCell();
        public SpecDataGridViewRow(PozObject o, int n)
            : base()
        {
            this.Cells.AddRange(new DataGridViewCell[] {img, poz, obozn, gost, naimen, marka, num_kei, num_kol, num_kfr, num_knr}); 
            m_o = o;
            img.Value = Utils.GetPozImage(m_o.num_kod);
            img.ToolTipText = Utils.NumKodString(m_o.num_kod);
            poz.Value = n.ToString();
            Update();
        }

        private void Update()
        {            
            obozn.Value = m_o.obozn;
            gost.Value = m_o.gost;
            naimen.Value = m_o.naimen;
            marka.Value = m_o.marka;
            num_kei.Value = m_o.kei;
            num_kol.Value = m_o.num_kol;
            num_kfr.Value = m_o.num_kfr;
            num_knr.Value = m_o.num_knr;         
        }
    }
}
