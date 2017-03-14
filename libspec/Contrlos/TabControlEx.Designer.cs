namespace libspec.Contrlos
{
    partial class TabControlEx
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TabControlEx));
            this.m_cms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_closeItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_cms.SuspendLayout();
            // 
            // m_cms
            // 
            this.m_cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_closeItem});
            this.m_cms.Name = "contextMenuStrip1";
            this.m_cms.Size = new System.Drawing.Size(121, 26);
            this.m_cms.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.m_cms_ItemClicked);
            // 
            // m_closeItem
            // 
            this.m_closeItem.Image = ((System.Drawing.Image)(resources.GetObject("m_closeItem.Image")));
            this.m_closeItem.Name = "m_closeItem";
            this.m_closeItem.Size = new System.Drawing.Size(120, 22);
            this.m_closeItem.Text = "Закрыть";
            this.m_cms.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip m_cms;
        private System.Windows.Forms.ToolStripMenuItem m_closeItem;
    }
}
