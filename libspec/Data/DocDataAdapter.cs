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
        public DocObject AddDoc(DocObject o, UInt32 parent)
        {
            string query = string.Format("insert into lid (obozn, descr) values('{0}', '{1}')", o.obozn, o.descr);
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
                return null;
            }
            UInt32 uid = DocIdByObozn(o.obozn);
            if (uid == 0)
                return null;
            query = string.Format("insert into _did (parent, uid) values({0}, {1})", parent, uid);
            cmd = new MySqlCommand(query, m_conn);
            try
            {
                int r = cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Failed to add project: " + ex.Message);
                return null;
            }
            return DocByObozn(o.obozn);
        }

        public bool SetStatusDoc(ref DocObject o, Closed status)
        {
            string query = null;
            MySqlCommand cmd = null;
            int r = 0;
            query = string.Format("update _did set closed='{0}' where id = {1}", status, o.id);
            cmd = new MySqlCommand(query, m_conn);
            try
            {
                r = cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Failed to open/clode doc : " + ex.Message);
                return false;
            }
            if (r > 0)
                o.status = status;
            return r > 0;
        }
        public bool DeleteDoc(DocObject o)
        {
            int ret = 0;
            string query = null;
            MySqlCommand cmd = null;
            
            query = string.Format("delete from lid_old where parent = {0}", o.refid);
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

            query = string.Format("delete from lid where id = {0}", o.refid);
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

            query = string.Format("delete from _did where id = {0}", o.id);
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
            return ret != 0;
        }
        public void UpdateDoc(DocObject o)
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
        private UInt32 DocIdByObozn(string obozn)
        {
            MySqlDataReader reader = null;
            UInt32 ret = 0;
            string query = string.Format("select id from lid where obozn='{0}'", obozn);
            MySqlCommand cmd = new MySqlCommand(query, m_conn);
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ret = reader.GetUInt32(0);
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
        private DocObject DocByObozn(string obozn)
        {
            string query = string.Format("select _did.id, lid.obozn, lid.naimen, lid.descr, _did.closed, _did.num_kol, _did.uid from _did inner join lid on _did.uid=lid.id where lid.obozn = '{0}'", obozn);
            MySqlDataReader reader = null;
            MySqlCommand cmd = new MySqlCommand(query, m_conn);
            DocObject ret = null;
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    //Type t = values[5].GetType();
                    ret = new DocObject(values);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Failed to populate doc list: " + ex.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
            }
            return ret;
        }
    }

}
