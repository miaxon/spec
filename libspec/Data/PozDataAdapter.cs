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
        public bool DeletePoz(string table, PozObject o)
        {
            string query = string.Format("delete from {0} where id={1}", table, o.id);
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
        public bool DeletePozRoot(string table, PozObject o)
        {
            int ret = 0;
            string query = null;
            MySqlCommand cmd = null;
            string child_table = Utils.GetChildTable(o.num_kod);
            if(string.IsNullOrEmpty(child_table))
                return false;
            UInt32 refid = o.refid == 0 ? o.id : o.refid;
            query = string.Format("delete from {0} where parent = {1}", child_table, refid);
            cmd = new MySqlCommand(query, m_conn);
            try
            {
                ret = cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Failed to delete doc (2): " + ex.Message);
                return false;
            }

            query = string.Format("delete from {0} where id = {1}", table, o.id);
            cmd = new MySqlCommand(query, m_conn);
            try
            {
                ret = cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Failed to delete doc (1): " + ex.Message);
                return false;
            }
            if (table == "lid") // remove lid refernce from _did if exists
            {
                query = string.Format("delete from _did where uid = {0}", o.id);
                cmd = new MySqlCommand(query, m_conn);
                try
                {
                    ret = cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Failed to delete doc (3): " + ex.Message);
                    return false;
                }
            }
            return ret != 0;
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
