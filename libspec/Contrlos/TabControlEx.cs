using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace libspec.Contrlos
{
    public partial class TabControlEx : TabControl
    {
        private Point m_lastClickedPoint;
        public TabControlEx()
        {
            InitializeComponent();
            
        }

        public TabControlEx(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        private void m_cms_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            for (int i = 0; i < this.TabCount; i++)
            {
                Rectangle rect = this.GetTabRect(i);
                if(rect.Contains(this.PointToClient(m_lastClickedPoint)))
                {
                    this.TabPages.RemoveAt(i);
                }
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                m_lastClickedPoint = Cursor.Position;
                m_cms.Show(Cursor.Position);
            }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                m_lastClickedPoint = Cursor.Position;
                for (int i = 0; i < this.TabCount; i++)
                {
                    Rectangle rect = this.GetTabRect(i);
                    if (rect.Contains(this.PointToClient(m_lastClickedPoint)))
                    {
                        this.TabPages.RemoveAt(i);
                    }
                }
            }
        }
        public SpecPozVew CurrentView { get { return (SelectedTab as SpecTabPage).View; } }
    }
}
