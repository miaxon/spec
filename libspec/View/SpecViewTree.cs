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
        #region events
        public event EventHandler<NodeClickEventArgs> NodeClickEvent;
        public event EventHandler<ExpandEventArgs> ExpandEvent;
        public event EventHandler<ButtonActionEventArgs> ButtonActionEvent;
        public event EventHandler<SearchEventArgs> SearchEvent;
        public event EventHandler<AddPozEventArgs> AddPozEvent;
        public event EventHandler<MovePozEventArgs> MovePozEvent;
        public event EventHandler<AddDocEventArgs> AddDocEvent;
        public event EventHandler<MoveDocEventArgs> MoveDocEvent;
        public event EventHandler<NodeEditEventArgs> NodeEditEvent;
        #endregion
        private TreeGridNode m_nodeCurrent;
        public SpecViewTree()
        {
            InitializeComponent();
            treeView.ImageList = Utils.ImageList;
            statusStrip.ImageList = Utils.ImageList;
            stlblAction.Text = "";
            //treeView.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            treeView.SelectionMode = DataGridViewSelectionMode.CellSelect;

        }
    }
}
