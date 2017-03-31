using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace libspec.View.Dialogs
{
    public partial class AboutDialog : Form
    {
        public AboutDialog()
        {
            InitializeComponent();
        }

        private void AboutDialog_Load(object sender, EventArgs e)
        {
            lblBranch.Text = GitVersionInformation.BranchName;
            lblCommit.Text = GitVersionInformation.CommitsSinceVersionSource;
            lblDate.Text = GitVersionInformation.CommitDate;
            lblSHA.Text = GitVersionInformation.Sha.Substring(0, 10);
            lblVersion.Text = GitVersionInformation.AssemblySemVer;
        }

        private void lnkUrl_Click(object sender, EventArgs e)
        {
            Process.Start(lnkUrl.Text);
        }
    }
}
