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
        
        public DocObject AddDoc(DocObject o, UInt32 parent)
        {
            string query = string.Format(CultureInfo.InvariantCulture, 
                           "insert into lid (obozn, naimen, descr) values('{0}', '{1}', '{2}')", 
                           o.obozn, 
                           o.naimen, 
                           o.descr);
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
            UInt32 uid = DocIdByObozn(o.obozn);
            if (uid == 0)
                return null;
            query = string.Format(CultureInfo.InvariantCulture, 
                    "insert into _did (parent, uid, num_kol) values({0}, {1})", 
                    parent, 
                    uid,
                    o.num_kol);
            cmd = new MySqlCommand(query, m_conn);
            try
            {
                int r = cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Utils.DBError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return null;
            }
            DocObject doc = DocByObozn(o.obozn);
            List<PozObject> list = GetPozList("lid_old", o.refid);
            foreach (PozObject poz in list)
            {
                AddPoz(poz, doc);
            }
            return doc;
        }

        public bool SetStatusDoc(ref DocObject o, Closed status)
        {
            string query = null;
            MySqlCommand cmd = null;
            int r = 0;
            query = string.Format(CultureInfo.InvariantCulture, 
                    "update _did set closed='{0}' where id = {1}", 
                    status, 
                    o.id);
            cmd = new MySqlCommand(query, m_conn);
            try
            {
                r = cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Utils.DBError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
            if (r > 0)
                o.status = status;
            return r > 0;
        }
        public bool DeleteDoc(DocObject o)
        {
            int ret = 0;
            string query = null;
            MySqlCommand cmd = null;

            query = string.Format(CultureInfo.InvariantCulture, 
                    "delete from lid_old where parent = {0}", 
                    o.refid);
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

            query = string.Format(CultureInfo.InvariantCulture, 
                    "delete from lid where id = {0}", 
                    o.refid);
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

            query = string.Format(CultureInfo.InvariantCulture, 
                    "delete from _did where id = {0}", 
                    o.id);
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
            return ret != 0;
        }        
        private UInt32 DocIdByObozn(string obozn)
        {
            MySqlDataReader reader = null;
            UInt32 ret = 0;
            string query = string.Format(CultureInfo.InvariantCulture, 
                            "select id from lid where obozn='{0}'", 
                            obozn);
            MySqlCommand cmd = new MySqlCommand(query, m_conn);
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ret = reader.GetUInt32(0);
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
            return ret;
        }
        private DocObject DocByObozn(string obozn)
        {
            string query = string.Format(CultureInfo.InvariantCulture, 
                "select _did.id, lid.obozn, lid.naimen, lid.descr, _did.closed, _did.num_kol, _did.uid from _did inner join lid on _did.uid=lid.id where lid.obozn = '{0}'", 
                obozn);
            MySqlDataReader reader = null;
            MySqlCommand cmd = new MySqlCommand(query, m_conn);
            DocObject ret = null;
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    //Type t = values[5].GetType();
                    ret = new DocObject(values);
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
            return ret;
        }

        public bool MoveDoc(DocObject doc, UInt32 parent)
        {
            string query = string.Format(CultureInfo.InvariantCulture, 
                "update _did set parent={0} where id={1}", 
                parent, 
                doc.id);

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
        public bool DocExists(string obozn)
        {
            string query = string.Format(CultureInfo.InvariantCulture, 
                "select count(id) from lid where obozn='{0}'", 
                obozn);
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
