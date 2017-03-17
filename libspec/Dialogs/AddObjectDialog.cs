using libspec.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace libspec.Dialogs
{
    public partial class AddObjectDialog : Form
    {
        public AddObjectDialog()
        {
            InitializeComponent();
        }
        public AddObjectDialog(ViewEvent.ButtonAction action)
        {
            InitializeComponent();
            Text = Utils.ActionString(action);
        }
        public BaseObject Object
        {
            get
            {
                return new BaseObject(new object[] {
                    UInt32.Parse("0"),
                    txt_obozn.Text,
                    txt_naimen.Text,
                    DateTime.Now.ToString("Создан: dd-MM-yy HH:mm:ss\r\n") + txt_descr.Text,
                    "N" }); 
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
