using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace libspec.Contrlos
{
    public partial class SpecTabPage : TabPage
    {
        public SpecTabPage(string text)
            : base(text)
        {
            InitializeComponent();
            this.Controls.Add(pozVew);
        }

        public SpecTabPage(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public SpecPozVew View { get { return pozVew; } }
    }
}
