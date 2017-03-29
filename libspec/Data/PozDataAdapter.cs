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
        public bool AddPoz(PozObject src, DocObject dst)
        {
            UInt32 refid = src.refid == 0 ? src.id : src.refid;
            string query = string.Format(CultureInfo.InvariantCulture, "insert into lid_old (parent, num_kod, num_kol, num_kfr, refid) values({0}, {1}, {2}, {3}, {4})",
                                           dst.refid,
                                           src.num_kod,
                                           src.num_kol,
                                           src.num_kfr,
                                           refid);
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
        public bool AddPoz(PozObject src, PozObject dst)
        {
            string table = Utils.GetChildTable(dst.num_kod);
            if (string.IsNullOrEmpty(table))
                return false;
            string query = string.Format(CultureInfo.InvariantCulture, "insert into {0} (parent, num_kod, num_kol, num_kfr, refid) values({1}, {2}, {3}, {4}, {5})",
                                           table,
                                           dst.id,
                                           src.num_kod,
                                           src.num_kol,
                                           src.num_kfr,
                                           src.refid);
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
        public PozObject AddRootPoz(PozObject target)
        {
            string table = Utils.GetTable(target.num_kod);
            if (string.IsNullOrEmpty(table))
                return null;
            string query = string.Format(CultureInfo.InvariantCulture, "insert into {0} (obozn, naimen, descr) values('{1}', '{2}', '{3}')", table, target.obozn, target.naimen, target.descr);
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
                Utils.DBError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return null;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
            if (id == 0)
                return null;
            string child_table = Utils.GetChildTable(target.num_kod);
            if (!string.IsNullOrEmpty(child_table))
            {
                List<PozObject> list = GetPozList(child_table, target.id);
                target.SetRootId(id);
                foreach (PozObject poz in list)
                {
                    AddPoz(poz, target);
                }
            }
            target.SetRootId(id);
            if(target.num_kod == 9 || target.num_kod == 92)
                SetMidPozParent(target);
            return target;
        }

        public bool DeletePoz(string table, PozObject o)
        {
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
        public bool DeletePozRoot(PozObject o)
        {
            string table = Utils.GetTable(o.num_kod);
            if (string.IsNullOrEmpty(table))
                return false;
            int ret = 0;
            string query = null;
            MySqlCommand cmd = null;
            string child_table = Utils.GetChildTable(o.num_kod);
            if (!string.IsNullOrEmpty(child_table))
            {
                query = string.Format(CultureInfo.InvariantCulture, "delete from {0} where parent = {1}", child_table, o.id);
                cmd = new MySqlCommand(query, m_conn);
                try
                {
                    ret = cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    Utils.DBError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    return false;
                }
            }
            if (GetMidChilds(o.id, o.num_kod) > 0)
                return false;
            query = string.Format(CultureInfo.InvariantCulture, "delete from {0} where id = {1}", table, o.id);
            cmd = new MySqlCommand(query, m_conn);
            try
            {
                ret = cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Utils.DBError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
            int ret_lid = 0;
            if (table == "lid") // remove lid refernce from _did if exists
            {
                query = string.Format(CultureInfo.InvariantCulture, "delete from _did where uid = {0}", o.id);
                cmd = new MySqlCommand(query, m_conn);
                try
                {
                    ret_lid = cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    Utils.DBError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    return false;
                }
            }
            if (o.num_kod <= 9)
                RemovePozRefs(o);
            return ret != 0;
        }
        private void RemovePozRefs(PozObject o)
        {
            string query = null;
            MySqlCommand cmd = null;
            string[] tables = Utils.GetChildTables();
            foreach (string table in tables)
            {
                query = string.Format(CultureInfo.InvariantCulture, "delete from {0} where refid = {1} and num_kod={2}", table, o.id, o.num_kod);
                cmd = new MySqlCommand(query, m_conn);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    Utils.DBError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                }
            }
        }      
        public bool MovePoz(PozObject src, DocObject dst)
        {
            string query = string.Format(CultureInfo.InvariantCulture, "update lid_old set parent={0} where id={1}", dst.refid, src.id);

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
        public bool MovePoz(PozObject src, PozObject dst)
        {
            string child_table = Utils.GetChildTable(dst.num_kod);
            if (string.IsNullOrEmpty(child_table))
                return false;
            string query = string.Format(CultureInfo.InvariantCulture, "update {0} set parent={1} where id={2}", child_table, dst.id, src.id);

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

        public bool PozExists(PozObject o)
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
            return ret > 0;
        }
    }
}
