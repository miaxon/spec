using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;
using libspec.View.Objects;
using System.Globalization;
namespace libspec.View.Data
{
    public partial class SpecDataAdapter
    {
        public ProjectObject AddProject(ProjectObject o)
        {
            string query = string.Format(CultureInfo.InvariantCulture, "insert into _pid (obozn, naimen, descr) values('{0}', '{1}', '{2}')", o.obozn, o.naimen, o.descr);
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
            return ProjectByObozn(o.obozn);
        }
        public void DeleteProject(ProjectObject o)
        {
            string query = string.Format(CultureInfo.InvariantCulture, "delete from _pid where id = {0}", o.id);
            MySqlCommand cmd = new MySqlCommand(query, m_conn);
            try
            {
                int r = cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Failed to populate projects list: " + ex.Message);
            }            
        }
        public void UpdateProject(ProjectObject o)
        {
            string query = string.Format(CultureInfo.InvariantCulture, "update _pid set obozn = '{0}', naimen = '{1}', descr = '{2}', closed= '{3}' where id = {4}", o.obozn, o.naimen, o.descr, o.status, o.id);
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
        public ProjectObject ProjectByObozn(string obozn)
        {
            MySqlDataReader reader = null;
            ProjectObject ret = null;
            string query = string.Format(CultureInfo.InvariantCulture, "select id, obozn, naimen, descr, closed from _pid where obozn='{0}'", obozn);
            MySqlCommand cmd = new MySqlCommand(query, m_conn);
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    ret = new ProjectObject(values);
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
        public bool ProjectExists(string obozn)
        {
            string query = string.Format(CultureInfo.InvariantCulture, "select * from _pid where obozn='{0}'", obozn);
            MySqlCommand cmd = new MySqlCommand(query, m_conn);
            MySqlDataReader reader = null;
            int ret = 0;
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ret++;
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Failed to add project: " + ex.Message);
                return false;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
            return ret > 0;
        }
    }
}