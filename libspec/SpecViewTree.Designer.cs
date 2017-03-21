namespace libspec
{
    partial class SpecViewTree
    {
        /// <summary> 
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tbtnSelectProject = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tbtnAddProject = new System.Windows.Forms.ToolStripButton();
            this.tbtnAddGroup = new System.Windows.Forms.ToolStripButton();
            this.tbtnAddDoc = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tbtnOpen = new System.Windows.Forms.ToolStripButton();
            this.tbtnClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbtnDelObject = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tbntUpdate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tbtnCopy = new System.Windows.Forms.ToolStripButton();
            this.tbtnCut = new System.Windows.Forms.ToolStripButton();
            this.tbtnPaste = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tbtnAddPoz = new System.Windows.Forms.ToolStripButton();
            this.tbtnCalc = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.stlblNumChilds = new System.Windows.Forms.ToolStripStatusLabel();
            this.stlblAction = new System.Windows.Forms.ToolStripStatusLabel();
            this.treeView = new AdvancedDataGridView.TreeGridView();
            this.obozn = new AdvancedDataGridView.TreeGridColumn();
            this.naimen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.marka = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kei = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kfr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.knr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeView)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbtnSelectProject,
            this.toolStripSeparator3,
            this.tbtnAddProject,
            this.tbtnAddGroup,
            this.tbtnAddDoc,
            this.toolStripSeparator4,
            this.tbtnOpen,
            this.tbtnClose,
            this.toolStripSeparator1,
            this.tbtnDelObject,
            this.toolStripSeparator2,
            this.tbntUpdate,
            this.toolStripSeparator5,
            this.tbtnCopy,
            this.tbtnCut,
            this.tbtnPaste,
            this.toolStripSeparator6,
            this.tbtnAddPoz,
            this.tbtnCalc});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1380, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // tbtnSelectProject
            // 
            this.tbtnSelectProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnSelectProject.Image = global::libspec.Properties.Resources.book;
            this.tbtnSelectProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnSelectProject.Name = "tbtnSelectProject";
            this.tbtnSelectProject.Size = new System.Drawing.Size(23, 22);
            this.tbtnSelectProject.Text = "toolStripButton1";
            this.tbtnSelectProject.ToolTipText = "выбрать проект";
            this.tbtnSelectProject.Click += new System.EventHandler(this.tbtnSelectProject_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tbtnAddProject
            // 
            this.tbtnAddProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnAddProject.Image = global::libspec.Properties.Resources.project_plus;
            this.tbtnAddProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnAddProject.Name = "tbtnAddProject";
            this.tbtnAddProject.Size = new System.Drawing.Size(23, 22);
            this.tbtnAddProject.Text = "toolStripButton1";
            this.tbtnAddProject.ToolTipText = "добавить проект";
            this.tbtnAddProject.Click += new System.EventHandler(this.tbtnAddProject_Click);
            // 
            // tbtnAddGroup
            // 
            this.tbtnAddGroup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnAddGroup.Image = global::libspec.Properties.Resources.group_plus;
            this.tbtnAddGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnAddGroup.Name = "tbtnAddGroup";
            this.tbtnAddGroup.Size = new System.Drawing.Size(23, 22);
            this.tbtnAddGroup.Text = "toolStripButton1";
            this.tbtnAddGroup.ToolTipText = "добавить группу";
            this.tbtnAddGroup.Click += new System.EventHandler(this.tbtnAddGroup_Click);
            // 
            // tbtnAddDoc
            // 
            this.tbtnAddDoc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnAddDoc.Image = global::libspec.Properties.Resources.doc_plus;
            this.tbtnAddDoc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnAddDoc.Name = "tbtnAddDoc";
            this.tbtnAddDoc.Size = new System.Drawing.Size(23, 22);
            this.tbtnAddDoc.Text = "toolStripButton2";
            this.tbtnAddDoc.ToolTipText = "добавить документ";
            this.tbtnAddDoc.Click += new System.EventHandler(this.tbtnAddDoc_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tbtnOpen
            // 
            this.tbtnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnOpen.Image = global::libspec.Properties.Resources.add;
            this.tbtnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnOpen.Name = "tbtnOpen";
            this.tbtnOpen.Size = new System.Drawing.Size(23, 22);
            this.tbtnOpen.Text = "toolStripButton1";
            this.tbtnOpen.ToolTipText = "включить в расчет";
            this.tbtnOpen.Click += new System.EventHandler(this.tbtnOpen_Click);
            // 
            // tbtnClose
            // 
            this.tbtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnClose.Image = global::libspec.Properties.Resources.delete;
            this.tbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnClose.Name = "tbtnClose";
            this.tbtnClose.Size = new System.Drawing.Size(23, 22);
            this.tbtnClose.Text = "toolStripButton1";
            this.tbtnClose.ToolTipText = "исключить из расчета";
            this.tbtnClose.Click += new System.EventHandler(this.tbtnClose_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tbtnDelObject
            // 
            this.tbtnDelObject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnDelObject.Image = global::libspec.Properties.Resources.cross;
            this.tbtnDelObject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnDelObject.Name = "tbtnDelObject";
            this.tbtnDelObject.Size = new System.Drawing.Size(23, 22);
            this.tbtnDelObject.Text = "toolStripButton1";
            this.tbtnDelObject.ToolTipText = "удалить";
            this.tbtnDelObject.Click += new System.EventHandler(this.tbtnDelObject_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tbntUpdate
            // 
            this.tbntUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbntUpdate.Image = global::libspec.Properties.Resources.update;
            this.tbntUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbntUpdate.Name = "tbntUpdate";
            this.tbntUpdate.Size = new System.Drawing.Size(23, 22);
            this.tbntUpdate.Text = "toolStripButton1";
            this.tbntUpdate.ToolTipText = "обновить содержимое";
            this.tbntUpdate.Click += new System.EventHandler(this.tbntUpdate_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // tbtnCopy
            // 
            this.tbtnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnCopy.Image = global::libspec.Properties.Resources.copy;
            this.tbtnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnCopy.Name = "tbtnCopy";
            this.tbtnCopy.Size = new System.Drawing.Size(23, 22);
            this.tbtnCopy.Text = "toolStripButton1";
            this.tbtnCopy.ToolTipText = "копировать";
            this.tbtnCopy.Click += new System.EventHandler(this.tbtnCopy_Click);
            // 
            // tbtnCut
            // 
            this.tbtnCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnCut.Image = global::libspec.Properties.Resources.cut;
            this.tbtnCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnCut.Name = "tbtnCut";
            this.tbtnCut.Size = new System.Drawing.Size(23, 22);
            this.tbtnCut.Text = "toolStripButton2";
            this.tbtnCut.ToolTipText = "вырезать";
            this.tbtnCut.Click += new System.EventHandler(this.tbtnCut_Click);
            // 
            // tbtnPaste
            // 
            this.tbtnPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnPaste.Image = global::libspec.Properties.Resources.paste;
            this.tbtnPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnPaste.Name = "tbtnPaste";
            this.tbtnPaste.Size = new System.Drawing.Size(23, 22);
            this.tbtnPaste.Text = "toolStripButton3";
            this.tbtnPaste.ToolTipText = "вставить";
            this.tbtnPaste.Click += new System.EventHandler(this.tbtnPaste_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // tbtnAddPoz
            // 
            this.tbtnAddPoz.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnAddPoz.Image = global::libspec.Properties.Resources.text_list_bullets;
            this.tbtnAddPoz.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnAddPoz.Name = "tbtnAddPoz";
            this.tbtnAddPoz.Size = new System.Drawing.Size(23, 22);
            this.tbtnAddPoz.Text = "toolStripButton1";
            this.tbtnAddPoz.Click += new System.EventHandler(this.tbtnAddPoz_Click);
            // 
            // tbtnCalc
            // 
            this.tbtnCalc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnCalc.Image = global::libspec.Properties.Resources.calc;
            this.tbtnCalc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnCalc.Name = "tbtnCalc";
            this.tbtnCalc.Size = new System.Drawing.Size(23, 22);
            this.tbtnCalc.Text = "toolStripButton1";
            this.tbtnCalc.ToolTipText = "запустить расчет";
            this.tbtnCalc.Click += new System.EventHandler(this.tbtnCalc_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stlblNumChilds,
            this.stlblAction});
            this.statusStrip.Location = new System.Drawing.Point(0, 595);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1380, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // stlblNumChilds
            // 
            this.stlblNumChilds.Name = "stlblNumChilds";
            this.stlblNumChilds.Size = new System.Drawing.Size(1301, 17);
            this.stlblNumChilds.Spring = true;
            this.stlblNumChilds.Text = "stlblNumChilds";
            this.stlblNumChilds.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // stlblAction
            // 
            this.stlblAction.Name = "stlblAction";
            this.stlblAction.Size = new System.Drawing.Size(64, 17);
            this.stlblAction.Text = "stlblAction";
            // 
            // treeView
            // 
            this.treeView.AllowUserToAddRows = false;
            this.treeView.AllowUserToDeleteRows = false;
            this.treeView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.treeView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.obozn,
            this.naimen,
            this.kol,
            this.gost,
            this.marka,
            this.kei,
            this.kfr,
            this.knr,
            this.description});
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.treeView.ImageList = null;
            this.treeView.Location = new System.Drawing.Point(0, 25);
            this.treeView.MultiSelect = false;
            this.treeView.Name = "treeView";
            this.treeView.RowHeadersVisible = false;
            this.treeView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.treeView.Size = new System.Drawing.Size(1380, 570);
            this.treeView.TabIndex = 2;
            this.treeView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.treeView_CellDoubleClick);
            this.treeView.SelectionChanged += new System.EventHandler(this.treeView_SelectionChanged);
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
            // kol
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.kol.DefaultCellStyle = dataGridViewCellStyle6;
            this.kol.HeaderText = "КОЛ";
            this.kol.Name = "kol";
            this.kol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.kol.Width = 60;
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
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.kei.DefaultCellStyle = dataGridViewCellStyle7;
            this.kei.HeaderText = "КЕИ";
            this.kei.Name = "kei";
            this.kei.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.kei.Width = 60;
            // 
            // kfr
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.kfr.DefaultCellStyle = dataGridViewCellStyle8;
            this.kfr.HeaderText = "КФ";
            this.kfr.Name = "kfr";
            this.kfr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.kfr.Width = 60;
            // 
            // knr
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.knr.DefaultCellStyle = dataGridViewCellStyle9;
            this.knr.HeaderText = "НР";
            this.knr.Name = "knr";
            this.knr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.knr.Width = 60;
            // 
            // description
            // 
            this.description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Gray;
            this.description.DefaultCellStyle = dataGridViewCellStyle10;
            this.description.HeaderText = "ИНФОРМАЦИЯ";
            this.description.Name = "description";
            this.description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SpecViewTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Name = "SpecViewTree";
            this.Size = new System.Drawing.Size(1380, 617);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel stlblNumChilds;
        private AdvancedDataGridView.TreeGridView treeView;
        private AdvancedDataGridView.TreeGridColumn obozn;
        private System.Windows.Forms.DataGridViewTextBoxColumn naimen;
        private System.Windows.Forms.DataGridViewTextBoxColumn kol;
        private System.Windows.Forms.DataGridViewTextBoxColumn gost;
        private System.Windows.Forms.DataGridViewTextBoxColumn marka;
        private System.Windows.Forms.DataGridViewTextBoxColumn kei;
        private System.Windows.Forms.DataGridViewTextBoxColumn kfr;
        private System.Windows.Forms.DataGridViewTextBoxColumn knr;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
        private System.Windows.Forms.ToolStripButton tbtnSelectProject;
        private System.Windows.Forms.ToolStripButton tbtnAddProject;
        private System.Windows.Forms.ToolStripButton tbtnAddGroup;
        private System.Windows.Forms.ToolStripButton tbtnAddDoc;
        private System.Windows.Forms.ToolStripButton tbtnDelObject;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tbtnClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tbtnOpen;
        private System.Windows.Forms.ToolStripButton tbntUpdate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tbtnAddPoz;
        private System.Windows.Forms.ToolStripButton tbtnCopy;
        private System.Windows.Forms.ToolStripButton tbtnCut;
        private System.Windows.Forms.ToolStripButton tbtnPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripStatusLabel stlblAction;
        private System.Windows.Forms.ToolStripButton tbtnCalc;
    }
}
