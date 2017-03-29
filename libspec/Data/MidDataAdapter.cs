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
        public List<MidObject> SearchMid1Bad()
        {
            List<MidObject> list = new List<MidObject>();
            string query = @"select id, obozn, naimen, descr, parent from mid1 where 
                             parent=0 or 
                             obozn not like '0%' or
                             naimen = 'не задано' or
                             length(naimen) = 0 or 
                             length(obozn) != 4 
                             order by obozn";
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
                    MidObject o = new MidObject(values, 93);                    
                    list.Add(o);
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
            foreach(MidObject o in list)
                o.num_kol = GetMidChilds(o.id, o.num_kod);
            return list;
        }
        public List<PozObject> SearchMid2Bad()
        {
            List<PozObject> list = new List<PozObject>();
            string query = @"select refid, num_kol, num_kod, id, num_kfr, obozn, naimen, descr, kei, marka, gost from mid2 where 
                             parent=0 or 
                             obozn not like '0%' or 
                             length(obozn) != 6 or
                             naimen = 'не задано' or
                             length(naimen) = 0 
                             order by obozn";
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
                Utils.DBError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            finally
            {
                if (reader != null) reader.Close();
            }
            foreach (PozObject o in list)
                o.num_kol = GetMidChilds(o.id, o.num_kod);
            return list;
        }
        public List<PozObject> SearchMid3Bad()
        {
            List<PozObject> list = new List<PozObject>();
            string query = @"select refid, num_kol, num_kod, id, num_kfr, obozn, naimen, descr, kei, marka, gost from mid3 where 
                             parent=0 or 
                             obozn not like '0%' or 
                             length(obozn) != 11 
                             order by obozn";

            //naimen = 'не задано' or
            //length(naimen) = 0 or 
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
            return list;
        }
        private UInt32 GetMidParent(string obozn, int num_kod)
        {            
            string parent_table = Utils.GetParentTable(num_kod);
            if (string.IsNullOrEmpty(parent_table))
                return 0;
            int len = Utils.OboznLength(parent_table);
            string srch = obozn.Substring(0, len);
            string query = string.Format(CultureInfo.InvariantCulture, "select id from {0} where obozn = '{1}'", parent_table, srch);
            MySqlCommand cmd = new MySqlCommand(query, m_conn);
            MySqlDataReader reader = null;
            UInt32 id = 0;
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetUInt32(0);
                }
            }
            catch (MySqlException ex)
            {
                Utils.DBError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return 0;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
            
            if (id == 0 && parent_table == "mid0")
            {
                query = string.Format(CultureInfo.InvariantCulture, "select id from {0} where locate('{1};', descr) > 0", parent_table, srch);
                cmd = new MySqlCommand(query, m_conn);
                reader = null;                
                try
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        id = reader.GetUInt32(0);
                    }
                }
                catch (MySqlException ex)
                {
                    Utils.DBError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    return 0;
                }
                finally
                {
                    if (reader != null) reader.Close();
                }
            }
            if (id == 0)
            {
                Utils.Error("Не удалось найти родительский объект для " + obozn);
            }
            return id;
        }
        public bool MidExists(MidObject o)
        {
            string table = Utils.GetTable(o.num_kod);
            if (string.IsNullOrEmpty(table))
                return false;
            string query = string.Format(CultureInfo.InvariantCulture, "select count(id) from {0} where obozn='{1}'", table, o.obozn);
            MySqlCommand cmd = new MySqlCommand(query, m_conn);
            MySqlDataReader reader = null;
            int ret = 0;
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ret = reader.GetInt32(0);
                }

            }
            catch (MySqlException ex)
            {
                Utils.DBError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
            if (ret != 0)
                return true;
            if (o.parent == 0 && !string.IsNullOrEmpty(Utils.GetParentTable(o.num_kod)))
            {
                o.parent = GetMidParent(o.obozn, o.num_kod);
                if (o.parent == 0)
                    return true;
            }
            return ret > 0;
        }
        public MidObject AddRootMid(MidObject o)
        {
            UInt32 parent = o.parent;
            if (parent == 0)
            {
                parent = GetMidParent(o.obozn, o.num_kod);
                if (parent == 0)
                    return null;
                else
                    o.parent = parent;
            }
            string table = Utils.GetTable(o.num_kod);
            if (string.IsNullOrEmpty(table))
                return null;
            string query = string.Format(CultureInfo.InvariantCulture,
                                        "insert into {0} (obozn, naimen, descr, parent) values('{1}', '{2}', '{3}', {4})",
                                        table,
                                        o.obozn,
                                        o.naimen,
                                        o.descr,
                                        o.parent);
            MySqlCommand cmd = new MySqlCommand(query, m_conn);
            try
            {
                int r = cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Utils.DBError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return null;
            }
            query = string.Format(CultureInfo.InvariantCulture, "select id from {0} where obozn='{1}'", table, o.obozn);
            MySqlDataReader reader = null;
            cmd = new MySqlCommand(query, m_conn);
            UInt32 id = 0;
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetUInt32(0);
                }
            }
            catch (MySqlException ex)
            {
                Utils.DBError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return null;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
            if (id == 0)
                return null;
            o.SetRootId(id);
            return o;
        }
        public void SetMidPozParent(PozObject o)
        {            
            UInt32 parent_id = GetMidParent(o.obozn, o.num_kod);
            if (parent_id == 0)
                return;
            string table = Utils.GetTable(o.num_kod);
            if (string.IsNullOrEmpty(table))
                return;
            string query = string.Format(CultureInfo.InvariantCulture, "update {0} set parent = {1} where id={2}", table, parent_id, o.id);
            MySqlCommand cmd = new MySqlCommand(query, m_conn);
            int ret = 0;
            try
            {
                ret = cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Utils.DBError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return;
            }
            if (ret == 0)
            {
                Utils.Error("Не удалось установить свойство parent для " + o.obozn);
                return;
            }            
        }
        public bool DeleteMidRoot(MidObject o)
        {            
            int childs = GetMidChilds(o.id, o.num_kod);
            if (childs > 0)
            {
                Utils.Error("Удаляемый объект имеет связанные записи в дочерней таблице.");
                return false;
            }
            string table = Utils.GetTable(o.num_kod);
            if (string.IsNullOrEmpty(table))
                return false;
            string query = string.Format(CultureInfo.InvariantCulture, "delete from {0} where id={1}", table, o.id);
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
        private int GetMidChilds(UInt32 id, int num_kod)
        {
            string child_table = Utils.GetChildMidTable(num_kod);
            if (string.IsNullOrEmpty(child_table))
                return 0;
            string query = string.Format(CultureInfo.InvariantCulture, "select count(id) from {0} where parent={1}", child_table, id);
            MySqlCommand cmd = new MySqlCommand(query, m_conn);
            MySqlDataReader reader = null;
            int ret = 0;
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ret = reader.GetInt32(0);
                }

            }
            catch (MySqlException ex)
            {
                Utils.DBError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return 0;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
            return ret;
        }
    }
}