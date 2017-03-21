namespace transformer
{
    partial class Dlg
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dlg));
            this.lblProject = new System.Windows.Forms.Label();
            this.checkMat = new System.Windows.Forms.CheckBox();
            this.checkDoc = new System.Windows.Forms.CheckBox();
            this.checkList = new System.Windows.Forms.CheckBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkViewMSCH = new System.Windows.Forms.CheckBox();
            this.checkMSCH = new System.Windows.Forms.CheckBox();
            this.checkViewList = new System.Windows.Forms.CheckBox();
            this.checkViewDoc = new System.Windows.Forms.CheckBox();
            this.checkViewMat = new System.Windows.Forms.CheckBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblProgress = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.lblGroup = new System.Windows.Forms.Label();
            this.lblDoc = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.listView = new System.Windows.Forms.ListView();
            this.obozn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.naimen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblProject
            // 
            this.lblProject.AutoSize = true;
            this.lblProject.Location = new System.Drawing.Point(12, 9);
            this.lblProject.Name = "lblProject";
            this.lblProject.Size = new System.Drawing.Size(47, 13);
            this.lblProject.TabIndex = 0;
            this.lblProject.Text = "Проект ";
            // 
            // checkMat
            // 
            this.checkMat.AutoSize = true;
            this.checkMat.Location = new System.Drawing.Point(6, 19);
            this.checkMat.Name = "checkMat";
            this.checkMat.Size = new System.Drawing.Size(171, 17);
            this.checkMat.TabIndex = 2;
            this.checkMat.Text = "применяемость материалов";
            this.checkMat.UseVisualStyleBackColor = true;
            this.checkMat.CheckedChanged += new System.EventHandler(this.checkMat_CheckedChanged);
            // 
            // checkDoc
            // 
            this.checkDoc.AutoSize = true;
            this.checkDoc.Location = new System.Drawing.Point(6, 42);
            this.checkDoc.Name = "checkDoc";
            this.checkDoc.Size = new System.Drawing.Size(158, 17);
            this.checkDoc.TabIndex = 3;
            this.checkDoc.Text = "применяемость чертежей";
            this.checkDoc.UseVisualStyleBackColor = true;
            this.checkDoc.CheckedChanged += new System.EventHandler(this.checkDoc_CheckedChanged);
            // 
            // checkList
            // 
            this.checkList.AutoSize = true;
            this.checkList.Location = new System.Drawing.Point(6, 65);
            this.checkList.Name = "checkList";
            this.checkList.Size = new System.Drawing.Size(145, 17);
            this.checkList.TabIndex = 4;
            this.checkList.Text = "ведомость материалов";
            this.checkList.UseVisualStyleBackColor = true;
            this.checkList.CheckedChanged += new System.EventHandler(this.checkList_CheckedChanged);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(9, 572);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(467, 23);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkViewMSCH);
            this.groupBox1.Controls.Add(this.checkMSCH);
            this.groupBox1.Controls.Add(this.checkViewList);
            this.groupBox1.Controls.Add(this.checkViewDoc);
            this.groupBox1.Controls.Add(this.checkViewMat);
            this.groupBox1.Controls.Add(this.checkMat);
            this.groupBox1.Controls.Add(this.checkDoc);
            this.groupBox1.Controls.Add(this.checkList);
            this.groupBox1.Location = new System.Drawing.Point(9, 435);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(467, 117);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Экспорт";
            // 
            // checkViewMSCH
            // 
            this.checkViewMSCH.AutoSize = true;
            this.checkViewMSCH.Location = new System.Drawing.Point(219, 88);
            this.checkViewMSCH.Name = "checkViewMSCH";
            this.checkViewMSCH.Size = new System.Drawing.Size(68, 17);
            this.checkViewMSCH.TabIndex = 12;
            this.checkViewMSCH.Text = "открыть";
            this.checkViewMSCH.UseVisualStyleBackColor = true;
            // 
            // checkMSCH
            // 
            this.checkMSCH.AutoSize = true;
            this.checkMSCH.Location = new System.Drawing.Point(6, 88);
            this.checkMSCH.Name = "checkMSCH";
            this.checkMSCH.Size = new System.Drawing.Size(108, 17);
            this.checkMSCH.TabIndex = 11;
            this.checkMSCH.Text = "ведомость МСЧ";
            this.checkMSCH.UseVisualStyleBackColor = true;
            this.checkMSCH.CheckedChanged += new System.EventHandler(this.checkMSCH_CheckedChanged);
            // 
            // checkViewList
            // 
            this.checkViewList.AutoSize = true;
            this.checkViewList.Location = new System.Drawing.Point(219, 65);
            this.checkViewList.Name = "checkViewList";
            this.checkViewList.Size = new System.Drawing.Size(68, 17);
            this.checkViewList.TabIndex = 10;
            this.checkViewList.Text = "открыть";
            this.checkViewList.UseVisualStyleBackColor = true;
            // 
            // checkViewDoc
            // 
            this.checkViewDoc.AutoSize = true;
            this.checkViewDoc.Location = new System.Drawing.Point(219, 42);
            this.checkViewDoc.Name = "checkViewDoc";
            this.checkViewDoc.Size = new System.Drawing.Size(68, 17);
            this.checkViewDoc.TabIndex = 9;
            this.checkViewDoc.Text = "открыть";
            this.checkViewDoc.UseVisualStyleBackColor = true;
            // 
            // checkViewMat
            // 
            this.checkViewMat.AutoSize = true;
            this.checkViewMat.Location = new System.Drawing.Point(219, 19);
            this.checkViewMat.Name = "checkViewMat";
            this.checkViewMat.Size = new System.Drawing.Size(68, 17);
            this.checkViewMat.TabIndex = 8;
            this.checkViewMat.Text = "открыть";
            this.checkViewMat.UseVisualStyleBackColor = true;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(6, 601);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(74, 23);
            this.btnOpen.TabIndex = 7;
            this.btnOpen.Text = "Открыть";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(317, 601);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 10;
            this.btnOk.Text = "Расчет";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnClose
            // 
            this.btnClose.AutoSize = true;
            this.btnClose.Location = new System.Drawing.Point(401, 601);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "Выход";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(6, 556);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(59, 13);
            this.lblProgress.TabIndex = 12;
            this.lblProgress.Text = "Прогресс:";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.Location = new System.Drawing.Point(12, 31);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(48, 13);
            this.lblGroup.TabIndex = 13;
            this.lblGroup.Text = "Группа: ";
            // 
            // lblDoc
            // 
            this.lblDoc.AutoSize = true;
            this.lblDoc.Location = new System.Drawing.Point(12, 54);
            this.lblDoc.Name = "lblDoc";
            this.lblDoc.Size = new System.Drawing.Size(64, 13);
            this.lblDoc.TabIndex = 14;
            this.lblDoc.Text = "Документ: ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkAll);
            this.groupBox2.Controls.Add(this.listView);
            this.groupBox2.Location = new System.Drawing.Point(9, 81);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(464, 348);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Настройки";
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(6, 19);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(97, 17);
            this.chkAll.TabIndex = 1;
            this.chkAll.Text = "Выделить все";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // listView
            // 
            this.listView.CheckBoxes = true;
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.obozn,
            this.naimen});
            this.listView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listView.Location = new System.Drawing.Point(3, 42);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(458, 303);
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listView_ItemChecked);
            // 
            // obozn
            // 
            this.obozn.Text = "Обозначение";
            this.obozn.Width = 176;
            // 
            // naimen
            // 
            this.naimen.Text = "Наименование";
            this.naimen.Width = 164;
            // 
            // Dlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 631);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblDoc);
            this.Controls.Add(this.lblGroup);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblProject);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Dlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Spec";
            this.Load += new System.EventHandler(this.Dlg_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProject;
        private System.Windows.Forms.CheckBox checkMat;
        private System.Windows.Forms.CheckBox checkDoc;
        private System.Windows.Forms.CheckBox checkList;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.CheckBox checkViewList;
        private System.Windows.Forms.CheckBox checkViewDoc;
        private System.Windows.Forms.CheckBox checkViewMat;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.Label lblDoc;
        private System.Windows.Forms.CheckBox checkMSCH;
        private System.Windows.Forms.CheckBox checkViewMSCH;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader obozn;
        private System.Windows.Forms.ColumnHeader naimen;
        private System.Windows.Forms.CheckBox chkAll;
    }
}

