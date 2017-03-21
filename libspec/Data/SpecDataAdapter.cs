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

    }
}
