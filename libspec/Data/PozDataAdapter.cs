using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;
using libspec.View.Objects;
namespace libspec.View.Data
{
    public partial class SpecDataAdapter
    {
        public bool AddPoz(DocObject target, PozObject poz)
        {
            UInt32 refid = poz.refid == 0 ? poz.id : poz.refid;
            string query = string.Format("insert into lid_old (parent, num_kod, num_kol, num_kfr, refid) values({0}, {1}, {2}, {3}, {4})",
                                           target.refid,
                                           poz.num_kod,
                                           1,
                                           poz.num_kfr,
                                           refid);
            MySqlCommand cmd = new MySqlCommand(query, m_conn);
            int ret = 0;
            try
            {
                ret = cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Failed to add poz: " + ex.Message);
                return false;
            }
            return ret > 0;
        }
        public bool AddPoz(PozObject target, PozObject poz)
        {
            UInt32 refid = poz.refid == 0 ? poz.id : poz.refid;
            string table = Utils.GetChildTable(target.num_kod);
            if (string.IsNullOrEmpty(table))
                return false;
            string query = string.Format("insert into {0} (parent, num_kod, num_kol, num_kfr, refid) values({1}, {2}, {3}, {4}, {5})",
                                           table,
                                           target.id,
                                           poz.num_kod,
                                           1,
                                           poz.num_kfr,
                                           refid);
            MySqlCommand cmd = new MySqlCommand(query, m_conn);
            int ret = 0;
            try
            {
                ret = cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Failed to add poz: " + ex.Message);
                return false;
            }
            return ret > 0;
        }
        public bool DeletePoz(PozObject o)
        {
            string query = string.Format("delete from lid_old where id={0}", o.id);
            MySqlCommand cmd = new MySqlCommand(query, m_conn);
            int ret = 0;
            try
            {
                ret = cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Failed to add poz: " + ex.Message);
                return false;
            }
            return ret > 0;
        }
        public bool UpdatePoz(PozObject o)
        {
            string query = string.Format("update lid_old set num_kol = {0}, num_kfr = {1} where id={2}", o.num_kol, o.num_kfr, o.id);
            MySqlCommand cmd = new MySqlCommand(query, m_conn);
            int ret = 0;
            try
            {
                ret = cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Failed to add poz: " + ex.Message);
                return false;
            }
            return ret > 0;
        }

        public bool MovePoz(PozObject poz, DocObject doc)
        {
            string query = string.Format("update lid_old set parent={0} where id={1}", doc.refid, poz.id);

            MySqlCommand cmd = new MySqlCommand(query, m_conn);
            int ret = 0;
            try
            {
                ret = cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Failed to add poz: " + ex.Message);
                return false;
            }
            return ret > 0;
        }
    }
}
