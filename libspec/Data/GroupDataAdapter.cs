using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;
using libspec.Objects;
namespace libspec.Data
{
    public partial class SpecDataAdapter
    {
        public GroupObject AddGroup(GroupObject o, UInt32 parent)
        {
            string query = string.Format("insert into _gid (obozn, naimen, descr, parent) values('{0}', '{1}', '{2}', {3})", o.obozn, o.naimen, o.descr, parent);
            MySqlCommand cmd = new MySqlCommand(query, m_conn);
            try
            {
                int r = cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                    MessageBox.Show("Failed to add object: name exists!");
                else
                    MessageBox.Show("Failed to add project: " + ex.Message);
            }
            return GroupByObozn(o.obozn, parent);
        }
        public bool SetStatusGroup(ref GroupObject o, Closed status)
        {
            string query = null;
            MySqlCommand cmd = null;
            int r = 0;
            query = string.Format("update _gid set closed='{0}' where id = {1}", status, o.id);
            cmd = new MySqlCommand(query, m_conn);
            try
            {
                r = cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Failed to delete doc (2): " + ex.Message);
                return false;
            }
            if (r > 0)
                o.status = status;
            return r > 0;
        }
        public bool DeleteGroup(GroupObject o)
        {
            int r = 0;
            string query = string.Format("select count(*) from _did where parent = {0}", o.id);
            MySqlCommand cmd = new MySqlCommand(query, m_conn);
            try
            {
                r = cmd.ExecuteNonQuery();
                if (r > 0)
                    return false;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Failed to populate projects list: " + ex.Message);
                return false;
            }

            query = string.Format("delete from _gid where id = {0}", o.id);
            cmd = new MySqlCommand(query, m_conn);
            try
            {
                r = cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Failed to populate projects list: " + ex.Message);
                return false;
            }
            return r > 0;
        }
        public void UpdateGroup(GroupObject o)
        {
            string query = string.Format("update _pid set obozn = '{0}', naimen = '{1}', descr = '{2}', closed= '{3}' where id = {4}", o.obozn, o.naimen, o.descr, o.status, o.id);
            MySqlCommand cmd = new MySqlCommand(query, m_conn);
            try
            {
                int r = cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Failed to populate projects list: " + ex.Message);
            }
            finally
            {

            }
        }
        public GroupObject GroupByObozn(string obozn, UInt32 parent)
        {
            MySqlDataReader reader = null;
            GroupObject ret = null;
            string query = string.Format("select id, obozn, naimen, descr, closed from _gid where obozn='{0}' and parent={1}", obozn, parent);
            MySqlCommand cmd = new MySqlCommand(query, m_conn);
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    ret = new GroupObject(values);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Failed to populate list: " + ex.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
            }
            return ret;

        }
    }
}