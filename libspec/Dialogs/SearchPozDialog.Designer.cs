﻿namespace libspec.View.Dialogs
{
    partial class SearchPozDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchPozDialog));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tbtn_lid = new System.Windows.Forms.ToolStripButton();
            this.tbtn_bid1 = new System.Windows.Forms.ToolStripButton();
            this.tbtn_bid2 = new System.Windows.Forms.ToolStripButton();
            this.tbtn_oid = new System.Windows.Forms.ToolStripButton();
            this.tbtn_pok = new System.Windows.Forms.ToolStripButton();
            this.tbtn_cid = new System.Windows.Forms.ToolStripButton();
            this.tbtn_mid = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.ttxtObozn = new System.Windows.Forms.ToolStripTextBox();
            this.tbtnSearchObozn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.ttxtGost = new System.Windows.Forms.ToolStripTextBox();
            this.tbtnSearchGost = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tbtnClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tbtnIsert = new System.Windows.Forms.ToolStripButton();
            this.treeView = new AdvancedDataGridView.TreeGridView();
            this.obozn = new AdvancedDataGridView.TreeGridColumn();
            this.naimen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.num_kol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.marka = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kei = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.num_kfr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.num_knr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.num_kod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.stlblEdit = new System.Windows.Forms.ToolStripStatusLabel();
            this.stlblAction = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeView)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbtn_lid,
            this.tbtn_bid1,
            this.tbtn_bid2,
            this.tbtn_oid,
            this.tbtn_pok,
            this.tbtn_cid,
            this.tbtn_mid,
            this.toolStripSeparator4,
            this.toolStripLabel1,
            this.ttxtObozn,
            this.tbtnSearchObozn,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.ttxtGost,
            this.tbtnSearchGost,
            this.toolStripSeparator2,
            this.tbtnClear,
            this.toolStripSeparator3,
            this.tbtnIsert});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1131, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
            // 
            // tbtn_lid
            // 
            this.tbtn_lid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtn_lid.Image = global::libspec.View.Properties.Resources.lid;
            this.tbtn_lid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtn_lid.Name = "tbtn_lid";
            this.tbtn_lid.Size = new System.Drawing.Size(23, 22);
            this.tbtn_lid.Tag = "0";
            this.tbtn_lid.Text = "toolStripButton1";
            this.tbtn_lid.ToolTipText = "лицевые чертежи";
            this.tbtn_lid.Click += new System.EventHandler(this.tbtn_num_kod_Click);
            // 
            // tbtn_bid1
            // 
            this.tbtn_bid1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtn_bid1.Image = global::libspec.View.Properties.Resources.bid;
            this.tbtn_bid1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtn_bid1.Name = "tbtn_bid1";
            this.tbtn_bid1.Size = new System.Drawing.Size(23, 22);
            this.tbtn_bid1.Tag = "1";
            this.tbtn_bid1.Text = "toolStripButton2";
            this.tbtn_bid1.ToolTipText = "покупные 1";
            this.tbtn_bid1.Click += new System.EventHandler(this.tbtn_num_kod_Click);
            // 
            // tbtn_bid2
            // 
            this.tbtn_bid2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtn_bid2.Image = global::libspec.View.Properties.Resources.bid;
            this.tbtn_bid2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtn_bid2.Name = "tbtn_bid2";
            this.tbtn_bid2.Size = new System.Drawing.Size(23, 22);
            this.tbtn_bid2.Tag = "2";
            this.tbtn_bid2.Text = "toolStripButton3";
            this.tbtn_bid2.ToolTipText = "покупные 2";
            this.tbtn_bid2.Click += new System.EventHandler(this.tbtn_num_kod_Click);
            // 
            // tbtn_oid
            // 
            this.tbtn_oid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtn_oid.Image = global::libspec.View.Properties.Resources.oid;
            this.tbtn_oid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtn_oid.Name = "tbtn_oid";
            this.tbtn_oid.Size = new System.Drawing.Size(23, 22);
            this.tbtn_oid.Tag = "3";
            this.tbtn_oid.Text = "toolStripButton4";
            this.tbtn_oid.ToolTipText = "обезличенные черетежи";
            this.tbtn_oid.Click += new System.EventHandler(this.tbtn_num_kod_Click);
            // 
            // tbtn_pok
            // 
            this.tbtn_pok.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtn_pok.Image = global::libspec.View.Properties.Resources.pok;
            this.tbtn_pok.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtn_pok.Name = "tbtn_pok";
            this.tbtn_pok.Size = new System.Drawing.Size(23, 22);
            this.tbtn_pok.Tag = "4";
            this.tbtn_pok.Text = "toolStripButton5";
            this.tbtn_pok.ToolTipText = "поковки";
            this.tbtn_pok.Click += new System.EventHandler(this.tbtn_num_kod_Click);
            // 
            // tbtn_cid
            // 
            this.tbtn_cid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtn_cid.Image = global::libspec.View.Properties.Resources.cid;
            this.tbtn_cid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtn_cid.Name = "tbtn_cid";
            this.tbtn_cid.Size = new System.Drawing.Size(23, 22);
            this.tbtn_cid.Tag = "7";
            this.tbtn_cid.Text = "toolStripButton6";
            this.tbtn_cid.ToolTipText = "сложные материалы";
            this.tbtn_cid.Click += new System.EventHandler(this.tbtn_num_kod_Click);
            // 
            // tbtn_mid
            // 
            this.tbtn_mid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtn_mid.Image = global::libspec.View.Properties.Resources.mid3;
            this.tbtn_mid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtn_mid.Name = "tbtn_mid";
            this.tbtn_mid.Size = new System.Drawing.Size(23, 22);
            this.tbtn_mid.Tag = "9";
            this.tbtn_mid.Text = "toolStripButton7";
            this.tbtn_mid.ToolTipText = "материалы";
            this.tbtn_mid.Click += new System.EventHandler(this.tbtn_num_kod_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(97, 22);
            this.toolStripLabel1.Text = "ОБОЗНАЧЕНИЕ:";
            // 
            // ttxtObozn
            // 
            this.ttxtObozn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ttxtObozn.Name = "ttxtObozn";
            this.ttxtObozn.Size = new System.Drawing.Size(100, 25);
            this.ttxtObozn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxtObozn_KeyDown);
            // 
            // tbtnSearchObozn
            // 
            this.tbtnSearchObozn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnSearchObozn.Image = global::libspec.View.Properties.Resources.find;
            this.tbtnSearchObozn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnSearchObozn.Name = "tbtnSearchObozn";
            this.tbtnSearchObozn.Size = new System.Drawing.Size(23, 22);
            this.tbtnSearchObozn.Text = "toolStripButton1";
            this.tbtnSearchObozn.ToolTipText = "искать";
            this.tbtnSearchObozn.Click += new System.EventHandler(this.tbtnSearchObozn_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(77, 22);
            this.toolStripLabel2.Text = "НОРМАТИВ:";
            // 
            // ttxtGost
            // 
            this.ttxtGost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ttxtGost.Name = "ttxtGost";
            this.ttxtGost.Size = new System.Drawing.Size(100, 25);
            this.ttxtGost.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxtGost_KeyDown);
            // 
            // tbtnSearchGost
            // 
            this.tbtnSearchGost.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnSearchGost.Image = global::libspec.View.Properties.Resources.find;
            this.tbtnSearchGost.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnSearchGost.Name = "tbtnSearchGost";
            this.tbtnSearchGost.Size = new System.Drawing.Size(23, 22);
            this.tbtnSearchGost.Text = "toolStripButton2";
            this.tbtnSearchGost.ToolTipText = "искать";
            this.tbtnSearchGost.Click += new System.EventHandler(this.tbtnSearchGost_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tbtnClear
            // 
            this.tbtnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnClear.Image = global::libspec.View.Properties.Resources.clear;
            this.tbtnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnClear.Name = "tbtnClear";
            this.tbtnClear.Size = new System.Drawing.Size(23, 22);
            this.tbtnClear.Text = "toolStripButton1";
            this.tbtnClear.ToolTipText = "очистить";
            this.tbtnClear.Click += new System.EventHandler(this.tbtnClear_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tbtnIsert
            // 
            this.tbtnIsert.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnIsert.Image = global::libspec.View.Properties.Resources.accept;
            this.tbtnIsert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnIsert.Name = "tbtnIsert";
            this.tbtnIsert.Size = new System.Drawing.Size(23, 22);
            this.tbtnIsert.Text = "Добавить позицию";
            this.tbtnIsert.ToolTipText = "добавить в текущий документ";
            this.tbtnIsert.Click += new System.EventHandler(this.tbtnIsert_Click);
            // 
            // treeView
            // 
            this.treeView.AllowUserToAddRows = false;
            this.treeView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Honeydew;
            this.treeView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.treeView.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.treeView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.treeView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.obozn,
            this.naimen,
            this.num_kol,
            this.gost,
            this.marka,
            this.kei,
            this.num_kfr,
            this.num_knr,
            this.num_kod,
            this.descr});
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.treeView.ImageList = null;
            this.treeView.Location = new System.Drawing.Point(0, 25);
            this.treeView.Name = "treeView";
            this.treeView.RowHeadersVisible = false;
            this.treeView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.treeView.Size = new System.Drawing.Size(1131, 464);
            this.treeView.TabIndex = 2;
            this.treeView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.treeView_CellBeginEdit);
            this.treeView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.treeView_CellDoubleClick);
            this.treeView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.treeView_CellEndEdit);
            this.treeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeView_KeyDown);
            // 
            // obozn
            // 
            this.obozn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.obozn.DefaultNodeImage = null;
            this.obozn.HeaderText = "ОБОЗНАЧЕНИЕ";
            this.obozn.Name = "obozn";
            this.obozn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.obozn.Width = 96;
            // 
            // naimen
            // 
            this.naimen.HeaderText = "НАИМЕНОВАНИЕ";
            this.naimen.Name = "naimen";
            this.naimen.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.naimen.Width = 300;
            // 
            // num_kol
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.num_kol.DefaultCellStyle = dataGridViewCellStyle3;
            this.num_kol.HeaderText = "КОЛ";
            this.num_kol.Name = "num_kol";
            this.num_kol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.num_kol.Width = 60;
            // 
            // gost
            // 
            this.gost.HeaderText = "НОРМАТИВ";
            this.gost.Name = "gost";
            this.gost.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // marka
            // 
            this.marka.HeaderText = "МАРКА";
            this.marka.Name = "marka";
            this.marka.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // kei
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.kei.DefaultCellStyle = dataGridViewCellStyle4;
            this.kei.HeaderText = "КЕИ";
            this.kei.Name = "kei";
            this.kei.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.kei.Width = 60;
            // 
            // num_kfr
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.num_kfr.DefaultCellStyle = dataGridViewCellStyle5;
            this.num_kfr.HeaderText = "КФ";
            this.num_kfr.Name = "num_kfr";
            this.num_kfr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.num_kfr.Width = 60;
            // 
            // num_knr
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.num_knr.DefaultCellStyle = dataGridViewCellStyle6;
            this.num_knr.HeaderText = "НР";
            this.num_knr.Name = "num_knr";
            this.num_knr.ReadOnly = true;
            this.num_knr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.num_knr.Width = 60;
            // 
            // num_kod
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.num_kod.DefaultCellStyle = dataGridViewCellStyle7;
            this.num_kod.HeaderText = "КП";
            this.num_kod.Name = "num_kod";
            this.num_kod.ReadOnly = true;
            this.num_kod.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.num_kod.Width = 50;
            // 
            // descr
            // 
            this.descr.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Gray;
            this.descr.DefaultCellStyle = dataGridViewCellStyle8;
            this.descr.HeaderText = "ИНФОРМАЦИЯ";
            this.descr.Name = "descr";
            this.descr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stlblEdit,
            this.stlblAction});
            this.statusStrip.Location = new System.Drawing.Point(0, 489);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statusStrip.Size = new System.Drawing.Size(1131, 22);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip";
            // 
            // stlblEdit
            // 
            this.stlblEdit.Name = "stlblEdit";
            this.stlblEdit.Size = new System.Drawing.Size(1060, 17);
            this.stlblEdit.Spring = true;
            this.stlblEdit.Text = "stlblEdit";
            this.stlblEdit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // stlblAction
            // 
            this.stlblAction.Name = "stlblAction";
            this.stlblAction.Size = new System.Drawing.Size(56, 17);
            this.stlblAction.Text = "stlblNum";
            // 
            // SearchPozDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1131, 511);
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SearchPozDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SearchPozDialog";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeView)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox ttxtObozn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox ttxtGost;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tbtnIsert;
        private System.Windows.Forms.ToolStripButton tbtnSearchObozn;
        private System.Windows.Forms.ToolStripButton tbtnSearchGost;
        private System.Windows.Forms.ToolStripButton tbtnClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private AdvancedDataGridView.TreeGridView treeView;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripButton tbtn_lid;
        private System.Windows.Forms.ToolStripButton tbtn_bid1;
        private System.Windows.Forms.ToolStripButton tbtn_bid2;
        private System.Windows.Forms.ToolStripButton tbtn_oid;
        private System.Windows.Forms.ToolStripButton tbtn_pok;
        private System.Windows.Forms.ToolStripButton tbtn_cid;
        private System.Windows.Forms.ToolStripButton tbtn_mid;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripStatusLabel stlblEdit;
        private System.Windows.Forms.ToolStripStatusLabel stlblAction;
        private AdvancedDataGridView.TreeGridColumn obozn;
        private System.Windows.Forms.DataGridViewTextBoxColumn naimen;
        private System.Windows.Forms.DataGridViewTextBoxColumn num_kol;
        private System.Windows.Forms.DataGridViewTextBoxColumn gost;
        private System.Windows.Forms.DataGridViewTextBoxColumn marka;
        private System.Windows.Forms.DataGridViewTextBoxColumn kei;
        private System.Windows.Forms.DataGridViewTextBoxColumn num_kfr;
        private System.Windows.Forms.DataGridViewTextBoxColumn num_knr;
        private System.Windows.Forms.DataGridViewTextBoxColumn num_kod;
        private System.Windows.Forms.DataGridViewTextBoxColumn descr;

    }
}