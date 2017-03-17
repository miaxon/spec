using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;
using libspec.Objects;
namespace libspec
{
    public class SpecDataAdapter
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
        public List<ProjectObject> GetProjectList()
        {
            MySqlDataReader reader = null;
            List<ProjectObject> list = new List<ProjectObject>();
            MySqlCommand cmd = new MySqlCommand("select id, obozn, naimen, descr, closed from _pid order by obozn", m_conn);
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];

                    reader.GetValues(values);
                    //Type t = values[4].GetType();
                    list.Add(new ProjectObject(values));
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Failed to populate projects list: " + ex.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
            }
            return list;
        }

        public List<GroupObject> GetGroupList(UInt32 parent)
        {
            MySqlDataReader reader = null;
            List<GroupObject> list = new List<GroupObject>();
            string query = string.Format("select id, obozn, naimen, descr, closed from _gid where deleted='N' and parent = {0} order by obozn", parent);
            MySqlCommand cmd = new MySqlCommand(query, m_conn);
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    list.Add(new GroupObject(values));
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Failed to populate group list: " + ex.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
            }
            return list;
        }

        public List<DocObject> GetDocList(UInt32 parent)
        {
            MySqlDataReader reader = null;
            List<DocObject> list = new List<DocObject>();
            string query = string.Format("select _did.id, lid.obozn, lid.naimen, lid.descr, _did.closed, _did.num_kol, _did.uid from _did inner join lid on _did.uid=lid.id where _did.parent = {0} order by lid.obozn", parent);
            MySqlCommand cmd = new MySqlCommand(query, m_conn);
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    //Type t = values[5].GetType();
                    list.Add(new DocObject(values));
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
            return list;
        }

        public List<PozObject> GetPozList(string table, UInt32 refid)
        {
            MySqlDataReader reader = null;
            List<PozObject> list = new List<PozObject>();
            string query = string.Format("select refid, num_kol, num_kod, id, num_kfr from {0} where parent = {1} order by num_kod", table, refid);
            /*
             * refid   - num_refid
             * num_kol - num_kol
             * num_kod - num_kod
             * id      - num_poz_id
             * num_kfr - num_kfr
             * 
            */
            MySqlCommand cmd = new MySqlCommand(query, m_conn);
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    //Type t = values[1].GetType();
                    list.Add(new PozObject(values));
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Failed to populate poz list: " + ex.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
            }
            foreach (PozObject o in list)
                GetPozReference(o);
            foreach (PozObject o in list.FindAll(o => o.num_kod == 9))
                FillGostForCode9(o);
            return list;
        }
        private void GetPozReference(PozObject o)
        {
            MySqlDataReader reader = null;
            string table = Utils.GetTable(o.num_kod);
            string query = string.Format("select obozn, naimen, descr, kei, marka, gost from {0} where id = {1}", table, o.refid);
            MySqlCommand cmd = new MySqlCommand(query, m_conn);
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    //Type t = values[1].GetType();
                    o.FillReference(values);                    
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Failed to populate poz reference list: " + ex.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
            }
           
        }

        private void FillGostForCode9(PozObject o)
        {
            MySqlDataReader reader = null;
            string query = string.Format("select mid2.gost from mid2, mid3 where mid2.id=mid3.parent and mid3.id = {0}", o.refid);
            MySqlCommand cmd = new MySqlCommand(query, m_conn);
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    o.gost = (string)reader.GetString(0); 
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Failed to populate gost reference list: " + ex.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }

        public List<PozObject> SearchPoz(string table, string field, string searchString)
        {
            MySqlDataReader reader = null;
            List<PozObject> list = new List<PozObject>();
            string query = string.Format("select refid, num_kol, num_kod, id, num_kfr, obozn, naimen, descr, kei, marka, gost from {0} where {1} like '{2}%'", table, field, searchString);
            MySqlCommand cmd = new MySqlCommand(query, m_conn);
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    //Type t = values[0].GetType();
                    list.Add(new PozObject(values));
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Failed to populate gost reference list: " + ex.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
            }
            return list;
        }
        public void AddProject()
        {
            string query = string.Format("insert into _pid (obozn, naimen, descr) values ('новый проект', 'не задано', '{0}')", DateTime.Now.ToShortDateString() );
            MySqlCommand cmd = new MySqlCommand(query, m_conn);
            try
            {
                int r = cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                    MessageBox.Show("Failed to add project: project name exist!");
                else
                    MessageBox.Show("Failed to add project: " + ex.Message);
            }
            

        }
    }
}
