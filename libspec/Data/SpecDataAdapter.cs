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
        private string template = "server={0};User Id={1};password={2};database=spec;Allow User Variables=True;Allow Zero Datetime=True;Character Set=utf8;Convert Zero Datetime=True";
        private string m_conn_string;
        private MySqlConnection m_conn;
        
        public SpecDataAdapter()
        {
            Connect();
        }

        public void Connect()
        {
            m_conn_string = string.Format(template, Utils.Server, Utils.User, Utils.Password);
            try
            {
                m_conn = new MySqlConnection(m_conn_string);
                m_conn.Open();
            }
            catch (MySqlException ex)
            {
                Utils.DBError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return;
            }
            if (m_conn.State == ConnectionState.Open)
            {
                Utils.KidList = GetKidList();
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
                Utils.DBError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        public bool ExecQuery(string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, m_conn);
            int ret = 0;
            try
            {
                ret = cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Utils.DBError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
            return ret > 0;
        }
        public List<KeiObject> GetKidList()
        {
            MySqlDataReader reader = null;
            List<KeiObject> list = new List<KeiObject>();
            MySqlCommand cmd = new MySqlCommand("select id, obozn, naimen from kid order by obozn", m_conn);
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];

                    reader.GetValues(values);
                    //Type t = values[4].GetType();
                    list.Add(new KeiObject(values));
                }
            }
            catch (MySqlException ex)
            {
                Utils.DBError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            finally
            {
                if (reader != null) reader.Close();
            }
            return list;
        }
        
        
    }
}
