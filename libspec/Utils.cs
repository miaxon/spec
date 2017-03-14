using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using libspec.Properties;
using libspec.Objects;
using System.Drawing;

namespace libspec
{
    public static class Utils
    {
        private static Dictionary<int, string> m_tables;
        private static Dictionary<int, string> m_child_tables;
        private static Dictionary<int, string> m_pozImages;
        private static Dictionary<int, string> m_kodes;
        private static ImageList m_imageList;
        public static void InitMaps()
        {
            m_kodes = new Dictionary<int, string>();
            m_kodes.Add(0, "Лицевые чертежи");
            m_kodes.Add(1, "Покупные 1");
            m_kodes.Add(2, "Покупные 2");
            m_kodes.Add(3, "Обезличенные черетежи");
            m_kodes.Add(4, "Поковки");
            m_kodes.Add(7, "Сложные материалы");
            m_kodes.Add(9, "Материалы");
            m_kodes.Add(92, "Материалы");

            m_tables = new Dictionary<int, string>();
            m_tables.Add(0, "lid");
            m_tables.Add(1, "bid1");
            m_tables.Add(2, "bid2");
            m_tables.Add(3, "oid");
            m_tables.Add(4, "pok");
            m_tables.Add(7, "cid");
            m_tables.Add(9, "mid3");
            m_tables.Add(92, "mid3");

            m_child_tables = new Dictionary<int, string>();
            m_child_tables.Add(0, "lid_old");
            m_child_tables.Add(1, "bid1");
            m_child_tables.Add(2, "bid2");
            m_child_tables.Add(3, "oid_old");
            m_child_tables.Add(4, "pok");
            m_child_tables.Add(7, "cid_");
            m_child_tables.Add(9, "mid3");
            m_child_tables.Add(92, "mid3");

            m_pozImages = new Dictionary<int, string>();
            m_pozImages.Add(0, "lid");
            m_pozImages.Add(1, "bid");
            m_pozImages.Add(2, "bid");
            m_pozImages.Add(3, "oid");
            m_pozImages.Add(4, "pok");
            m_pozImages.Add(7, "cid");
            m_pozImages.Add(9, "mid3");
            m_pozImages.Add(92, "mid3");

            m_imageList = new ImageList();
            m_imageList.Images.Add("project", Resources.project);
            m_imageList.Images.Add("group", Resources.group);
            m_imageList.Images.Add("doc", Resources.doc);
            m_imageList.Images.Add("project_minus", Resources.project_minus);
            m_imageList.Images.Add("group_minus", Resources.group_minus);
            m_imageList.Images.Add("doc_minus", Resources.doc_minus);
            m_imageList.Images.Add("lid", Resources.lid);
            m_imageList.Images.Add("mid3", Resources.mid3);
            m_imageList.Images.Add("oid", Resources.oid);
            m_imageList.Images.Add("bid", Resources.bid);
            m_imageList.Images.Add("cid", Resources.cid);
            m_imageList.Images.Add("pok", Resources.pok);
        }
        public static string GetTable(int num_kod)
        {
            return m_tables[num_kod];
        }
        public static string GetChildTable(int num_kod)
        {
            return m_child_tables[num_kod];
        }
        public static string GetPozImageKey(int num_kod)
        {
            return m_pozImages[num_kod];
        }
        public static Image GetPozImage(int num_kod)
        {
            return m_imageList.Images[m_pozImages[num_kod]];
        }
        public static string GetNodeImageKey(BaseObject o)
        {
            if (o is ProjectObject)
            {
                return (o.status == Closed.N) ? "project" : "project_minus"; ;
            }
            if (o is GroupObject)
            {
                return (o.status == Closed.N) ? "group" : "group_minus"; ;
            }
            if (o is DocObject)
            {
                return (o.status == Closed.N) ? "doc" : "doc_minus"; ;
            }
            return string.Empty;
        }
        public static ImageList ImageList { get { return m_imageList; } }

        public static string NumKodString(int num_kod)
        {
            return m_kodes[num_kod];
        }
 
    }
}
