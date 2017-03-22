using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using libspec.View.Properties;
using libspec.View.Objects;
using System.Drawing;
using Microsoft.Win32;
using System.IO;
using System.Security.Principal;
using System.Xml;

namespace libspec.View
{

    public static class Utils
    {
        private static Dictionary<int, string> m_tables;
        private static Dictionary<int, string> m_child_tables;
        private static Dictionary<int, string> m_pozImages;
        private static Dictionary<int, string> m_kodes;
        private static Dictionary<int, string> m_actions;
        private static ImageList m_imageList;
        public static string Server;
        public static List<KidObject> KidList;
        public static void InitMaps()
        {

            m_actions = new Dictionary<int, string>();
            m_actions.Add(0, "Добавить проект");
            m_actions.Add(1, "Добавить группу");
            m_actions.Add(2, "Добавить документ");

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
            m_imageList.Images.Add("project_plus", Resources.project_plus);
            m_imageList.Images.Add("group_plus", Resources.group_plus);
            m_imageList.Images.Add("doc_plus", Resources.doc_plus);
            m_imageList.Images.Add("lid", Resources.lid);
            m_imageList.Images.Add("mid3", Resources.mid3);
            m_imageList.Images.Add("oid", Resources.oid);
            m_imageList.Images.Add("bid", Resources.bid);
            m_imageList.Images.Add("cid", Resources.cid);
            m_imageList.Images.Add("pok", Resources.pok);
            m_imageList.Images.Add("book", Resources.book);
            m_imageList.Images.Add("add", Resources.add);
            m_imageList.Images.Add("update", Resources.update);
            m_imageList.Images.Add("cross", Resources.cross);
            m_imageList.Images.Add("accept", Resources.accept);
            m_imageList.Images.Add("find", Resources.find);
            m_imageList.Images.Add("clear", Resources.clear);


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
        public static Image GetNodeImage(BaseObject o)
        {
            if (o is ProjectObject)
            {
                return (o.status == Closed.N) ? m_imageList.Images["project"] : m_imageList.Images["project_minus"];
            }
            if (o is GroupObject)
            {
                return (o.status == Closed.N) ? m_imageList.Images["group"] : m_imageList.Images["group_minus"];
            }
            if (o is DocObject)
            {
                return (o.status == Closed.N) ? m_imageList.Images["doc"] : m_imageList.Images["doc_minus"];
            }
            return null;
        }
        public static ImageList ImageList { get { return m_imageList; } }

        public static string NumKodString(int num_kod)
        {
            return m_kodes[num_kod];
        }

        public static string ActionString(ViewEvent.ButtonAction action)
        {
            return m_actions[(int)action];
        }

        public static void SaveProjectList(List<ProjectObject> list)
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey specKey = currentUserKey.CreateSubKey("Spec");
            foreach (string obozn in specKey.GetSubKeyNames())
                specKey.DeleteSubKey(obozn);
            foreach (ProjectObject o in list)
            {
                RegistryKey key = specKey.CreateSubKey(o.obozn);
                key.SetValue("id", o.id);
                key.SetValue("naimen", o.naimen);
                key.SetValue("descr", o.descr);
                key.SetValue("status", o.status);
            }
        }
        public static List<ProjectObject> LoadProjectList()
        {
            List<ProjectObject> list = new List<ProjectObject>();
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey specKey = currentUserKey.CreateSubKey("Spec");
            foreach (string obozn in specKey.GetSubKeyNames())
            {
                RegistryKey key = specKey.CreateSubKey(obozn);
                object[] values = new object[] { Convert.ToUInt32(key.GetValue("id")), obozn, key.GetValue("naimen"), key.GetValue("descr"), key.GetValue("status") };
                ProjectObject o = new ProjectObject(values);
                list.Add(o);
            }
            return list;
        }

        public static string GetRandomString()
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", ""); // Remove period.
            return path.ToUpper(); ;
        }

        public static string GetUserName()
        {
            WindowsIdentity idetnity = WindowsIdentity.GetCurrent();
            return idetnity.Name.Split('\\')[1];

        }

        public static bool CheckAccess()
        {
            string user = GetUserName();
            string p = Directory.GetCurrentDirectory() + @"\Config\config.xml";
            if (!File.Exists(p))
            {
                MessageBox.Show("Не найден файл конфигураци.");
                return false;
            }
            XmlTextReader reader = new XmlTextReader(p);
            bool ret = false;
            string admis = "";
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "server")
                {
                    if (!reader.MoveToAttribute("address"))

                        MessageBox.Show("Файл конфигурации не содержит address.");
                    else
                        Server = reader.Value;
                    if (!reader.MoveToAttribute("admins"))
                        MessageBox.Show("Файл конфигурации не содержит admins.");
                    else
                        admis = reader.Value;

                }
            }
            reader.Close();
            ret = !string.IsNullOrEmpty(Server) && !string.IsNullOrEmpty(admis) && admis.Contains(user);
            return ret;
        }
        public static string GetKidString(string kei)
        {
            KidObject k = KidList.Find(o => o.obozn == kei);
            if (k != null)
                return k.naimen;
            else
                return kei;
        }
    }
}
