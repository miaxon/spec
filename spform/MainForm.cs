﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using libspec;
namespace spform
{
    public partial class MainForm : Form
    {

        public MainForm()
        {

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SpecMain se = new SpecMain(this);
        }
    }
}