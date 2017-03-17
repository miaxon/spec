using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using libspec.ViewItem;
using libspec.Objects;
using libspec.ViewEvent;

namespace libspec.Contrlos
{
    public partial class SpecPozVew : UserControl
    {
        #region events
        public event EventHandler<ExpandEventArgs> RaskrEvent;
        public event EventHandler<SearchEventArgs> SearchEvent;
        #endregion
        public SpecPozVew()
        {
            InitializeComponent();
        }
        public void AddPoz(PozObject o)
        {
            dgView.Rows.Add(new SpecDataGridViewRow(o, dgView.Rows.Count + 1));
        }
        public void Clear()
        {
            dgView.Rows.Clear();
        }

        private void dgView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgView.Columns[e.ColumnIndex].Equals(c_img))
            {
                SpecDataGridViewRow row = dgView.Rows[e.RowIndex] as SpecDataGridViewRow;
                if (row.Object.num_kod < 9 && RaskrEvent != null)
                    RaskrEvent(this, new ExpandEventArgs(row.Object));
            }
        }

        private void dgView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgView.Columns[e.ColumnIndex].Equals(c_obozn))
            {
                SpecDataGridViewRow row = dgView.Rows[e.RowIndex] as SpecDataGridViewRow;
                if (SearchEvent != null)
                    SearchEvent(this, new SearchEventArgs("obozn", row.Cells[e.ColumnIndex].Value.ToString(), row.Object.num_kod));
            }

            if (dgView.Columns[e.ColumnIndex].Equals(c_gost))
            {
                SpecDataGridViewRow row = dgView.Rows[e.RowIndex] as SpecDataGridViewRow;
                if (SearchEvent != null)
                    SearchEvent(this, new SearchEventArgs("gost", row.Cells[e.ColumnIndex].Value.ToString(), row.Object.num_kod));
            }
        }

    }
}
