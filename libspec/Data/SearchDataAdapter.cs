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
        public List<PozObject> SearchPoz(string table, string field, string searchString)
        {
            List<PozObject> list = new List<PozObject>();
            string query = string.Format(CultureInfo.InvariantCulture, "select refid, num_kol, num_kod, id, num_kfr, obozn, naimen, descr, kei, marka, gost from {0} where {1} like '{2}%'", table, field, searchString);
            MySqlCommand cmd = new MySqlCommand(query, m_conn);
            MySqlDataReader reader = null;
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    //Type t = values[0].GetType();
                    PozObject o = new PozObject(values);
                    list.Add(o);
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
            foreach (PozObject o in list)
            {
                if (o.num_kod > 90 && o.num_kod < 100)
                {
                    o.num_kol = GetMidChilds(o.id, o.num_kod);
                }
            }

            return list;
        }
        public List<MidObject> SearchMid0()
        {
            MySqlDataReader reader = null;
            List<MidObject> list = new List<MidObject>();
            string query = string.Format(CultureInfo.InvariantCulture, "select id, obozn, naimen, descr from mid0 order by naimen");
            MySqlCommand cmd = new MySqlCommand(query, m_conn);
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    //Type t = values[0].GetType();
                    list.Add(new MidObject(values, 94));
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
            foreach (MidObject o in list)
            {
                o.num_kol = GetMidChilds(o.id, o.num_kod);
            }
            return list;
        }
        public List<MidObject> SearchMid1(string searchString)
        {
            List<MidObject> list = new List<MidObject>();
            string query = string.Format(CultureInfo.InvariantCulture, "select id, obozn, naimen, descr, parent from mid1 where obozn like '{0}%' order by obozn", searchString);
            MySqlCommand cmd = new MySqlCommand(query, m_conn);
            MySqlDataReader reader = null;
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    //Type t = values[0].GetType();
                    list.Add(new MidObject(values, 93));
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
            foreach (MidObject o in list)
            {
                o.num_kol = GetMidChilds(o.id, o.num_kod);
            }
            return list;
        }
    }
}