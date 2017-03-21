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
    public partial class Viewer : Form
    {
        public Viewer(string path)
        {
            InitializeComponent();
            web.Navigate(path);
            this.Text = path;
            this.Show();
        }

    }
}
