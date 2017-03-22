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
    }
}