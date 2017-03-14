namespace libspec.Contrlos
{
    partial class SpecTabPage
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
            this.pozVew = new libspec.Contrlos.SpecPozVew();
            this.SuspendLayout();
            // 
            // pozVew
            // 
            this.pozVew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pozVew.Location = new System.Drawing.Point(0, 0);
            this.pozVew.Name = "pozVew";
            this.pozVew.Size = new System.Drawing.Size(850, 388);
            this.pozVew.TabIndex = 0;
            this.ResumeLayout(false);

        }

        #endregion

        private SpecPozVew pozVew;
    }
}
