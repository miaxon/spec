using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace transformer
{
    class MAT_WmlMaker : MyDotNetClass
    {
        private string path;
        private string xml;
        private int im0;
        private int im1;
        private int im2;
        private int im3;
        //private string head;
        //private string tail;
        //private string mid0_row;
        //private string mid1_row;
        //private string mid2_row;
        //private string mid3_row;
        //private string row;
        //private string sum;

        private string wmlspath = @"C:\Monolit\dms\maintenance\net\wml\";

        public MAT_WmlMaker(int id, string unit, string path, string xml)
        {
            this.path = path;
            this.xml = xml;
            sort();
        }
        private void sort()
        {
            DataTable tmp = new DataTable();
            DataView view = mid0.DefaultView;
            view.Sort = "id asc";
            tmp = view.ToTable();
            mid0.Clear();
            mid0 = tmp.Copy();

            view = mid1.DefaultView;
            view.Sort = "obozn asc";
            tmp = view.ToTable();
            mid1.Clear();
            mid1 = tmp.Copy();

            view = mid2.DefaultView;
            view.Sort = "obozn asc";
            tmp = view.ToTable();
            mid2.Clear();
            mid2 = tmp.Copy();

            view = mid3.DefaultView;
            view.Sort = "obozn asc";
            tmp = view.ToTable();
            mid3.Clear();
            mid3 = tmp.Copy();

            view.Dispose();
            tmp.Dispose();
            merge();
        }
        private void merge()
        {
            int i = 0;
            int im = 0;
            int j = 0;

            im = mid0.Rows.Count;
            for (i = 0; i < im; i++)
            {
                int id = (int)mid0.Rows[i]["id"];
                j = i + 1;
                if (j == mid0.Rows.Count) break;
                int id_next = (int)mid0.Rows[j]["id"];
                while (id == id_next)
                {
                    mid0.Rows.RemoveAt(j);
                    im = mid0.Rows.Count;
                    if (j == mid0.Rows.Count) break;
                    id_next = (int)mid0.Rows[j]["id"];
                }
            }
            im0 = im;

            im = mid1.Rows.Count;
            for (i = 0; i < im; i++)
            {
                string obozn = (string)mid1.Rows[i]["obozn"];
                j = i + 1;
                if (j == mid1.Rows.Count) break;
                string obozn_next = (string)mid1.Rows[j]["obozn"];
                while (obozn == obozn_next)
                {
                    mid1.Rows.RemoveAt(j);
                    im = mid1.Rows.Count;
                    if (j == mid1.Rows.Count) break;
                    obozn_next = (string)mid1.Rows[j]["obozn"];
                }
            }
            im1 = im;

            im = mid2.Rows.Count;
            for (i = 0; i < im; i++)
            {
                string obozn = (string)mid2.Rows[i]["obozn"];
                j = i + 1;
                if (j == mid2.Rows.Count) break;
                string obozn_next = (string)mid2.Rows[j]["obozn"];
                while (obozn == obozn_next)
                {
                    mid2.Rows.RemoveAt(j);
                    im = mid2.Rows.Count;
                    if (j == mid2.Rows.Count) break;
                    obozn_next = (string)mid2.Rows[j]["obozn"];
                }
            }
            im2 = im;

            im = mid3.Rows.Count;
            for (i = 0; i < im; i++)
            {
                string obozn = (string)mid3.Rows[i]["obozn"];
                j = i + 1;
                if (j == mid3.Rows.Count) break;
                string obozn_next = (string)mid3.Rows[j]["obozn"];
                while (obozn == obozn_next)
                {
                    DataRow row = mid3.Rows[i];
                    double sum = (double)row["sum"];
                    double sum_next = (double)mid3.Rows[j]["sum"];
                    row["sum"] = sum + sum_next;

                    double nr = (double)row["nr"];
                    double nr_next = (double)mid3.Rows[j]["nr"];
                    row["nr"] = nr + nr_next;

                    mid3.Rows.RemoveAt(j);
                    im = mid3.Rows.Count;
                    if (j == mid3.Rows.Count) break;
                    obozn_next = (string)mid3.Rows[j]["obozn"];
                }
            }
            im3 = im;

            init_mat();
        }
        private void init_mat()
        {
            string head = read_tmpl(wmlspath + @"mat\head.txt");
            string tail = read_tmpl(wmlspath + @"mat\tail.txt");
            string mid0_row = read_tmpl(wmlspath + @"mat\mid0_row.txt");
            string mid1_row = read_tmpl(wmlspath + @"mat\mid1_row.txt");
            string mid2_row = read_tmpl(wmlspath + @"mat\mid2_row.txt");
            string mid3_row = read_tmpl(wmlspath + @"mat\mid3_row.txt");
            string sum = read_tmpl(wmlspath + @"mat\sum.txt");
            string row = read_tmpl(wmlspath + @"mat\row.txt");
            StringBuilder sb = new StringBuilder();
            sb.Append(head);
            sb.Append(row);
            int n = 0;
            //double sumAll = 0;
            // double snrAll = 0;
            // double sum0 = 0;
            // double snr0 = 0;
            for (int i0 = 0; i0 < im0; i0++)
            {
                //sumAll += sum0;
                //snrAll += snr0;
                //sum0 = 0;
                //snr0 = 0;
                string naimen0 = (string)mid0.Rows[i0]["naimen"];
                string s0 = mid0_row.Replace("$naimen0", naimen0);
                sb.Append(s0);
                sb.Append(row);
                int id0 = (int)mid0.Rows[i0]["id"];
                double sum1 = 0;
                double snr1 = 0;
                for (int i1 = 0; i1 < im1; i1++)
                {
                    int m0 = (int)mid1.Rows[i1]["m0"];
                    if (m0 == id0)
                    {
                        // sum0 += sum1;
                        // snr0 += snr1;
                        sum1 = 0;
                        snr1 = 0;
                        string naimen1 = (string)mid1.Rows[i1]["naimen"];
                        string s1 = mid1_row.Replace("$naimen1", naimen1);
                        sb.Append(s1);
                        sb.Append(row);
                        int id1 = (int)mid1.Rows[i1]["id"];
                        for (int i2 = 0; i2 < im2; i2++)
                        {
                            int m1 = (int)mid2.Rows[i2]["m1"];
                            if (m1 == id1)
                            {
                                string naimen2 = (string)mid2.Rows[i2]["naimen"];
                                string gost2 = (string)mid2.Rows[i2]["gost"];
                                string s2 = mid2_row.Replace("$naimen2", naimen2);
                                s2 = s2.Replace("$gost2", gost2);
                                sb.Append(s2);
                                int id2 = (int)mid2.Rows[i2]["id"];
                                for (int i3 = 0; i3 < im3; i3++)
                                {
                                    int m2 = (int)mid3.Rows[i3]["m2"];
                                    if (m2 == id2)
                                    {
                                        string n3 = Convert.ToString(n++);
                                        string naimen3 = (string)mid3.Rows[i3]["naimen"];
                                        string gost3 = (string)mid3.Rows[i3]["gost"];
                                        string marka = (string)mid3.Rows[i3]["marka"];
                                        string kei = (string)mid3.Rows[i3]["kei"];
                                        //string kei = (mid3.Rows[i3]["kei"] is DBNull) ? "" : (string)mid3.Rows[i3]["kei"];
                                        double sum3 = (double)mid3.Rows[i3]["sum"];
                                        double snr = (double)mid3.Rows[i3]["nr"];
                                        double kfr = Math.Round((sum3 / snr) * 1000, 3) / 1000;
                                        string s3 = mid3_row.Replace("$naimen3", naimen3);
                                        s3 = s3.Replace("$gost3", gost3);
                                        s3 = s3.Replace("$marka", marka);
                                        s3 = s3.Replace("$kei", kei);
                                        s3 = s3.Replace("$sum3", format(sum3));
                                        s3 = s3.Replace("$nr", format(snr));
                                        s3 = s3.Replace("$kfr", format(kfr));
                                        //s3 = s3.Replace("$n", n3);
                                        s3 = s3.Replace("$n", "");
                                        sb.Append(s3);
                                        sum1 += sum3;
                                        snr1 += snr;
                                    }
                                }
                            }
                        }
                        string itog = sum.Replace("$sum", format(sum1));
                        itog = itog.Replace("$naimen1", naimen1);
                        itog = itog.Replace("$snr", format(snr1));
                        sb.Append(row);
                        sb.Append(itog);
                        sb.Append(row);
                    }
                }
            }

            //string itogAll = sum.Replace("$sum", Convert.ToString(sumAll));
            // itogAll = itogAll.Replace("$naimen1", "Общее");
            // itogAll = itogAll.Replace("$snr", Convert.ToString(snrAll));
            // sb.Append(row);
            // sb.Append(itogAll);
            // sb.Append(row);

            string[] dlm = { @"\" };
            string[] arr = path.Split(dlm, StringSplitOptions.RemoveEmptyEntries);
            string unitName = arr[arr.Length - 1];
            sb.Append(tail.Replace("$name", unitName));
            string wml = path + xml + "_ведомость$.xml";
            StreamWriter sw = new StreamWriter(wml);
            sw.Write(sb.ToString());
            sw.Close();
            mid0.Dispose();
            mid1.Dispose();
            mid2.Dispose();
            mid3.Dispose();
            open(wml, dlg.getcheck_listview());

        }
        private string format(double n)
        {
            if (n < 1)
                return string.Format("{0:0.000}", n);
            if (1 <= n && n < 10)
                return string.Format("{0:0.00}", n);
            if (10 <= n && n < 100)
                return string.Format("{0:0.0}", n);
            if (100 <= n )
                return string.Format("{0:0}", n);
            return n.ToString();
        }
        

    }
}
