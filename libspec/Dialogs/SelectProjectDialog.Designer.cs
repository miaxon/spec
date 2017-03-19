namespace libspec.Dialogs
{
    partial class SelectProjectDialog
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
            this.treeView = new AdvancedDataGridView.TreeGridView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.obozn = new AdvancedDataGridView.TreeGridColumn();
            this.naimen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.treeView)).BeginInit();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.AllowUserToAddRows = false;
            this.treeView.AllowUserToDeleteRows = false;
            this.treeView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.treeView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.obozn,
            this.naimen,
            this.descr});
            this.treeView.Dock = System.Windows.Forms.DockStyle.Top;
            this.treeView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.treeView.ImageList = null;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.RowHeadersVisible = false;
            this.treeView.Size = new System.Drawing.Size(889, 291);
            this.treeView.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(802, 297);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "button1";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(721, 297);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "button2";
            this.btnOk.UseVisualStyleBackColor = true;
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
            this.naimen.Width = 400;
            // 
            // descr
            // 
            this.descr.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.descr.HeaderText = "ИНФОРМАЦИЯ";
            this.descr.Name = "descr";
            this.descr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SelectProjectDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 326);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.treeView);
            this.Name = "SelectProjectDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выбор проекта";
            ((System.ComponentModel.ISupportInitialize)(this.treeView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AdvancedDataGridView.TreeGridView treeView;
        private AdvancedDataGridView.TreeGridColumn obozn;
        private System.Windows.Forms.DataGridViewTextBoxColumn naimen;
        private System.Windows.Forms.DataGridViewTextBoxColumn descr;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
    }
}