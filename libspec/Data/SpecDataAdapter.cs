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
        //private string m_conn_string = "server=debian;User Id=dmsadmin;password=98130777;database=spec;Integrated Security=False;Allow User Variables=True;Allow Zero Datetime=True;Character Set=cp1251;Convert Zero Datetime=True";
        private string m_conn_string = "server=192.168.255.251;User Id=dms;password=21061972;database=spec;Allow User Variables=True;Allow Zero Datetime=True;Character Set=utf8;Convert Zero Datetime=True";
        private MySqlConnection m_conn;
        public SpecDataAdapter()
        {
            Connect();            
        }
        
        public void Connect()
        {
            try
            {
                m_conn = new MySqlConnection(m_conn_string);
                m_conn.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Disconnect()
        {
            try
            {
                if (m_conn != null)
                    m_conn.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void InsertProject()
        {
            ProjectObject o = new ProjectObject();
            string query = string.Format("insert into _pid (obozn, naimen, descr) values('{0}', '{1}', '{2}')", o.obozn, o.naimen, o.descr);
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
        public void DeleteProject(ProjectObject o)
        {
            string query = string.Format("delete from _pid where id = {0}", o.id);
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
        public void UpdateProject(ProjectObject o)
        {
            string query = string.Format("update _pid set obozn = '{0}', naimen = '{1}', descr = '{2}' where id = {3}", o.obozn, o.naimen, o.descr, o.id);
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
        

        

        

        

        
        
    }
}
