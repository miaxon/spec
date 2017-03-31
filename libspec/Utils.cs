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
using System.Diagnostics;
using System.Security.Cryptography;

namespace libspec.View
{

    public static class Utils
    {
        private static Dictionary<int, string> m_tables;
        private static Dictionary<int, string> m_child_tables;
        private static Dictionary<int, string> m_child_mid_tables;
        private static Dictionary<int, string> m_parent_tables;
        private static Dictionary<int, string> m_pozImages;
        private static Dictionary<int, string> m_kodes;
        private static Dictionary<int, string> m_actions;
        private static ImageList m_imageList;
        public static string Server;
        public static string User;
        public static string Password;
        public static List<KeiObject> KidList;
        public static void InitMaps()
        {

            m_actions = new Dictionary<int, string>();
            m_actions.Add(0, "Добавить проект");
            m_actions.Add(1, "Добавить группу");
            m_actions.Add(2, "Добавить документ");
            m_actions.Add(8, "Добавить запись");
            m_actions.Add(9, "Добавить корневую запись");

            m_kodes = new Dictionary<int, string>();
            m_kodes.Add(0, "Лицевые чертежи");
            m_kodes.Add(1, "Покупные 1");
            m_kodes.Add(2, "Покупные 2");
            m_kodes.Add(3, "Обезличенные черетежи");
            m_kodes.Add(4, "Поковки");
            m_kodes.Add(7, "Сложные материалы");
            m_kodes.Add(9, "Материалы 3");
            m_kodes.Add(92, "Материалы 2");
            m_kodes.Add(93, "Материалы 1");
            m_kodes.Add(94, "Материалы 0");
            m_kodes.Add(100, "Сложные материалы");

            m_tables = new Dictionary<int, string>();
            m_tables.Add(0, "lid");
            m_tables.Add(1, "bid1");
            m_tables.Add(2, "bid2");
            m_tables.Add(3, "oid");
            m_tables.Add(4, "pok");
            m_tables.Add(7, "cid");
            m_tables.Add(9, "mid3");
            m_tables.Add(92, "mid2");
            m_tables.Add(93, "mid1");
            m_tables.Add(94, "mid0");
            m_tables.Add(100, "cid_");

            m_child_tables = new Dictionary<int, string>();
            m_child_tables.Add(0, "lid_old");
            m_child_tables.Add(3, "oid_old");
            m_child_tables.Add(7, "cid_");// cid -> cid_

            m_child_mid_tables = new Dictionary<int, string>();
            m_child_mid_tables.Add(92, "mid3"); // mid2 -> mid3
            m_child_mid_tables.Add(93, "mid2"); // mid1 -> mid2
            m_child_mid_tables.Add(94, "mid1"); // mid0 -> mid2

            m_parent_tables = new Dictionary<int, string>();
            m_parent_tables.Add(9, "mid2"); // mid3 -> mid2
            m_parent_tables.Add(92, "mid1"); // mid2 -> mid1
            m_parent_tables.Add(93, "mid0"); // mid1 -> mid0

            m_pozImages = new Dictionary<int, string>();
            m_pozImages.Add(0, "lid");
            m_pozImages.Add(1, "bid");
            m_pozImages.Add(2, "bid");
            m_pozImages.Add(3, "oid");
            m_pozImages.Add(4, "pok");
            m_pozImages.Add(7, "cid");
            m_pozImages.Add(9, "mid3");
            m_pozImages.Add(92, "mid0");
            m_pozImages.Add(93, "mid0");
            m_pozImages.Add(94, "mid0");
            m_pozImages.Add(100, "cid");


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
            m_imageList.Images.Add("mid2", Resources.mid);
            m_imageList.Images.Add("mid1", Resources.mid);
            m_imageList.Images.Add("mid0", Resources.mid);


        }
        public static string GetTable(int num_kod)
        {
            return m_tables[num_kod];
        }
        public static string GetChildMidTable(int num_kod)
        {
            string str = string.Empty;
            try
            {
                str = m_child_mid_tables[num_kod];
            }
            catch
            {
                return string.Empty;
            }
            return str;
        }
        public static string GetChildTable(int num_kod)
        {
            string str = string.Empty;
            try
            {
                str = m_child_tables[num_kod];
            }
            catch
            {
                return string.Empty;
            }
            return str;
        }
        public static int OboznLength(string table)
        {
            int ret = 0;
            switch (table)
            {
                case "mid3":
                    ret = 11;
                    break;
                case "mid2":
                    ret = 6;
                    break;
                case "mid1":
                    ret = 4;
                    break;
                case "mid0":
                    ret = 4;
                    break;
            }
            return ret;
        }
        public static int OboznLength(int num_kod)
        {
            int ret = 0;
            switch (num_kod)
            {
                case 9:
                    ret = 11;
                    break;
                case 92:
                    ret = 6;
                    break;
                case 93:
                    ret = 4;
                    break;
                case 94:
                    ret = 4;
                    break;
            }
            return ret;
        }
        public static bool CheckOboznLength(string obozn, int num_kod)
        {
            bool ret = true;
            switch (num_kod)
            {
                case 9:
                    ret = obozn.Length == 11;
                    break;
                case 92:
                    ret = obozn.Length == 6;
                    break;
                case 93:
                    ret = obozn.Length == 4;
                    break;
                case 94:
                    ret = obozn.Length == 4;
                    break;
            }
            return ret;
        }
        public static int SearchOboznLength(int kod)
        {
            int ret = 0;
            switch (kod)
            {
                case 9:
                    ret = 5;
                    break;
                case 92:
                    ret = 4;
                    break;
                case 93:
                    ret = 3;
                    break;
                case 94:
                    ret = 2;
                    break;
            }
            return ret;
        }
        public static int GetParentNumKod(int kod)
        {
            int ret = 0;
            switch (kod)
            {
                case 9:
                    ret = 92;
                    break;
                case 92:
                    ret = 93;
                    break;
                case 93:
                    ret = 94;
                    break;
            }
            return ret;
        }
        public static string GetParentTable(int num_kod)
        {
            string str = string.Empty;
            try
            {
                str = m_parent_tables[num_kod];
            }
            catch
            {
                return string.Empty;
            }
            return str;
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
        public static Image GetMidImage(MidObject o)
        {
            if (o is MidObject)
            {
                return m_imageList.Images["mid0"];
            }
            return null;
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
            //string KeyString = GenerateAPassKey("PassKey");
            //string EncryptedPassword = Encrypt("spec", KeyString);
            //string DecryptedPassword = Decrypt(EncryptedPassword, KeyString); 

            string user = GetUserName();
            string p = Directory.GetCurrentDirectory() + @"\Config\config.xml";
            if (!File.Exists(p))
            {
                Utils.Error("Не найден файл конфигураци config.xml.");
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
                        Utils.Error("Файл конфигурации не содержит address.");
                    else
                        Server = reader.Value;
                    if (!reader.MoveToAttribute("user"))
                        Utils.Error("Файл конфигурации не содержит user.");
                    else
                        User = reader.Value;
                    if (!reader.MoveToAttribute("password"))
                        Utils.Error("Файл конфигурации не содержит password.");
                    else
                        Password = Decrypt(reader.Value, GenerateAPassKey("PassKey"));
                    if (!reader.MoveToAttribute("admins"))
                        Utils.Error("Файл конфигурации не содержит admins.");
                    else
                        admis = reader.Value;

                }
            }
            reader.Close();
            ret = !string.IsNullOrEmpty(Server) && !string.IsNullOrEmpty(admis) && admis.Contains(user);
            return ret;
        }
        public static string GetKeiNaimen(string kei_num)
        {
            KeiObject k = KidList.Find(o => o.obozn == kei_num);
            if (k != null)
                return k.naimen;
            else
                return kei_num;
        }
        public static string GetKeiObozn(string kei_str)
        {
            KeiObject k = KidList.Find(o => o.naimen == kei_str);
            if (k != null)
                return k.obozn;
            else
                return kei_str;
        }
        public static string[] GetChildTables()
        {
            return new string[] { "lid_old", "oid_old", "cid_" };
        }
        public static void Error(string text)
        {
            MessageBox.Show(text, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void DBError(string text, MySql.Data.MySqlClient.MySqlException ex)
        {
            if (ex.Number == 1062)
                Utils.Error("Значение уже существует.");
            else
                MessageBox.Show(text + ": " + ex.Message, "Ошибка MySql", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static bool Warning(string text)
        {
            return MessageBox.Show(text, "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK;
        }
        public static void Info(string text)
        {
            MessageBox.Show(text, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void Version()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.ProductVersion;
            Info(version);
            Clipboard.SetText(version);
        }
        private static string GenerateAPassKey(string passphrase)
        {
            // Pass Phrase can be any string
            string passPhrase = passphrase;
            // Salt Value can be any string(for simplicity use the same value as used for the pass phrase)
            string saltValue = passphrase;
            // Hash Algorithm can be "SHA1 or MD5"
            string hashAlgorithm = "SHA1";
            // Password Iterations can be any number
            int passwordIterations = 2;
            // Key Size can be 128,192 or 256
            int keySize = 256;
            // Convert Salt passphrase string to a Byte Array
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            // Using System.Security.Cryptography.PasswordDeriveBytes to create the Key
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
            //When creating a Key Byte array from the base64 string the Key must have 32 dimensions.
            byte[] Key = pdb.GetBytes(keySize / 11);
            String KeyString = Convert.ToBase64String(Key);

            return KeyString;
        }

        //Save the keystring some place like your database and use it to decrypt and encrypt
        //any text string or text file etc. Make sure you dont lose it though.

        private static string Encrypt(string plainStr, string KeyString)
        {
            RijndaelManaged aesEncryption = new RijndaelManaged();
            aesEncryption.KeySize = 256;
            aesEncryption.BlockSize = 128;
            aesEncryption.Mode = CipherMode.ECB;
            aesEncryption.Padding = PaddingMode.ISO10126;
            byte[] KeyInBytes = Encoding.UTF8.GetBytes(KeyString);
            aesEncryption.Key = KeyInBytes;
            byte[] plainText = ASCIIEncoding.UTF8.GetBytes(plainStr);
            ICryptoTransform crypto = aesEncryption.CreateEncryptor();
            byte[] cipherText = crypto.TransformFinalBlock(plainText, 0, plainText.Length);
            return Convert.ToBase64String(cipherText);
        }

        private static string Decrypt(string encryptedText, string KeyString)
        {
            RijndaelManaged aesEncryption = new RijndaelManaged();
            aesEncryption.KeySize = 256;
            aesEncryption.BlockSize = 128;
            aesEncryption.Mode = CipherMode.ECB;
            aesEncryption.Padding = PaddingMode.ISO10126;
            byte[] KeyInBytes = Encoding.UTF8.GetBytes(KeyString);
            aesEncryption.Key = KeyInBytes;
            ICryptoTransform decrypto = aesEncryption.CreateDecryptor();
            byte[] encryptedBytes = Convert.FromBase64CharArray(encryptedText.ToCharArray(), 0, encryptedText.Length);
            return ASCIIEncoding.UTF8.GetString(decrypto.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length));
        } 
    }
}
