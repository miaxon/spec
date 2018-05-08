using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AdvancedDataGridView;
using libspec.View.ViewEvent;
using libspec.View.Objects;
using libspec.View.Dialogs;
using System.IO;
using System.Diagnostics;

namespace libspec.View
{
    public partial class SpecViewTree : UserControl
    {
        public void RemoveCurrentNode()
        {
            if (m_nodeCurrent == null)
                return;
            if (m_nodeCurrent.Parent == null)
                treeView.Nodes.Remove(m_nodeCurrent);
            else
                m_nodeCurrent.Parent.Nodes.Remove(m_nodeCurrent);
        }
        public void AddNode(object obj)
        {
            if (m_nodeCurrent == null)
            {
                ProjectObject o = obj as ProjectObject;
                TreeGridNode node = treeView.Nodes.Add(o.obozn);
                UpdateNode(o, node);
            }
            else
            {
                if (obj is GroupObject)
                {
                    GroupObject o = obj as GroupObject;
                    TreeGridNode node = m_nodeCurrent.Nodes.Add(o.obozn);
                    UpdateNode(o, node);
                }
                if (obj is DocObject)
                {
                    DocObject o = obj as DocObject;
                    TreeGridNode node = m_nodeCurrent.Nodes.Add(o.obozn);
                    UpdateNode(o, node);
                }
            }
        }
        public void UpdateNode(object obj, TreeGridNode node = null)
        {
            if (node == null)
                node = m_nodeCurrent;
            if (obj is ProjectObject)
            {
                ProjectObject o = obj as ProjectObject;
                node.Image = Utils.GetNodeImage(o);
                node.Cells[1].Value = o.naimen;
                node.Cells[9].Value = o.descr;
                node.Tag = o;
                node.Cells[0].ReadOnly = false;
                node.Cells[1].ReadOnly = false;
                node.Cells[2].ReadOnly = true;
                node.Cells[6].ReadOnly = true;

            }
            if (obj is GroupObject)
            {
                GroupObject o = obj as GroupObject;
                node.Image = Utils.GetNodeImage(o);
                node.Cells[1].Value = o.naimen;
                node.Cells[9].Value = o.descr;
                node.Tag = o;
                node.Cells[0].ReadOnly = false;
                node.Cells[1].ReadOnly = false;
                node.Cells[2].ReadOnly = true;
                node.Cells[6].ReadOnly = true;
                node.DefaultCellStyle.ForeColor = o.status == Closed.Y ? Color.Gray : Color.Black;
            }
            if (obj is DocObject)
            {
                DocObject o = obj as DocObject;
                node.Image = Utils.GetNodeImage(o);
                node.Cells[1].Value = o.naimen;
                node.Cells[2].Value = o.num_kol;
                node.Cells[9].Value = o.descr;
                node.Tag = o;
                node.Cells[0].ReadOnly = false;
                node.Cells[1].ReadOnly = false;
                node.Cells[2].ReadOnly = false;
                node.Cells[6].ReadOnly = true;
                node.DefaultCellStyle.ForeColor = o.status == Closed.Y ? Color.Gray : node.Parent.DefaultCellStyle.ForeColor; ;
            }
            if (obj is PozObject)
            {
                PozObject o = obj as PozObject;
                node.Image = Utils.GetPozImage(o.num_kod);
                node.Cells[0].ToolTipText = Utils.NumKodString(o.num_kod);
                node.Cells[1].Value = o.naimen;
                node.Cells[2].Value = o.num_kol;
                node.Cells[3].Value = o.gost;
                node.Cells[4].Value = o.marka;
                node.Cells[5].Value = o.keiString;
                node.Cells[6].Value = o.num_kfr;
                node.Cells[7].Value = o.num_knr;
                node.Cells[8].Value = o.num_kod;
                node.Cells[9].Value = o.descr;
                if (node.Level == 4)
                {
                    node.Cells[0].ReadOnly = true;
                    node.Cells[1].ReadOnly = true;
                    node.Cells[2].ReadOnly = false;
                    node.Cells[6].ReadOnly = false;
                }
                if (node.Level > 4)
                {
                    node.Cells[0].ReadOnly = true;
                    node.Cells[1].ReadOnly = true;
                    node.Cells[2].ReadOnly = true;
                    node.Cells[6].ReadOnly = true;
                }
                node.Tag = o;
                node.DefaultCellStyle.ForeColor = node.Parent.DefaultCellStyle.ForeColor;

            }


        }
    }
}