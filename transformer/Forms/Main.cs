using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;
using transformer;
using libspec.View;

namespace transformer
{
    public partial class Dlg : Form
    {
        private string id;
        private string unit;
        private int numDoc = 0;
        private double currDoc = 0;
        public bool endOfThread;
        public List<ListRecord> listRecord;

        MySqlConnection conn;
        string path;
        string file;
        //private string basePath = @"C:\Monolit\spec\";
        private string basePath = Directory.GetCurrentDirectory() + @"\data\";
        public Dlg()
        {
            InitializeComponent();
            int w = listView.Width;
            listView.Columns[0].Width = (int)(w * .3);
            listView.Columns[1].Width = (int)(w * 0.7);
        }
        public bool getcheck_MSCH()
        {
            return checkMSCH.Checked;
        }
        public bool getcheck_mat()
        {
            return checkMat.Checked;
        }
        public bool getcheck_doc()
        {
            return checkDoc.Checked;
        }
        public bool getcheck_list()
        {
            return checkList.Checked;
        }
        public bool getcheck_listview()
        {
            return checkViewList.Checked;
        }
        public bool getcheck_MSCHview()
        {
            return checkViewMSCH.Checked;
        }
        public string GetConnectionString()
        {
            return "server=" + Utils.Server + ";User Id=spec;password=spec;database=spec;Allow User Variables=True;Allow Zero Datetime=True;Character Set=utf8;Convert Zero Datetime=True";
        }
        private void Connect()
        {
            try
            {
                if (!Directory.Exists(basePath)) Directory.CreateDirectory(basePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не найден путь data в текущем каталоге или отсутствуют права. " + ex.Message);
                Application.Exit();
            }
            try
            {
                string constr = GetConnectionString();
                conn = new MySqlConnection(constr);
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("MySqlConnection error: " + ex.Message);
                Application.Exit();
            }
            int parent = 0;
            string command = "";
            if (unit == "project")
            {
                command = "select obozn, naimen, parent from _pid where id = " + id;
                lblDoc.Text += "все";
                string pname = "";
                lblProject.Text += getItem(command, ref pname, ref parent);

                command = "select count(id) from _gid where parent = " + id;
                int num = getNumOfItem(command);
                lblGroup.Text = "Групп: " + Convert.ToString(num);

                //view group list
                command = "select id, obozn, naimen  from _gid where parent = " + id + " order by obozn";
                listRecord = getItems(command);
                FillListView();
                //

                command = "select count(_did.id) from _did, _gid where _gid.parent = " + id + " and _did.parent=_gid.id";
                num = getNumOfItem(command);
                lblDoc.Text = "Документов: " + Convert.ToString(num);
                numDoc = num;

                path = basePath + pname + @"\";
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                file = pname;
                this.Text = "Расчет проекта";

            }
            if (unit == "group")
            {
                command = "select obozn, naimen, parent from _gid where id = " + id;

                string gname = "";
                string pname = "";
                lblGroup.Text += getItem(command, ref gname, ref parent);
                command = "select obozn, naimen, parent from _pid where id = " + Convert.ToString(parent);
                lblProject.Text += getItem(command, ref pname, ref parent);

                command = "select count(id) from _did where parent = " + id;
                int num = getNumOfItem(command);
                lblDoc.Text = "Документов: " + Convert.ToString(num);
                numDoc = num;
                //
                command = "select _did.id, lid.obozn, lid.naimen  from _did, lid where _did.parent=" + id + " and lid.id=_did.uid order by lid.obozn";
                listRecord = getItems(command);
                FillListView();
                //
                path = basePath + pname;
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                path += @"\" + gname + @"\"; ;
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                file = gname;
                this.Text = "Расчет группы";

            }
            if (unit == "document")
            {
                string dname = "";
                string gname = "";
                string pname = "";

                command = "select lid.obozn, lid.naimen, _did.parent, _did.uid  from _did, lid where _did.id=" + id + " and lid.id=_did.uid";
                lblDoc.Text += getItem(command, ref dname, ref parent);
                command = "select obozn, naimen, parent from _gid where id = " + Convert.ToString(parent);
                lblGroup.Text += getItem(command, ref gname, ref parent);
                command = "select obozn, naimen, parent from _pid where id = " + Convert.ToString(parent);
                lblProject.Text += getItem(command, ref pname, ref parent);
                numDoc = 1;

                path = basePath + pname;
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                path += @"\" + gname + @"\";
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                file = dname;
                this.Text = "Расчет документа";
            }
            if (command.Length == 0)
            {
                MessageBox.Show("Недопустимое значение \"" + unit + "\".", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            conn.Dispose();
            conn.Close();

        }
        private void FillListView()
        {
            foreach (ListRecord r in listRecord)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = r.Obozn;
                lvi.SubItems.Add(r.Naimen);
                lvi.Checked = r.Checked = true;
                lvi.Tag = r;
                listView.Items.Add(lvi);
            }
            chkAll.Checked = true;
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "xml-файлы (*.xml)|*.xml|документы Word (*.doc)|*.doc|документы Word 2010 (*.docx)|*.docx";
            openFileDialog.Multiselect = false;
            openFileDialog.InitialDirectory = basePath;
            DialogResult res = openFileDialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                string file = openFileDialog.FileName;
                if (file.IndexOf("$") != -1 || file.IndexOf(".doc") != -1)
                {
                    IntPtr r = MyDotNetClass.ShellExecute(IntPtr.Zero, "open", "winword", file, "", MyDotNetClass.ShowCommands.SW_SHOWNORMAL);
                    if (r.ToInt32() < 33)
                        r = MyDotNetClass.ShellExecute(IntPtr.Zero, "open", "swriter", file, "", MyDotNetClass.ShowCommands.SW_SHOWNORMAL);
                    if (r.ToInt32() < 33)
                        MessageBox.Show("Не удалось отркрыть документ.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Viewer v = new Viewer(file);
                }
            }

        }

        private void checkMat_CheckedChanged(object sender, EventArgs e)
        {
            checkViewMat.Checked = checkMat.Checked;
        }
        private void checkDoc_CheckedChanged(object sender, EventArgs e)
        {
            checkViewDoc.Checked = checkDoc.Checked;
        }
        private void checkList_CheckedChanged(object sender, EventArgs e)
        {
            checkViewList.Checked = checkList.Checked;
        }

        private void checkMSCH_CheckedChanged(object sender, EventArgs e)
        {
            checkViewMSCH.Checked = checkMSCH.Checked;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Viewer v;
            if (checkMat.Checked || checkDoc.Checked || checkList.Checked || checkMSCH.Checked)
            {
                endOfThread = false;
                currDoc = 0;
                progressBar.Value = 0;
                btnClose.Enabled = false;
                btnOk.Enabled = false;
                btnOpen.Enabled = false;
                MyDotNetClass cl = new MyDotNetClass(this, Convert.ToInt32(id), unit, path, file);
                btnClose.Enabled = true;
                btnOk.Enabled = true;
                btnOpen.Enabled = true;
                //while (!endOfThread)
                //{
                //    lock (this)
                //    {
                //        lblProgress.Text = "Выполнение: " + Convert.ToString(currDoc);
                //        lblProgress.Update();
                //        double val = currDoc / numDoc;
                //        val = Math.Round(val, 2);
                //        progressBar.Value = (int)(val * 100);
                //    }
                //}
                if (checkViewDoc.Checked)
                    v = new Viewer(path + file + "_doc.xml");
                if (checkViewMat.Checked)
                    v = new Viewer(path + file + "_mat.xml");


            }
        }
        private string getItem(string command, ref string name, ref int parent)
        {
            string err = "";
            string item = "";
            try
            {
                MySqlDataReader ds = null;
                MySqlCommand cmd = new MySqlCommand(command, conn);
                cmd.Prepare();
                try
                {
                    ds = cmd.ExecuteReader();

                    if (!ds.HasRows)
                    {
                        MessageBox.Show("Пустой набор:\n" + command, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    string obozn = "";
                    string naimen = "";
                    while (ds.Read())
                    {
                        obozn = baseTOansi(ds.GetString(0));
                        naimen = baseTOansi(ds.GetString(1));
                        parent = ds.GetInt32(2);
                    }
                    if (naimen.Length > 55)
                    {
                        naimen = naimen.Substring(0, 55) + "...";
                    }
                    item = obozn + " " + naimen;
                    name = obozn;
                }
                catch (MySqlException ex)
                {
                    err = ex.Message;
                    MessageBox.Show(err, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (ds != null)
                    {
                        ds.Dispose();
                        ds.Close();
                    }
                }
            }
            catch (MySqlException ex)
            {
                err = ex.Message;
                MessageBox.Show(err, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return item;
        }
        private List<ListRecord> getItems(string command)
        {
            string err = "";
            List<ListRecord> items = new List<ListRecord>();
            try
            {
                MySqlDataReader ds = null;
                MySqlCommand cmd = new MySqlCommand(command, conn);
                cmd.Prepare();
                try
                {
                    ds = cmd.ExecuteReader();

                    if (!ds.HasRows)
                    {
                        MessageBox.Show("Пустой набор:\n" + command, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    while (ds.Read())
                    {
                        ListRecord r = new ListRecord();
                        r.Id = ds.GetInt32(0);
                        r.Obozn = baseTOansi(ds.GetString(1));
                        r.Naimen = baseTOansi(ds.GetString(2));
                        items.Add(r);
                    }

                }
                catch (MySqlException ex)
                {
                    err = ex.Message;
                    MessageBox.Show(err, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (ds != null)
                    {
                        ds.Dispose();
                        ds.Close();
                    }
                }
            }
            catch (MySqlException ex)
            {
                err = ex.Message;
                MessageBox.Show(err, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return items;
        }
        private int getNumOfItem(string command)
        {
            string err = "";
            int item = 0;
            try
            {
                MySqlDataReader ds = null;
                MySqlCommand cmd = new MySqlCommand(command, conn);
                cmd.Prepare();
                try
                {
                    ds = cmd.ExecuteReader();

                    if (!ds.HasRows)
                    {
                        MessageBox.Show(command, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    while (ds.Read())
                    {
                        item = ds.GetInt32(0);
                    }

                }
                catch (MySqlException ex)
                {
                    err = ex.Message;
                    MessageBox.Show(err, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (ds != null)
                    {
                        ds.Dispose();
                        ds.Close();
                    }
                }
            }
            catch (MySqlException ex)
            {
                err = ex.Message;
                MessageBox.Show(err, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return item;
        }
        private static string baseTOansi(string str)
        {
            return str;
            /*byte[] unicodeBytes = Encoding.Unicode.GetBytes(str);
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

        private void Dlg_Load(object sender, EventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();
            btnOk.Enabled = false;
            if (args.Length - 1 == 2)
            {
                btnOk.Enabled = true;
                unit = args[1];
                if (unit == "document") checkMSCH.Enabled = false;
                id = args[2];
                if (Utils.CheckAccess())
                    Connect();
            }
            lblProgress.Text = "Выполнение: ";

        }
        public void update_pb()
        {
            currDoc++;
            lblProgress.Text = "Выполнение: " + Convert.ToString(currDoc);
            lblProgress.Update();
            double val = currDoc / numDoc;
            val = Math.Round(val, 2);
            progressBar.Value = (int)(val * 100);
        }

        private void listView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            ListRecord r = e.Item.Tag as ListRecord;
            r.Checked = e.Item.Checked;
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem i in listView.Items)
            {
                i.Checked = chkAll.Checked;
            }
        }





    }
}
