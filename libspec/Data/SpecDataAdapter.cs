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
        //private string m_conn_string = "server=debian;User Id=dmsadmin;password=98130777;database=spec;Integrated Security=False;Allow User Variables=True;Allow Zero Datetime=True;Character Set=cp1251;Convert Zero Datetime=True";
        private string m_conn_string = "server=" + Utils.Server + ";User Id=dms;password=21061972;database=spec;Allow User Variables=True;Allow Zero Datetime=True;Character Set=utf8;Convert Zero Datetime=True";
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
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (ex.Number == 1062)
                    MessageBox.Show("Значение уже существет.");
                else
                    MessageBox.Show("MySqlError: " + ex.Message);
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
                MessageBox.Show("Failed to populate projects list: " + ex.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
            }
            return list;
        }
        
        
    }
}
