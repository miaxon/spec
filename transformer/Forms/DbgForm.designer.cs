namespace transformer
{
    partial class DbgForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DbgForm));
            this.tf = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tf
            // 
            this.tf.BackColor = System.Drawing.SystemColors.Window;
            this.tf.Location = new System.Drawing.Point(12, 12);
            this.tf.Multiline = true;
            this.tf.Name = "tf";
            this.tf.ReadOnly = true;
            this.tf.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tf.Size = new System.Drawing.Size(474, 383);
            this.tf.TabIndex = 0;
            // 
            // DbgForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 407);
            this.Controls.Add(this.tf);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DbgForm";
            this.ShowInTaskbar = false;
            this.Text = "Dbg Output";
            this.Load += new System.EventHandler(this.DbgForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DbgForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tf;
    }
}