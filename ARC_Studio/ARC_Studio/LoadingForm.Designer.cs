namespace ARC_Studio
{
    partial class LoadingForm
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
            this.loadingLabel = new System.Windows.Forms.Label();
            this.flatProgressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // loadingLabel
            // 
            this.loadingLabel.AutoSize = true;
            this.loadingLabel.Location = new System.Drawing.Point(12, 37);
            this.loadingLabel.Name = "loadingLabel";
            this.loadingLabel.Size = new System.Drawing.Size(72, 13);
            this.loadingLabel.TabIndex = 1;
            this.loadingLabel.Text = "Opening file...";
            this.loadingLabel.Click += new System.EventHandler(this.loadingLabel_Click);
            // 
            // flatProgressBar1
            // 
            this.flatProgressBar1.Location = new System.Drawing.Point(17, 62);
            this.flatProgressBar1.MarqueeAnimationSpeed = 25;
            this.flatProgressBar1.Name = "flatProgressBar1";
            this.flatProgressBar1.Size = new System.Drawing.Size(427, 14);
            this.flatProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.flatProgressBar1.TabIndex = 2;
            // 
            // LoadingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(456, 98);
            this.ControlBox = false;
            this.Controls.Add(this.flatProgressBar1);
            this.Controls.Add(this.loadingLabel);
            this.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoadingForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ARC Studio";
            this.Load += new System.EventHandler(this.LoadingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label loadingLabel;
        private System.Windows.Forms.ProgressBar flatProgressBar1;
    }
}