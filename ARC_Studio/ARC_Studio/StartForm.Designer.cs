namespace ARC_Studio
{
    partial class StartForm
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
            this.CenterPanel = new System.Windows.Forms.Panel();
            this.RecentList = new System.Windows.Forms.ListBox();
            this.OpenFileBtn = new System.Windows.Forms.Button();
            this.CenterPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // CenterPanel
            // 
            this.CenterPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CenterPanel.Controls.Add(this.RecentList);
            this.CenterPanel.Controls.Add(this.OpenFileBtn);
            this.CenterPanel.Location = new System.Drawing.Point(3, 3);
            this.CenterPanel.Margin = new System.Windows.Forms.Padding(2);
            this.CenterPanel.Name = "CenterPanel";
            this.CenterPanel.Size = new System.Drawing.Size(657, 347);
            this.CenterPanel.TabIndex = 0;
            // 
            // RecentList
            // 
            this.RecentList.Dock = System.Windows.Forms.DockStyle.Left;
            this.RecentList.FormattingEnabled = true;
            this.RecentList.Location = new System.Drawing.Point(0, 0);
            this.RecentList.Margin = new System.Windows.Forms.Padding(2);
            this.RecentList.Name = "RecentList";
            this.RecentList.Size = new System.Drawing.Size(284, 347);
            this.RecentList.TabIndex = 0;
            this.RecentList.DoubleClick += new System.EventHandler(this.RecentList_DoubleClick);
            // 
            // OpenFileBtn
            // 
            this.OpenFileBtn.Location = new System.Drawing.Point(590, 314);
            this.OpenFileBtn.Margin = new System.Windows.Forms.Padding(2);
            this.OpenFileBtn.Name = "OpenFileBtn";
            this.OpenFileBtn.Size = new System.Drawing.Size(56, 23);
            this.OpenFileBtn.TabIndex = 1;
            this.OpenFileBtn.Text = "Open";
            this.OpenFileBtn.UseVisualStyleBackColor = true;
            this.OpenFileBtn.Click += new System.EventHandler(this.OpenFileBtn_Click);
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 351);
            this.Controls.Add(this.CenterPanel);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "StartForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ARC Studio ";
            this.Load += new System.EventHandler(this.StartForm_Load);
            this.CenterPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel CenterPanel;
        private System.Windows.Forms.ListBox RecentList;
        private System.Windows.Forms.Button OpenFileBtn;
    }
}