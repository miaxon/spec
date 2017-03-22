using libspec.View.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace libspec.View.Dialogs
{
    public partial class AddObjectDialog : Form
    {
        private ViewEvent.ButtonAction m_action;
        public AddObjectDialog()
        {
            InitializeComponent();
        }
        public AddObjectDialog(ViewEvent.ButtonAction action, string name = "")
        {
            InitializeComponent();
            Text = Utils.ActionString(action);
            m_action = action;
            txt_obozn.Text = name;
        }
        public BaseObject Object
        {
            get
            {
                switch (m_action)
                {
                    case ViewEvent.ButtonAction.AddProject:
                        return new ProjectObject(new object[] {
                                                UInt32.Parse("0"),
                                                txt_obozn.Text,
                                                txt_naimen.Text,
                                                DateTime.Now.ToString("Создан: dd-MM-yy HH:mm:ss\r\n  ") + txt_descr.Text,
                                                "N" });
                    case ViewEvent.ButtonAction.AddGroup:
                        return new GroupObject(new object[] {
                                                UInt32.Parse("0"),
                                                txt_obozn.Text,
                                                txt_naimen.Text,
                                                DateTime.Now.ToString("Создан: dd-MM-yy HH:mm:ss  \r\n") +  txt_descr.Text,
                                                "N" });
                    case ViewEvent.ButtonAction.AddDoc:
                        return new DocObject(new object[] {
                                                UInt32.Parse("0"),
                                                txt_obozn.Text,
                                                txt_naimen.Text,
                                                DateTime.Now.ToString("Создан: dd-MM-yy HH:mm:ss\r\n  ") + txt_descr.Text,
                                                "N",
                                                UInt16.Parse("1"),
                                                UInt32.Parse("0")});

                }
                return null;
            }
        }

        private void txt_obozn_TextChanged(object sender, EventArgs e)
        {
            string s = txt_obozn.Text;
            if (string.IsNullOrWhiteSpace(s) || string.IsNullOrEmpty(s))
                btnOk.Enabled = false;
            else
                btnOk.Enabled = true;
        }
    }
}
