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
                Utils.DBError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
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
            string query = string.Format(CultureInfo.InvariantCulture, "select id, obozn, naimen, descr, closed from _gid where deleted='N' and parent = {0} order by obozn", parent);
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
                Utils.DBError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
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
            string query = string.Format(CultureInfo.InvariantCulture, "select _did.id, lid.obozn, lid.naimen, lid.descr, _did.closed, _did.num_kol, _did.uid from _did inner join lid on _did.uid=lid.id where _did.parent = {0} order by lid.obozn", parent);
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
                Utils.DBError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
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
            string query = string.Format(CultureInfo.InvariantCulture, "select refid, num_kol, num_kod, id, num_kfr from {0} where parent = {1} order by num_kod", table, refid);
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
                Utils.DBError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
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
            UInt32 refid = o.refid == 0 ? o.id : o.refid;
            string query = string.Format(CultureInfo.InvariantCulture, "select obozn, naimen, descr, kei, marka, gost from {0} where id = {1}", table, refid);
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
                Utils.DBError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            finally
            {
                if (reader != null) reader.Close();
            }

        }
        private void FillGostForCode9(PozObject o)
        {
            MySqlDataReader reader = null;
            string query = string.Format(CultureInfo.InvariantCulture, "select mid2.gost from mid2, mid3 where mid2.id=mid3.parent and mid3.id = {0}", o.refid);
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
                Utils.DBError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }
    }
}