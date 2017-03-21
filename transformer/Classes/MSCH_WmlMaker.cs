using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Windows.Forms;
namespace transformer
{
    class MSCH_WmlMaker : MyDotNetClass
    {
        private string path;
        private string xml;

        private string head;
        private string tail;
        private string full_row;
        private string cut_row;
        private string group_row;
        private string sum;
        private string row;
        private int ns;
        private string wmlspath = Directory.GetCurrentDirectory() + @"\Config\wml\";
        private string group;
        public MSCH_WmlMaker(int id, string unit, string path, string xml)
        {
            this.path = path;
            this.xml = xml;
            head = read_tmpl(wmlspath + @"msch\head.txt");
            tail = read_tmpl(wmlspath + @"msch\tail.txt");
            full_row = read_tmpl(wmlspath + @"msch\full_row.txt");
            cut_row = read_tmpl(wmlspath + @"msch\cut_row.txt");
            group_row = read_tmpl(wmlspath + @"msch\group_row.txt");
            sum = read_tmpl(wmlspath + @"msch\sum.txt");
            row = read_tmpl(wmlspath + @"msch\row.txt");
            prepare_gr();
        }
        private void prepare_gr()
        {
            DataTable tmp = new DataTable();
            DataView view = msch.DefaultView;
            view.Sort = "gr asc";
            tmp = view.ToTable();
            msch.Clear();
            msch = tmp.Copy();
            view.Dispose();
            tmp.Dispose();

            int count = msch.Rows.Count;
            int r = 0;
            do
            {
                ns = 1;
                DataTable gr = msch.Clone();
                gr.Columns.Remove("gr");
                int i = r;
                int nextGroup = 0;
                int currentGroup = (int)msch.Rows[r]["gr"];
                group = Convert.ToString(currentGroup) + "00";
                do
                {
                    DataRow row = gr.NewRow();
                    row["id"] = msch.Rows[i]["id"];
                    row["naimen"] = msch.Rows[i]["naimen"];
                    row["obozn"] = msch.Rows[i]["obozn"];
                    row["kol"] = msch.Rows[i]["kol"];
                    row["dobozn"] = msch.Rows[i]["dobozn"];
                    row["kp"] = msch.Rows[i]["kp"];
                    gr.Rows.Add(row);
                    i++;
                    if (i == count) break;
                    nextGroup = (int)msch.Rows[i]["gr"];
                } while (currentGroup == nextGroup);

                prepare_kp(ref gr);
                r += i;
            } while (r < count);
        }
        private void prepare_kp(ref DataTable tab)
        {
            DataTable tmp = new DataTable();
            DataView view = tab.DefaultView;
            view.Sort = "kp, id asc";
            //view.Sort = "kp, id, obozn, dobozn asc";
            tmp = view.ToTable();
            tab.Clear();
            tab = tmp.Copy();
            view.Dispose();
            tmp.Dispose();
            prepare_primen(ref tab);
            prepare_file(ref tab);
        }
        private void prepare_primen(ref DataTable tab)
        {
            int im = tab.Rows.Count;
            int i = 0;
            while (i < im)
            {
                int id = (int)tab.Rows[i]["id"];
                int kol = (int)tab.Rows[i]["kol"];
                int j = i + 1;
                if (j == im) break;
                int id_next = (int)tab.Rows[j]["id"];
                while (id == id_next)
                {
                    for (int k = i; k < j; k++)
                    {
                        string sj = tab.Rows[j]["dobozn"].ToString();
                        string sk = tab.Rows[k]["dobozn"].ToString();
                        if ((int)tab.Rows[k]["kol"] >= 0 && sj == sk)
                        {
                            int kol2 = (int)tab.Rows[k]["kol"];
                            kol2 += (int)tab.Rows[j]["kol"];
                            tab.Rows[k]["kol"] = kol2;
                            kol += (int)tab.Rows[j]["kol"];
                            tab.Rows[j]["kol"] = -1;
                        }
                    }
                    j++;
                    if (j == im) break;
                    id_next = (int)tab.Rows[j]["id"];
                }
                i = j;
            }
            DataTable tmp = new DataTable();
            DataView view = tab.DefaultView;
            //view.Sort = "kp, id asc";
            view.Sort = "kp, obozn, dobozn asc";
            tmp = view.ToTable();
            tab.Clear();
            tab = tmp.Copy();
            view.Dispose();
            tmp.Dispose();
        }
        private void prepare_file(ref DataTable tab)
        {
            DataTable tmp = tab.Clone();
            int im = tab.Rows.Count;
            int i = 0;
            int n = 1;
            while (i < im)
            {
                DataRow row = tmp.NewRow();
                int id = (int)tab.Rows[i]["id"];
                row["id"] = id;
                row["obozn"] = tab.Rows[i]["obozn"];
                row["naimen"] = tab.Rows[i]["naimen"];
                row["dobozn"] = tab.Rows[i]["dobozn"];
                row["kol"] = tab.Rows[i]["kol"];
                row["kp"] = (int)tab.Rows[i]["kp"];
                int kol = (int)tab.Rows[i]["kol"];
                tmp.Rows.Add(row);
                n++;
                int j = i + 1;
                int k = j;
                if (j == im) break;
                int id_next = (int)tab.Rows[j]["id"];
                while (id == id_next)
                {

                    if ((int)tab.Rows[j]["kol"] >= 0)
                    {
                        DataRow crow = tmp.NewRow();
                        crow["id"] = id;
                        crow["obozn"] = tab.Rows[j]["obozn"];
                        crow["naimen"] = "same";
                        crow["dobozn"] = tab.Rows[j]["dobozn"];
                        crow["kol"] = tab.Rows[j]["kol"];
                        crow["kp"] = (int)tab.Rows[j]["kp"];
                        int kol1 = (int)tab.Rows[j]["kol"];
                        tmp.Rows.Add(crow);
                        kol += kol1;
                        k++;
                    }
                    j++;
                    if (j == im) break;
                    id_next = (int)tab.Rows[j]["id"];
                }
                if (k - i != 1)
                {
                    DataRow srow = tmp.NewRow();
                    srow["id"] = id;
                    srow["obozn"] = tab.Rows[i]["obozn"];
                    srow["naimen"] = "sum";
                    srow["dobozn"] = "";
                    srow["kol"] = kol;
                    srow["kp"] = (int)tab.Rows[i]["kp"];
                    tmp.Rows.Add(srow);
                }
                i = j;
            }
            ///DataView tview = tmp.DefaultView;
            //tview.Sort = "kp, obozn, dobozn asc";
            //tab.Clear();
            //tab = tview.ToTable();
            //tview.Dispose();
            //tmp.Clear();
            //tmp.Dispose();
            make_file(ref tmp);
        }
        private void make_file(ref DataTable tab)
        {
            string head = read_tmpl(wmlspath + @"msch\head.txt");
            string tail = read_tmpl(wmlspath + @"msch\tail.txt");
            string full_row = read_tmpl(wmlspath + @"msch\full_row.txt");
            string cut_row = read_tmpl(wmlspath + @"msch\cut_row.txt");
            string group_row = read_tmpl(wmlspath + @"msch\group_row.txt");
            string sum = read_tmpl(wmlspath + @"msch\sum.txt");
            string row = read_tmpl(wmlspath + @"msch\row.txt");
            StringBuilder sb = new StringBuilder();
            sb.Append(head);
            sb.Append(group_row.Replace("$group", group).Replace("$ns", get_ns()));
            //
            int im = tab.Rows.Count;
            int kp0 = 0;
            int i = 0;
            int n = 1;
            while (i < im)
            {
                int kp = (int)tab.Rows[i]["kp"];
                if (kp != kp0)
                {
                    kp0 = kp;
                    //sb.Append(row.Replace("$ns", get_ns()));
                    //sb.Append(row.Replace("$ns", get_ns()));
                    //sb.Append(row.Replace("$ns", get_ns()));
                    sb.Append(row.Replace("$ns", ""));
                    sb.Append(row.Replace("$ns", ""));
                    sb.Append(row.Replace("$ns", ""));
                }
                string obozn = (string)tab.Rows[i]["obozn"];
                string naimen = (string)tab.Rows[i]["naimen"];
                string dobozn = (string)tab.Rows[i]["dobozn"];
                int kol = (int)tab.Rows[i]["kol"];
                if (naimen == "same")
                {
                    string s = full_row.Replace("$obozn", "");
                    s = s.Replace("$naimen", "");
                    s = s.Replace("$dobozn", dobozn);
                    s = s.Replace("$kol", Convert.ToString(kol));
                    s = s.Replace("$kei", "796");
                    //s = s.Replace("$ns", get_ns());
                    //s = s.Replace("$n", "");
                    s = s.Replace("$ns", "");
                    s = s.Replace("$n", "");
                    sb.Append(s);
                }

                if (naimen == "sum")
                {
                    string s2 = sum.Replace("$sum", Convert.ToString(kol));
                    //s2 = s2.Replace("$ns", get_ns());
                    s2 = s2.Replace("$ns", "");
                    s2 = s2.Replace("$kei", "796");
                    sb.Append(s2);
                }
                if (naimen != "same" && naimen != "sum")
                {
                    string s = full_row.Replace("$obozn", obozn);
                    s = s.Replace("$naimen", naimen);
                    s = s.Replace("$dobozn", dobozn);
                    s = s.Replace("$kol", Convert.ToString(kol));
                    s = s.Replace("$kei", "796");
                    //s = s.Replace("$ns", get_ns());
                    //s = s.Replace("$n", Convert.ToString(n));
                    s = s.Replace("$ns", "");
                    s = s.Replace("$n", "");
                    sb.Append(s);
                    n++;
                }
                i++;
            }
            //
            string[] dlm = { @"\" };
            string[] arr = path.Split(dlm, StringSplitOptions.RemoveEmptyEntries);
            string unitName = arr[arr.Length - 1];
            sb.Append(tail.Replace("$name", unitName));
            string wml = path + xml + "-" + group + "_мсч$.xml";
            StreamWriter sw = new StreamWriter(wml);
            sw.Write(sb.ToString());
            sw.Close();
            open(wml, dlg.getcheck_MSCHview());
        }
        private string get_ns()
        {
            string res = Convert.ToString(ns);
            ns++;
            if (ns == 17)
                ns = 1;
            return res;
        }
    }
}
