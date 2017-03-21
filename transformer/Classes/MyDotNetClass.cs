using System;
using System.Collections;
//using System.Linq;
using System.Xml;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;
using System.Reflection;
//using System.Threading;
using System.Data;
using Word = Microsoft.Office.Interop.Word;
using transformer;
using System.Collections.Generic;
namespace transformer
{
    public class MyDotNetClass
    {
        public enum ShowCommands : int
        {
            SW_HIDE = 0,
            SW_SHOWNORMAL = 1,
            SW_NORMAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMAXIMIZED = 3,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_SHOWDEFAULT = 10,
            SW_FORCEMINIMIZE = 11,
            SW_MAX = 11
        }

        [DllImport("shell32.dll")]
        public static extern IntPtr ShellExecute(
            IntPtr hwnd,
            string lpOperation,
            string lpFile,
            string lpParameters,
            string lpDirectory,
            ShowCommands nShowCmd
            );
        protected static Dlg dlg;
        protected static Hashtable kei = null;
        protected static DbgForm dbg = null;
        protected static MySqlConnection conn = null;
        public bool isInit = false;
        protected static string err = "";
        protected const int mid3_Code   = 9;
        protected const int oid_Code    = 3;
        protected const int lid_Code    = 0;
        protected const int cid_Code    = 7;
        protected const int bid1_Code   = 1;
        protected const int bid2_Code   = 2;
        protected const int pok_Code    = 4;

        protected const string mid3_Node = "mid3";
        protected const string mid2_Node = "mid2";
        protected const string mid1_Node = "mid1";
        protected const string mid0_Node = "mid0";
        protected const string oid_Node  = "oid";
        protected const string lid_Node  = "lid";
        protected const string cid_Node  = "cid";
        protected const string bid1_Node = "bid1";
        protected const string bid2_Node = "bid2";
        protected const string pok_Node  = "pok";

        protected const string did_Node = "did";
        protected const string gid_Node = "gid";
        protected const string pid_Node = "pid";

        protected const string hdid_Node = "hdr";
        protected const string hgid_Node = "hdr";
        protected const string hpid_Node = "hdr";

        //private string xslt_mat = @"C:\Monolit\xslt\mat.xslt";
        //private string xslt_doc = @"C:\Monolit\xslt\doc.xslt";

        private string xslt_mat = Directory.GetCurrentDirectory() + @"\Config\xslt\mat.xslt";
        private string xslt_doc = Directory.GetCurrentDirectory() + @"\Config\xslt\doc.xslt";

        private int id;
        private string unit;
        private string path;
        private string xml;
        //protected StringBuilder sb = null;

        protected static XmlTextWriter writer = null;
        //protected static Thread t;
        protected static DataTable mid0;
        protected static DataTable mid1;
        protected static DataTable mid2;
        protected static DataTable mid3;
        protected static DataTable msch;

