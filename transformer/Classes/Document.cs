using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
// класс основного документа
namespace transformer
{
    class Document : MyDotNetClass
    {
        bool needHead;
        string obozn;
        string gr;
        public Document(int id, bool needHead, string gr="")
        {
            //trace("new Document instance created.");
            this.needHead = needHead;
            this.gr = gr;
            init(id, needHead);
        }

        private void init(int id, bool needHead)
        {
           trace("///////////////////////////////////////////////init on id=" + Convert.ToString(id));
            MySqlDataReader reader = null;
            MySqlParameter p = new MySqlParameter("@val", id);
            MySqlCommand cmd = new MySqlCommand("select _did.id, _did.uid, _did.num_kol, lid.obozn, lid.naimen, lid.descr from _did, lid where _did.id=@val and _did.closed='N' and lid.id=_did.uid", conn);
            cmd.Parameters.Add(p);
            cmd.Prepare();
            try
            {
                reader = cmd.ExecuteReader();
                reader.Read();
                if (!reader.IsDBNull(0))
                {
                    if (needHead)
                    {
                        writer.WriteStartElement(hdid_Node);
                        string obozn = reader.GetString("obozn");
                        string naimen = reader.GetString("naimen");
                        string descr = reader.GetString("descr");
                        //string num_kol = reader.GetUInt32("num_kol").ToString();
                        //writer.WriteAttributeString("title", obozn);
                        //writer.WriteAttributeString("naimen", naimen);
                        writer.WriteAttributeString("title", "Документ " + obozn + " " + naimen);
                        writer.WriteEndElement();
                    }
                    make_DID(reader);
                }
                else return;
            }
            catch (MySqlException ex)
            {
                trace("Error: \r\n" + ex.Message);
            }
            
            if (reader != null) reader.Close();
            
            return;
        }
        private void make_DID(MySqlDataReader reader)
        {
            trace("    Begin make_DID:");
            //<did id="" obozn="" naimen="" descr="" num_kol="">
            writer.WriteStartElement(did_Node);
            int intID = reader.GetInt32("id");
            int intUID = reader.GetInt32("uid");
            string id = Convert.ToString(intID);
            string uid = Convert.ToString(intUID);
            obozn = reader.GetString("obozn");
            string naimen = reader.GetString("naimen");
            string descr = reader.GetString("descr");
            string num_kol = reader.GetUInt32("num_kol").ToString();
            writer.WriteAttributeString("id", id);
            writer.WriteAttributeString("obozn", obozn);
            writer.WriteAttributeString("naimen", naimen);
            writer.WriteAttributeString("descr", descr);
            writer.WriteAttributeString("num_kol", num_kol);
            writer.WriteAttributeString("sum", num_kol.Replace(",", "."));
            reader.Close();
            reader.Dispose();
            //get pozitions
            int poz = get_POZ(intUID, "lid_old", Convert.ToDouble(num_kol), obozn);

            //writer.WriteAttributeString("positions", Convert.ToString(poz));

            //</did>
            writer.WriteEndElement();
            trace("    End make_DID.");
            //lock (this)
            //{
                dlg.update_pb();
            //}

        }
        private int get_POZ(int parentID, string tab, double parent_kol, string parent_obozn)
        {
           // trace("getPOZ " + tab + " " + parentID.ToString());
            MySqlDataReader ds = null;
            MySqlParameter id = new MySqlParameter("@id", parentID);
            MySqlCommand cmd = new MySqlCommand("select refid, num_kol, num_kod, num_kfr from " + tab + " where parent=@id", conn);
            cmd.Parameters.Add(id);
            //cmd.Parameters.Add(kod);
            cmd.Prepare();
            DataTable table = new DataTable();
            DataColumn column;
            DataRow row;
            //   
            column = new DataColumn();
            column.DataType = Type.GetType("System.Int32");
            column.ColumnName = "refid";
            table.Columns.Add(column);
            //
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "num_kol";
            table.Columns.Add(column);
            //
            column = new DataColumn();
            column.DataType = Type.GetType("System.Int32");
            column.ColumnName = "num_kod";
            table.Columns.Add(column);
            //
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "num_kfr";
            table.Columns.Add(column);
            try
            {
                ds = cmd.ExecuteReader();
                while (ds.Read())
                {
                    row = table.NewRow();
                    row["refid"]    = ds.GetInt32("refid");
                    row["num_kol"]  = ds.GetDouble("num_kol");
                    row["num_kod"]  = ds.GetInt32("num_kod");
                    row["num_kfr"] = ds.GetDouble("num_kfr");
                    table.Rows.Add(row);
                }

            }
            catch (MySqlException ex)
            {
                trace("Error: \r\n" + ex.Message);
            }
            if (ds != null) ds.Close();
            int len = table.Rows.Count;
            for (int i = 0; i < len; i++)
            {
                int c0 = (int)table.Rows[i]["refid"];
                double c1 = (double)table.Rows[i]["num_kol"];
                int c2 = (int)table.Rows[i]["num_kod"];
                double c3 = (double)table.Rows[i]["num_kfr"];
                switch (c2)
                {
                    case mid3_Code:
                        get_MID3(c0, c1, parent_kol, c3);
                        break;
                    case oid_Code:
                        get_OID(c0, c1, parent_kol, parent_obozn);
                        break;
                    case lid_Code:
                        get_LID(c0, c1, parent_kol, parent_obozn);
                        break;
                    case cid_Code:
                        get_CID(c0, c1, parent_kol);
                        break;
                    case bid1_Code:
                        get_BID1(c0, c1, parent_kol);
                        break;
                    case bid2_Code:
                        get_BID2(c0, c1, parent_kol);
                        break;
                }
            }
            
            table.Dispose();
            return len;
        }
        protected void get_MID3(int refid, double num_kol, double parent_kol, double kfr)
        {
            trace("Begin get_MID3");
            MySqlDataReader ds = null;
            double kol = num_kol * parent_kol;
            double nr = kol / kfr;
            writer.WriteStartElement(mid3_Node);
            writer.WriteAttributeString("id", Convert.ToString(refid));
            writer.WriteAttributeString("code", Convert.ToString(mid3_Code));
            writer.WriteAttributeString("num_kol", Convert.ToString(num_kol).Replace(",", "."));
            writer.WriteAttributeString("sum", Convert.ToString(kol).Replace(",", "."));
            MySqlParameter id = new MySqlParameter("@id", refid);
            MySqlCommand cmd = new MySqlCommand("select obozn, naimen, descr, gost, marka, kei, parent, id from mid3 where id=@id", conn);
            cmd.Parameters.Add(id);
            cmd.Prepare();
            try
            { ds = cmd.ExecuteReader(); }
            catch (MySqlException ex)
            { trace("Error: \r\n" + ex.Message); }
            string obozn3 = "";
            string naimen3 = "";
            string gost3 = "";
            string marka3 = "";
            string kei3 = "";
            Int32 parent3 = 0;
            Int32 id3 = 0;
            //<mid3 id="1" obozn="111" naimen="111" descr="" gost="111" num_kol="111" marka="111" kei="кг.">
            if (ds.Read())
            {
                obozn3 = ds.GetString("obozn");
                naimen3 = ds.GetString("naimen");
                gost3 = ds.GetString("gost");
                marka3 = ds.GetString("marka");
                kei3 = (string)kei[ds.GetString("kei")];
                parent3 = ds.GetInt32("parent");
                id3 = ds.GetInt32("id");

                DataRow row = mid3.NewRow();
                row["id"] = id3;
                row["m2"] = parent3;
                row["obozn"] = obozn3;
                row["naimen"] = naimen3;
                row["marka"] = marka3;
                row["gost"] = gost3;
                row["kei"] = kei3;
                row["sum"] = kol;
                row["nr"] = nr;
                mid3.Rows.Add(row);


                writer.WriteAttributeString("obozn", obozn3);
                writer.WriteAttributeString("naimen", naimen3);
                writer.WriteAttributeString("descr", ds.GetString("descr"));
                writer.WriteAttributeString("gost", gost3);
                writer.WriteAttributeString("marka", marka3);
                writer.WriteAttributeString("kei", kei3);
                
            }
            if (ds != null) ds.Close();
            if (parent3 != 0)
            {
                get_MID2(parent3);
            }
            writer.WriteEndElement();               
            trace("End get_MID3");
        }
        protected void get_MID2(Int32 parent)
        {
            trace("Begin get_MID2");
            MySqlDataReader ds = null;
            writer.WriteStartElement(mid2_Node);
            writer.WriteAttributeString("id", Convert.ToString(parent));
            MySqlParameter id = new MySqlParameter("@id", parent);
            MySqlCommand cmd = new MySqlCommand("select obozn, naimen, descr, gost, parent, id from mid2 where id=@id", conn);
            cmd.Parameters.Add(id);
            cmd.Prepare();
            try
            { ds = cmd.ExecuteReader(); }
            catch (MySqlException ex)
            { trace("Error: \r\n" + ex.Message); }
            string obozn2 = "";
            string naimen2 = "";
            string gost2 = "";
            Int32 parent2 = 0;
            Int32 id2 = 0;
            //<mid2 id="22" obozn="" naimen="" descr="" gost="">
            if (ds.Read())
            {
                obozn2 = ds.GetString("obozn");
                naimen2 = ds.GetString("naimen");
                gost2 = ds.GetString("gost");
                parent2 = ds.GetInt32("parent");
                id2 = ds.GetInt32("id");

                DataRow row = mid2.NewRow();
                row["id"] = id2;
                row["m1"] = parent2;
                row["obozn"] = obozn2;
                row["naimen"] = naimen2;
                row["gost"] = gost2;
                mid2.Rows.Add(row);

                writer.WriteAttributeString("obozn", obozn2);
                writer.WriteAttributeString("naimen", naimen2);
                writer.WriteAttributeString("descr", ds.GetString("descr"));
                writer.WriteAttributeString("gost", gost2);
            }
            if (ds != null) ds.Close();
            if (parent2 != 0)
            {
                get_MID1(parent2);
            }
            writer.WriteEndElement();
            trace("End get_MID2");
        }
        protected void get_MID1(Int32 parent)
        {
            trace("Begin get_MID1");
            MySqlDataReader ds = null;
            writer.WriteStartElement(mid1_Node);
            writer.WriteAttributeString("id", Convert.ToString(parent));
            MySqlParameter id = new MySqlParameter("@id", parent);
            MySqlCommand cmd = new MySqlCommand("select obozn, naimen, descr, parent, id from mid1 where id=@id", conn);
            cmd.Parameters.Add(id);
            cmd.Prepare();
            try
            { ds = cmd.ExecuteReader(); }
            catch (MySqlException ex)
            { trace("Error: \r\n" + ex.Message); }
            string obozn1 = "";
            string naimen1 = "";
            Int32 parent1 = 0;
            Int32 id1 = 0;
            //<mid1 id="zz" obozn="" naimen="" descr="">
            if (ds.Read())
            {
                obozn1 = ds.GetString("obozn");
                naimen1 = ds.GetString("naimen");
                parent1 = ds.GetInt32("parent");
                id1 = ds.GetInt32("id");

                DataRow row = mid1.NewRow();
                row["id"] = id1;
                row["m0"] = parent1;
                row["obozn"] = obozn1;
                row["naimen"] = naimen1;
                mid1.Rows.Add(row);

                writer.WriteAttributeString("obozn", obozn1);
                writer.WriteAttributeString("naimen", naimen1);
                writer.WriteAttributeString("descr", ds.GetString("descr"));

            }
            if (ds != null) ds.Close();
            if (parent1 != 0)
            {
                get_MID0(parent1);
            }
            writer.WriteEndElement();
            trace("End get_MID1");
        }
        protected void get_MID0(Int32 parent)
        {
            trace("Begin get_MID0");
            MySqlDataReader ds = null;
            writer.WriteStartElement(mid0_Node);
            writer.WriteAttributeString("id", Convert.ToString(parent));
            MySqlParameter id = new MySqlParameter("@id", parent);
            MySqlCommand cmd = new MySqlCommand("select naimen, descr, id, obozn from mid0 where id=@id", conn);
            cmd.Parameters.Add(id);
            cmd.Prepare();
            try
            { ds = cmd.ExecuteReader(); }
            catch (MySqlException ex)
            { trace("Error: \r\n" + ex.Message); }
            string obozn0 = "";
            string naimen0 = "";
            Int32 id0 = 0;
            //<mid0 id="xx" obozn="" naimen="" descr=""/>
            if (ds.Read())
            {
                obozn0 = ds.GetString("obozn");
                naimen0 = ds.GetString("naimen");
                id0 = ds.GetInt32("id");

                DataRow row = mid0.NewRow();
                row["id"] = id0;
                row["obozn"] = obozn0;
                row["naimen"] = naimen0;
                mid0.Rows.Add(row);

                //writer.WriteAttributeString("obozn", baseTOansi(ds.GetString("obozn")));
                writer.WriteAttributeString("naimen", naimen0);
                writer.WriteAttributeString("descr", ds.GetString("descr"));
            }
            if (ds != null) ds.Close();
            writer.WriteEndElement();
            trace("End get_MID0");
        }
        protected void get_OID(int refid, double num_kol, double parent_kol, string parent_obozn)
        {
            trace("Begin  ------------------- get_OID");
            MySqlDataReader ds = null;
            MySqlParameter id = new MySqlParameter("@id", refid);
            double kol = num_kol * parent_kol;
            writer.WriteStartElement(oid_Node);
            writer.WriteAttributeString("id", Convert.ToString(refid));
            writer.WriteAttributeString("code", Convert.ToString(oid_Code));
            writer.WriteAttributeString("num_kol", Convert.ToString(num_kol).Replace(",","."));
            writer.WriteAttributeString("sum", Convert.ToString(kol).Replace(",", "."));
            MySqlCommand cmd1 = new MySqlCommand("select naimen, obozn, descr from oid where id=@id", conn);
            cmd1.Parameters.Add(id);
            cmd1.Prepare();
            try
            { ds = cmd1.ExecuteReader(); }
            catch (MySqlException ex)
            { trace("Error: \r\n" + ex.Message); }
            //<oid id="1" obozn="111" naimen="111" descr="" num_kol="111" >
            string obozno = "";
            string naimeno = "";
            if (ds.Read())
            {
                obozno = ds.GetString("obozn");
                naimeno = ds.GetString("naimen");
                if (needHead == false)
                {
                    DataRow row = msch.NewRow();
                    row["id"] = refid;
                    row["obozn"] = obozno;
                    row["naimen"] = naimeno;
                    row["dobozn"] = parent_obozn;
                    row["kp"] = 3;
                    row["gr"] = Convert.ToInt32(gr);
                    row["kol"] = kol;
                    msch.Rows.Add(row);
                }

                writer.WriteAttributeString("obozn", obozno);
                writer.WriteAttributeString("naimen", naimeno);
                writer.WriteAttributeString("descr", ds.GetString("descr"));
            }
            if (ds != null) ds.Close();
            int poz = get_POZ(refid, "oid_old", kol, obozno);
           //writer.WriteAttributeString("positions", Convert.ToString(poz));
            writer.WriteEndElement();
            trace("End  ------------------- get_OID");
        }
        protected void get_LID(int refid, double num_kol, double parent_kol, string parent_obozn)
        {
            trace("Begin ------------------- get_LID");
            MySqlDataReader ds = null;
            MySqlParameter id = new MySqlParameter("@id", refid);
            double kol = num_kol * parent_kol;
            writer.WriteStartElement(lid_Node);
            writer.WriteAttributeString("id", Convert.ToString(refid));
            writer.WriteAttributeString("code", Convert.ToString(lid_Code));
            writer.WriteAttributeString("num_kol", Convert.ToString(num_kol).Replace(",", "."));
            writer.WriteAttributeString("sum", Convert.ToString(kol).Replace(",", "."));
            MySqlCommand cmd1 = new MySqlCommand("select naimen, obozn, descr from lid where id=@id", conn);
            cmd1.Parameters.Add(id);
            cmd1.Prepare();
            try
            { ds = cmd1.ExecuteReader(); }
            catch (MySqlException ex)
            { trace("Error: \r\n" + ex.Message); }
            //<lid id="1" obozn="111" naimen="111" descr="" num_kol="111" >
            string oboznl = "";
            string naimenl = "";
            if (ds.Read())
            {
                oboznl = ds.GetString("obozn");
                naimenl = ds.GetString("naimen");
                if (needHead == false)
                {
                    DataRow row = msch.NewRow();
                    row["id"] = refid;
                    row["obozn"] = oboznl;
                    row["naimen"] = naimenl;
                    row["dobozn"] = parent_obozn;
                    row["kp"] = 0;
                    row["gr"] = Convert.ToInt32(gr);
                    row["kol"] = kol;
                    msch.Rows.Add(row);
                }

                writer.WriteAttributeString("obozn", oboznl);
                writer.WriteAttributeString("naimen", naimenl);
                writer.WriteAttributeString("descr", ds.GetString("descr"));
            }
            if (ds != null) ds.Close();
            int poz = get_POZ(refid, "lid_old", kol, oboznl);
            //writer.WriteAttributeString("positions", Convert.ToString(poz));
            writer.WriteEndElement();
            trace("End  ------------------- get_LID");
        }
        protected void get_CID(int refid, double num_kol, double parent_kol)
        {
            trace("Begin ------------------- get_CID");
            MySqlDataReader ds = null;
            MySqlParameter id = new MySqlParameter("@id", refid);
            double kol = num_kol * parent_kol;
            writer.WriteStartElement(cid_Node);
            writer.WriteAttributeString("id", refid.ToString());
            writer.WriteAttributeString("code", Convert.ToString(cid_Code));
            writer.WriteAttributeString("num_kol", Convert.ToString(num_kol).Replace(",", "."));
            writer.WriteAttributeString("sum", Convert.ToString(kol).Replace(",", "."));
            MySqlCommand cmd1 = new MySqlCommand("select naimen, obozn, descr, kei from cid where id=@id", conn);
            cmd1.Parameters.Add(id);
            cmd1.Prepare();
            try
            { ds = cmd1.ExecuteReader(); }
            catch (MySqlException ex)
            { trace("Error: \r\n" + ex.Message); }
            //<cid id="1" obozn="111" naimen="111" descr="" num_kol="111" >
            if (ds.Read())
            {
                writer.WriteAttributeString("obozn", ds.GetString("obozn"));
                writer.WriteAttributeString("naimen", ds.GetString("naimen"));
                writer.WriteAttributeString("descr", ds.GetString("descr"));
                writer.WriteAttributeString("kei", (string)kei[ds.GetString("kei")]);
            }
            string obozn = ds.GetString("obozn");
            if (ds != null) ds.Close();
            int poz = get_POZ(refid, "cid_", kol, obozn);
            
            
            //writer.WriteAttributeString("positions", Convert.ToString(poz));
            writer.WriteEndElement();
            trace("End  ------------------- get_CID");
        }
        protected void get_BID1(int refid, double num_kol, double parent_kol)
        {
            trace("Begin ------------------- get_BID1");
            MySqlDataReader ds = null;
            MySqlParameter id = new MySqlParameter("@id", refid);
            double kol = num_kol * parent_kol;
            writer.WriteStartElement(cid_Node);
            writer.WriteAttributeString("id", Convert.ToString(refid));
            writer.WriteAttributeString("code", Convert.ToString(bid1_Code));
            writer.WriteAttributeString("num_kol", Convert.ToString(num_kol).Replace(",", "."));
            writer.WriteAttributeString("sum", Convert.ToString(kol).Replace(",", "."));
            MySqlCommand cmd1 = new MySqlCommand("select naimen, obozn, descr, kei from bid1 where id=@id", conn);
            cmd1.Parameters.Add(id);
            cmd1.Prepare();
            try
            { ds = cmd1.ExecuteReader(); }
            catch (MySqlException ex)
            { trace("Error: \r\n" + ex.Message); }
            //<cid id="1" obozn="111" naimen="111" descr="" num_kol="111" >
            if (ds.Read())
            {
                writer.WriteAttributeString("obozn", ds.GetString("obozn"));
                writer.WriteAttributeString("naimen", ds.GetString("naimen"));
                writer.WriteAttributeString("descr", ds.GetString("descr"));
                writer.WriteAttributeString("kei", (string)kei[ds.GetString("kei")]);
            }
            if (ds != null) ds.Close();
            //get_POZ(refid, "cid_");
            writer.WriteEndElement();
            trace("End  ------------------- get_BID1");
        }
        protected void get_BID2(int refid, double num_kol, double parent_kol)
        {
            trace("Begin ------------------- get_BID2");
            MySqlDataReader ds = null;
            MySqlParameter id = new MySqlParameter("@id", refid);
            double kol = num_kol * parent_kol;
            writer.WriteStartElement(bid2_Node);
            writer.WriteAttributeString("id", Convert.ToString(refid));
            writer.WriteAttributeString("code", Convert.ToString(bid2_Code));
            writer.WriteAttributeString("num_kol", Convert.ToString(num_kol).Replace(",", "."));
            writer.WriteAttributeString("sum", Convert.ToString(kol).Replace(",", "."));
            MySqlCommand cmd1 = new MySqlCommand("select naimen, obozn, descr, kei from bid2 where id=@id", conn);
            cmd1.Parameters.Add(id);
            cmd1.Prepare();
            try
            { ds = cmd1.ExecuteReader(); }
            catch (MySqlException ex)
            { trace("Error: \r\n" + ex.Message); }
            //<cid id="1" obozn="111" naimen="111" descr="" num_kol="111" >
            if (ds.Read())
            {
                writer.WriteAttributeString("obozn", ds.GetString("obozn"));
                writer.WriteAttributeString("naimen", ds.GetString("naimen"));
                writer.WriteAttributeString("descr", ds.GetString("descr"));
                writer.WriteAttributeString("kei", (string)kei[ds.GetString("kei")]);
            }
            if (ds != null) ds.Close();
            //get_POZ(refid, "bid1");
            writer.WriteEndElement();
            trace("End  ------------------- get_BID2");
        }
    }

}
