using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace transformer
{
    public partial class DbgForm : Form
    {
        public DbgForm()
        {
            InitializeComponent();
            this.BringToFront();
            this.TopMost = true;
            this.CenterToScreen();
        }

        private void DbgForm_Load(object sender, EventArgs e)
        {

        }
        public void trace(object o)
        {
            tf.AppendText(o.ToString()+"\r\n");
        }

        private void DbgForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