        public MyDotNetClass()
        {
        }
        private void init_msch()
        {
            msch = new DataTable();
            DataColumn column = new DataColumn();
            column.DataType = Type.GetType("System.Int32");
            column.ColumnName = "id";
            msch.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "naimen";
            msch.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "obozn";
            msch.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.Int32");
            column.ColumnName = "kol";
            msch.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "dobozn";
            msch.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.Int32");
            column.ColumnName = "kp";
            msch.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.Int32");
            column.ColumnName = "gr";
            msch.Columns.Add(column);
        }
        private void init_mid0()
        {
            mid0 = new DataTable();
            DataColumn column = new DataColumn();
            column.DataType = Type.GetType("System.Int32");
            column.ColumnName = "id";
            mid0.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "naimen";
            mid0.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "obozn";
            mid0.Columns.Add(column);
        }
        private void init_mid1()
        {
            mid1 = new DataTable();
            DataColumn column = new DataColumn();
            column.DataType = Type.GetType("System.Int32");
            column.ColumnName = "id";
            mid1.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.Int32");
            column.ColumnName = "m0";
            mid1.Columns.Add(column);
            
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "obozn";
            mid1.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "naimen";
            mid1.Columns.Add(column);
            
        }
        private void init_mid2()
        {
            mid2 = new DataTable();
            DataColumn column = new DataColumn();
            column.DataType = Type.GetType("System.Int32");
            column.ColumnName = "id";
            mid2.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.Int32");
            column.ColumnName = "m1";
            mid2.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "obozn";
            mid2.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "naimen";
            mid2.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "gost";
            mid2.Columns.Add(column);

        }
        private void init_mid3()
        {
            mid3 = new DataTable();
            DataColumn column = new DataColumn();
            column.DataType = Type.GetType("System.Int32");
            column.ColumnName = "id";
            mid3.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.Int32");
            column.ColumnName = "m2";
            mid3.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "obozn";
            mid3.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "naimen";
            mid3.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "marka";
            mid3.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "gost";
            mid3.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "kei";
            mid3.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "sum";
            mid3.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "nr";
            mid3.Columns.Add(column);
        }
        protected static void trace(string str){
            if(dbg != null)dbg.trace(str);
        }
        public MyDotNetClass(Dlg dlg, int id, string unit, string path, string xml) {
            MyDotNetClass.dlg = dlg;
            //MyDotNetClass.dbg = new DbgForm();
           // dbg.Show();
            //t = new Thread(new ThreadStart(this.init));
            this.id = id;
            this.unit = unit;
            this.path = path;
            this.xml = xml;
           // t.Start();
            init();
        }
        public void init()
        {
            if (isInit) return;
            //dbg = new DbgForm();
            //dbg.Show();
            
            int res = Connect();
            if (res == 0)
            {
                trace(DateTime.Now + " Server version:" + conn.ServerVersion);
                trace("Connection successful.");
                trace("start id = " + Convert.ToString(id));
                trace("start unit = " + unit);
                isInit = true;
                init_mid0();
                init_mid1();
                init_mid2();
                init_mid3();
                init_msch();
            }
            else
            {
                trace(DateTime.Now + " Connection failed:");
                trace(err);
                isInit = false;
                MessageBox.Show("Не могу соединиться с сервером MySQL.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            EntryPoint(id,unit, path, xml);
            dlg.endOfThread = true;
            return;
        }//
        public int Connect()
        {
            if (conn != null) return 2;
            try
            {
                conn = new MySqlConnection(dlg.GetConnectionString());
                conn.Open();
            }
            catch (MySqlException ex)
            {
                err = ex.Message;
            }
            if (conn == null) return 1;
            return 0;
        }
        public void EntryPoint(int id, string unit, string path, string xml)
        {
            get_KEI();
            string src = path + "src.xml";
           
            writer = new XmlTextWriter(src, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();

            writer.WriteStartElement("root");
            switch (unit)
            {
                case "project":
                    Project p = new Project(id, true);
                    break;
                case "group":
                    Group g = new Group(id, true);
                    break;
                case "document":
                    Document d = new Document(id, true);
                    break;
                default:
                    return;
            }
            writer.WriteEndElement();
            writer.Flush();
            writer.Close();
           
            if (dlg.getcheck_mat())
            {
                XmlTextReader r = new XmlTextReader(src);
                XmlTextWriter w = new XmlTextWriter(path + xml + "_mat.xml", Encoding.UTF8);
                w.Formatting = Formatting.Indented;
                w.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
                string PI = "type='text/xsl' href='" + xslt_mat + "'";
                w.WriteProcessingInstruction("xml-stylesheet", PI);
                w.WriteNode(r, true);
                w.Flush();
                w.Close();
                r.Close();
            }
            if (dlg.getcheck_doc())
            {
                XmlTextReader r = new XmlTextReader(src);
                XmlTextWriter w = new XmlTextWriter(path + xml + "_doc.xml", Encoding.UTF8);
                w.Formatting = Formatting.Indented;
                w.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
                string PI = "type='text/xsl' href='" + xslt_doc + "'";
                w.WriteProcessingInstruction("xml-stylesheet", PI);
                w.WriteNode(r, true);
                w.Flush();
                w.Close();
                r.Close();
            }
            File.Delete(src);
            if (dlg.getcheck_list())
            {
                MAT_WmlMaker wmlm = new MAT_WmlMaker(id, unit, path, xml);
            }
            if (dlg.getcheck_MSCH())
            {
                MSCH_WmlMaker wmlm = new MSCH_WmlMaker(id, unit, path, xml);
            }
            conn.Dispose();
            conn.Close();
            conn = null;
            return;
        }
        private void get_KEI()
        {
            trace("Begin get_KEI");
            kei = new Hashtable();
            MySqlDataReader ds = null;
            MySqlCommand cmd = new MySqlCommand("select * from kid", conn);
            cmd.Prepare();
            try
            { 
                ds = cmd.ExecuteReader();
                while (ds.Read())
                {
                    kei.Add(baseTOansi(ds.GetString("obozn")), baseTOansi(ds.GetString("naimen")));
                }
            }
            catch (MySqlException ex)
            { err = ex.Message;trace("Error: \r\n" + ex.Message); }
            finally
            {
                if (ds != null) ds.Close();
            }
            trace("End get_KEI");

        }
        protected static string baseTOansi(string str)
        {
            return str;
            /*
            byte[] unicodeBytes = Encoding.Unicode.GetBytes(str);
            byte[] ansiBytes = new byte[str.Length + 1];
            int j = 0;
            int i = 0;
            for (i = 0; i < str.Length; i++)
            {

                ansiBytes[i] = unicodeBytes[j];
                j = j + 2;

            }
            ansiBytes[i] = 0;
            char[] ansiChars = new char[i];
            Encoding.Default.GetChars(ansiBytes, 0, i, ansiChars, 0);
            return new string(ansiChars);*/
        }
        
        protected string saveDoc(string wml)
        {
            if (!checkWord()) return "";
            try
            {
                Word._Application app = new Word.Application();
                app.Visible = false;
                Object template = wml;
                Object doc = wml.Replace("$.xml", ".doc"); ;
                app.Documents.Add(ref template);
                app.ActiveDocument.SaveAs(ref doc);
                app.Quit();
                return (string)doc;
            }catch
            {
                return "";
            }
        }
        bool checkWord()
        {
            return (null != Type.GetTypeFromProgID("Word.Application"));
        }
        protected string read_tmpl(string path)
        {
            StreamReader sr = new StreamReader(path, System.Text.Encoding.Default);
            string res = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            return res;
        }
        protected void open(string xml, bool run)
        {
            string wml = xml;
           
            string doc = saveDoc(xml);
            if (doc.Length != 0)
            {
                File.Delete(xml);
                wml = doc;
            }
            
            if (run)
            {
                IntPtr res = ShellExecute(IntPtr.Zero, "open", "winword", wml, "", ShowCommands.SW_SHOWNORMAL);
                int ires = res.ToInt32();
                if (ires < 33)
                    res = ShellExecute(IntPtr.Zero, "open", "swriter", wml, "", ShowCommands.SW_SHOWNORMAL);
                if (ires < 33)
                    MessageBox.Show("Не удалось отркрыть документ.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } }
    }
}
