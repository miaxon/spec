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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpecViewTree));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tbtnAddProject = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.stlblNumChilds = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.tbtnAddGroup = new System.Windows.Forms.ToolStripButton();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeView)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbtnAddProject,
            this.tbtnAddGroup});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1380, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // tbtnAddProject
            // 
            this.tbtnAddProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnAddProject.Image = ((System.Drawing.Image)(resources.GetObject("tbtnAddProject.Image")));
            this.tbtnAddProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnAddProject.Name = "tbtnAddProject";
            this.tbtnAddProject.Size = new System.Drawing.Size(23, 22);
            this.tbtnAddProject.Text = "toolStripButton1";
            this.tbtnAddProject.ToolTipText = "добавить проект";
            this.tbtnAddProject.Click += new System.EventHandler(this.tbtnAddProject_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stlblNumChilds});
            this.statusStrip.Location = new System.Drawing.Point(0, 595);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1380, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // stlblNumChilds
            // 
            this.stlblNumChilds.Name = "stlblNumChilds";
            this.stlblNumChilds.Size = new System.Drawing.Size(89, 17);
            this.stlblNumChilds.Text = "stlblNumChilds";
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
            this.treeView.Name = "treeView";
            this.treeView.RowHeadersVisible = false;
            this.treeView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.treeView.Size = new System.Drawing.Size(1380, 570);
            this.treeView.TabIndex = 2;
            this.treeView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tgView_CellDoubleClick);
            this.treeView.SelectionChanged += new System.EventHandler(this.treeView_SelectionChanged);
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.kol.DefaultCellStyle = dataGridViewCellStyle1;
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
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.kei.DefaultCellStyle = dataGridViewCellStyle2;
            this.kei.HeaderText = "КЕИ";
            this.kei.Name = "kei";
            this.kei.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.kei.Width = 60;
            // 
            // kfr
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.kfr.DefaultCellStyle = dataGridViewCellStyle3;
            this.kfr.HeaderText = "КФ";
            this.kfr.Name = "kfr";
            this.kfr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.kfr.Width = 60;
            // 
            // knr
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.knr.DefaultCellStyle = dataGridViewCellStyle4;
            this.knr.HeaderText = "НР";
            this.knr.Name = "knr";
            this.knr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.knr.Width = 60;
            // 
            // description
            // 
            this.description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Gray;
            this.description.DefaultCellStyle = dataGridViewCellStyle5;
            this.description.HeaderText = "ИНФОРМАЦИЯ";
            this.description.Name = "description";
            this.description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tbtnAddGroup
            // 
            this.tbtnAddGroup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnAddGroup.Image = ((System.Drawing.Image)(resources.GetObject("tbtnAddGroup.Image")));
            this.tbtnAddGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnAddGroup.Name = "tbtnAddGroup";
            this.tbtnAddGroup.Size = new System.Drawing.Size(23, 22);
            this.tbtnAddGroup.Text = "toolStripButton1";
            this.tbtnAddGroup.ToolTipText = "добавить группу";
            this.tbtnAddGroup.Click += new System.EventHandler(this.tbtnAddGroup_Click);
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
        private System.Windows.Forms.ToolStripButton tbtnAddProject;
        private System.Windows.Forms.ToolStripButton tbtnAddGroup;
    }
}
