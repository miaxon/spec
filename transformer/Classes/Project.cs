using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace transformer
{
    class Project : MyDotNetClass
    {
       

        public Project(int id, bool needHead)
        {
            trace("new Project instance created on id=" + Convert.ToString(id));
            init(id, needHead);
        }
        private bool CheckListRecord(int id)
        {
            ListRecord r  = dlg.listRecord.Find(o => o.Id == id);
            if (r != null)
                return r.Checked;
            return false;
        }
        private void init(int id, bool needHead)
        {
            MySqlDataReader ds = null;
            trace("Begin make_PID:");
            //<pid id="" obozn="" naimen="" descr="">
            
            MySqlParameter p = new MySqlParameter("@val", id);
            MySqlCommand cmd = new MySqlCommand("select id, obozn, naimen, descr from _pid where id=@val", conn);
            cmd.Parameters.Add(p);
            cmd.Prepare();
            try
            {
                ds = cmd.ExecuteReader();
                if (ds.Read())
                {
                    if (needHead)
                    {
                        writer.WriteStartElement(hpid_Node);
                        //writer.WriteAttributeString("obozn", baseTOansi(ds.GetString("obozn")));
                        //writer.WriteAttributeString("naimen", baseTOansi(ds.GetString("naimen")));
                        writer.WriteAttributeString("title", "Проект " + baseTOansi(ds.GetString("obozn")) + " " + baseTOansi(ds.GetString("naimen")));
                        writer.WriteEndElement();
                    }
                    writer.WriteStartElement(pid_Node);
                    writer.WriteAttributeString("id", Convert.ToString(ds.GetInt32("id")));
                    writer.WriteAttributeString("obozn", baseTOansi(ds.GetString("obozn")));
                    writer.WriteAttributeString("naimen", baseTOansi(ds.GetString("naimen")));
                    writer.WriteAttributeString("descr", baseTOansi(ds.GetString("descr")));
                }
            }
            catch (MySqlException ex)
            {
                trace("Error: \r\n" + ex.Message);
            }
            finally
            {
                if (ds != null) ds.Close();
            }
            ds = null;
            p = new MySqlParameter("@val", id);
            cmd = new MySqlCommand("select id from _gid where parent=@val", conn);
            cmd.Parameters.Add(p);
            cmd.Prepare();
            DataTable table = new DataTable();
            DataColumn column;
            DataRow row;
            //   
            column = new DataColumn();
            column.DataType = Type.GetType("System.Int32");
            column.ColumnName = "id";
            table.Columns.Add(column);
            try
            {
                ds = cmd.ExecuteReader();
                while (ds.Read())
                {
                    int idr = ds.GetInt32("id");
                    if (CheckListRecord(idr))
                    {
                        row = table.NewRow();
                        row["id"] = idr;
                        table.Rows.Add(row);
                    }
                }

            }
            catch (MySqlException ex)
            {
                trace("Error: \r\n" + ex.Message);
            }
            finally
            {
                if (ds != null) ds.Close();
            }

            int len = table.Rows.Count;
            for (int i = 0; i < len; i++)
            {
                int gid = (int)table.Rows[i][0];
                Group g = new Group(gid, false);
            }
            writer.WriteEndElement();
            trace("End make_PID:");
        }
    }
}
