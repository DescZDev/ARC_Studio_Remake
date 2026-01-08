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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadingForm));
            this.loadingLabel = new System.Windows.Forms.Label();
            this.flatProgressBar1 = new System.Windows.Forms.ProgressBar();
            this.arcPicture = new ARC_Studio.PictureBoxWithInterpolationMode();
            ((System.ComponentModel.ISupportInitialize)(this.arcPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // loadingLabel
            // 
            this.loadingLabel.AutoSize = true;
            this.loadingLabel.ForeColor = System.Drawing.SystemColors.Info;
            this.loadingLabel.Location = new System.Drawing.Point(14, 41);
            this.loadingLabel.Name = "loadingLabel";
            this.loadingLabel.Size = new System.Drawing.Size(90, 13);
            this.loadingLabel.TabIndex = 1;
            this.loadingLabel.Text = "Opening the file...";
            this.loadingLabel.Click += new System.EventHandler(this.loadingLabel_Click);
            // 
            // flatProgressBar1
            // 
            this.flatProgressBar1.Location = new System.Drawing.Point(17, 66);
            this.flatProgressBar1.MarqueeAnimationSpeed = 25;
            this.flatProgressBar1.Name = "flatProgressBar1";
            this.flatProgressBar1.Size = new System.Drawing.Size(427, 10);
            this.flatProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.flatProgressBar1.TabIndex = 2;
            // 
            // arcPicture
            // 
            this.arcPicture.BackColor = System.Drawing.Color.Transparent;
            this.arcPicture.DrawFrame = false;
            this.arcPicture.FrameColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(160)))), ((int)(((byte)(30)))));
            this.arcPicture.FrameThickness = 4;
            this.arcPicture.Image = ((System.Drawing.Image)(resources.GetObject("arcPicture.Image")));
            this.arcPicture.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            this.arcPicture.KeepAspectRatio = true;
            this.arcPicture.Location = new System.Drawing.Point(171, 12);
            this.arcPicture.Name = "arcPicture";
            this.arcPicture.Size = new System.Drawing.Size(113, 17);
            this.arcPicture.TabIndex = 11;
            this.arcPicture.TabStop = false;
            // 
            // LoadingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(461, 94);
            this.ControlBox = false;
            this.Controls.Add(this.arcPicture);
            this.Controls.Add(this.flatProgressBar1);
            this.Controls.Add(this.loadingLabel);
            this.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoadingForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.LoadingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.arcPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label loadingLabel;
        private System.Windows.Forms.ProgressBar flatProgressBar1;
        private PictureBoxWithInterpolationMode arcPicture;
    }
}