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
                MessageBox.Show("Failed to populate gost reference list: " + ex.Message);
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
                MessageBox.Show("Failed to populate gost reference list: " + ex.Message);
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
                MessageBox.Show("Failed to populate gost reference list: " + ex.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
            }
            return list;
        }
        private UInt32 GetMidParent(object o)
        {
            int num_kod = -1;
            string obozn = string.Empty;
            if (o is MidObject)
            {
                num_kod = (o as MidObject).num_kod;
                obozn = (o as MidObject).obozn;
            }
            if (o is PozObject)
            {
                num_kod = (o as PozObject).num_kod;
                obozn = (o as PozObject).obozn;
            }

            if (num_kod < 0)
                return 0;
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
                MessageBox.Show("Failed to search parent: " + ex.Message);
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
                    MessageBox.Show("Failed to search parent: " + ex.Message);
                    return 0;
                }
                finally
                {
                    if (reader != null) reader.Close();
                }
            }
            if (id == 0)
            {
                MessageBox.Show("Failed to search parent for " + obozn);
            }
            return id;
        }
        public bool MidExists(MidObject o)
        {
            string table = Utils.GetTable(o.num_kod);
            if (string.IsNullOrEmpty(table))
                return false;
            string query = string.Format(CultureInfo.InvariantCulture, "select * from {0} where obozn='{1}'", table, o.obozn);
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
            if (ret != 0)
                return true;
            if (o.parent == 0)
            {
                o.parent = GetMidParent(o);
                if (o.parent == 0)
                    return true;
            }
            return ret > 0;
        }
        public MidObject AddRootMid(MidObject target)
        {
            UInt32 parent = target.parent;
            if (parent == 0)
            {
                parent = GetMidParent(target);
                if (parent == 0)
                    return null;
                else
                    target.parent = parent;
            }
            string table = Utils.GetTable(target.num_kod);
            if (string.IsNullOrEmpty(table))
                return null;
            string query = string.Format(CultureInfo.InvariantCulture,
                                        "insert into {0} (obozn, naimen, descr, parent) values('{1}', '{2}', '{3}', {4})",
                                        table,
                                        target.obozn,
                                        target.naimen,
                                        target.descr,
                                        target.parent);
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
                return null;
            }
            query = string.Format(CultureInfo.InvariantCulture, "select id from {0} where obozn='{1}'", table, target.obozn);
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
                MessageBox.Show("Failed to add project: " + ex.Message);
                return null;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
            if (id == 0)
                return null;
            target.SetRootId(id);
            
            //SetMidParent(target);
            return target;
        }
        public void SetMidParent(object o)
        {
            int num_kod = -1;
            UInt32 id = 0;
            MidObject m = null;
            string obozn = string.Empty;
            if (o is MidObject)
            {
                m = o as MidObject;
                num_kod = m.num_kod;
                id = m.id;
                obozn = m.obozn;
            }
            if (o is PozObject)
            {
                PozObject p = o as PozObject;
                num_kod = p.num_kod;
                id = p.id;
                obozn = p.obozn;
            }

            if (num_kod < 0)
                return;
            UInt32 parent_id = GetMidParent(o);
            if (parent_id == 0)
                return;
            string table = Utils.GetTable(num_kod);
            if (string.IsNullOrEmpty(table))
                return;
            string query = string.Format(CultureInfo.InvariantCulture, "update {0} set parent = {1} where id={2}", table, parent_id, id);
            MySqlCommand cmd = new MySqlCommand(query, m_conn);
            int ret = 0;
            try
            {
                ret = cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Failed to set parent: " + ex.Message);
                return;
            }
            if (ret == 0)
            {
                MessageBox.Show("Failed to set parent for " + obozn);
                return;
            }
            if (m != null)
                m.parent = parent_id;
        }
        public bool DeleteMidRoot(MidObject o)
        {            
            int childs = GetMidChilds(o.id, o.num_kod);
            if (childs > 0)
            {
                MessageBox.Show("Object contains childs: " + childs);
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
                MessageBox.Show("Failed to set parent: " + ex.Message);
                return false;
            }
            return ret > 0;
        }
        private int GetMidChilds(UInt32 id, int num_kod)
        {
            string child_table = Utils.GetChildMidTable(num_kod);
            if (string.IsNullOrEmpty(child_table))
                return 0;
            string query = string.Format(CultureInfo.InvariantCulture, "select * from {0} where parent={1}", child_table, id);
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