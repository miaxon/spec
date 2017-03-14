namespace libspec.Contrlos
{
    partial class SpecPozVew
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
            this.dgView = new System.Windows.Forms.DataGridView();
            this.c_img = new System.Windows.Forms.DataGridViewImageColumn();
            this.c_poz = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_obozn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_gost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_naimen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_marka = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_num_kei = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_num_kol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_num_kfr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_num_knr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).BeginInit();
            this.SuspendLayout();
            // 
            // dgView
            // 
            this.dgView.AllowUserToAddRows = false;
            this.dgView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.c_img,
            this.c_poz,
            this.c_obozn,
            this.c_gost,
            this.c_naimen,
            this.c_marka,
            this.c_num_kei,
            this.c_num_kol,
            this.c_num_kfr,
            this.c_num_knr});
            this.dgView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgView.Location = new System.Drawing.Point(0, 0);
            this.dgView.Name = "dgView";
            this.dgView.RowHeadersVisible = false;
            this.dgView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgView.ShowEditingIcon = false;
            this.dgView.Size = new System.Drawing.Size(850, 388);
            this.dgView.TabIndex = 1;
            this.dgView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgView_CellContentDoubleClick);
            this.dgView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgView_CellEndEdit);
            // 
            // img
            // 
            this.c_img.Frozen = true;
            this.c_img.HeaderText = "";
            this.c_img.Image = global::libspec.Properties.Resources.empty;
            this.c_img.Name = "img";
            this.c_img.ReadOnly = true;
            this.c_img.Width = 32;
            // 
            // poz
            // 
            this.c_poz.Frozen = true;
            this.c_poz.HeaderText = "№";
            this.c_poz.Name = "poz";
            this.c_poz.ReadOnly = true;
            this.c_poz.Width = 40;
            // 
            // c_obozn
            // 
            this.c_obozn.Frozen = true;
            this.c_obozn.HeaderText = "ОБОЗНАЧЕНИЕ";
            this.c_obozn.Name = "c_obozn";
            this.c_obozn.Width = 120;
            // 
            // c_gost
            // 
            this.c_gost.Frozen = true;
            this.c_gost.HeaderText = "НОРМАТИВ";
            this.c_gost.Name = "c_gost";
            // 
            // c_naimen
            // 
            this.c_naimen.Frozen = true;
            this.c_naimen.HeaderText = "НАИМЕНОВАНИЕ/ТИП-РАЗМЕР";
            this.c_naimen.Name = "c_naimen";
            this.c_naimen.ReadOnly = true;
            this.c_naimen.Width = 300;
            // 
            // c_marka
            // 
            this.c_marka.HeaderText = "МАРКА";
            this.c_marka.Name = "c_marka";
            this.c_marka.ReadOnly = true;
            this.c_marka.Width = 80;
            // 
            // c_num_kei
            // 
            this.c_num_kei.HeaderText = "КЕИ";
            this.c_num_kei.Name = "c_num_kei";
            this.c_num_kei.ReadOnly = true;
            this.c_num_kei.Width = 40;
            // 
            // c_num_kol
            // 
            this.c_num_kol.HeaderText = "КОЛ";
            this.c_num_kol.Name = "c_num_kol";
            this.c_num_kol.Width = 40;
            // 
            // c_num_kfr
            // 
            this.c_num_kfr.HeaderText = "КФ";
            this.c_num_kfr.Name = "c_num_kfr";
            this.c_num_kfr.Width = 40;
            // 
            // c_num_knr
            // 
            this.c_num_knr.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.c_num_knr.HeaderText = "НР";
            this.c_num_knr.Name = "c_num_knr";
            this.c_num_knr.ReadOnly = true;
            // 
            // SpecPozVew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgView);
            this.Name = "SpecPozVew";
            this.Size = new System.Drawing.Size(850, 388);
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgView;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_obozn;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_gost;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_naimen;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_marka;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_num_kei;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_num_kol;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_num_kfr;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_num_knr;
        private System.Windows.Forms.DataGridViewImageColumn c_img;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_poz;
    }
}
