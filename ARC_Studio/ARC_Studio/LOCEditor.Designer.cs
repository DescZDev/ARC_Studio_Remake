namespace ARC_Studio.ARC
{
    partial class LOCEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LOCEditor));
            this.duohnRabql = new System.Windows.Forms.ListView();
            this.columnHeader_0 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvMessages = new System.Windows.Forms.ListView();
            this.columnHeader_1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // duohnRabql
            // 
            this.duohnRabql.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.duohnRabql.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.duohnRabql.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.duohnRabql.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_0});
            this.duohnRabql.Dock = System.Windows.Forms.DockStyle.Left;
            this.duohnRabql.ForeColor = System.Drawing.Color.White;
            this.duohnRabql.FullRowSelect = true;
            this.duohnRabql.HideSelection = false;
            this.duohnRabql.Location = new System.Drawing.Point(0, 24);
            this.duohnRabql.Name = "duohnRabql";
            this.duohnRabql.Size = new System.Drawing.Size(207, 637);
            this.duohnRabql.TabIndex = 4;
            this.duohnRabql.UseCompatibleStateImageBehavior = false;
            this.duohnRabql.View = System.Windows.Forms.View.Details;
            this.duohnRabql.SelectedIndexChanged += new System.EventHandler(this.duohnRabql_SelectedIndexChanged);
            // 
            // columnHeader_0
            // 
            this.columnHeader_0.Text = "Languages";
            this.columnHeader_0.Width = 105;
            // 
            // lvMessages
            // 
            this.lvMessages.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.lvMessages.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lvMessages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvMessages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_1,
            this.columnHeader_2});
            this.lvMessages.Dock = System.Windows.Forms.DockStyle.Top;
            this.lvMessages.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lvMessages.ForeColor = System.Drawing.Color.White;
            this.lvMessages.FullRowSelect = true;
            this.lvMessages.HideSelection = false;
            this.lvMessages.Location = new System.Drawing.Point(0, 0);
            this.lvMessages.Name = "lvMessages";
            this.lvMessages.Size = new System.Drawing.Size(677, 318);
            this.lvMessages.TabIndex = 5;
            this.lvMessages.UseCompatibleStateImageBehavior = false;
            this.lvMessages.View = System.Windows.Forms.View.Details;
            this.lvMessages.SelectedIndexChanged += new System.EventHandler(this.lvMessages_SelectedIndexChanged);
            // 
            // columnHeader_1
            // 
            this.columnHeader_1.DisplayIndex = 1;
            this.columnHeader_1.Text = "#";
            this.columnHeader_1.Width = 582;
            // 
            // columnHeader_2
            // 
            this.columnHeader_2.DisplayIndex = 0;
            this.columnHeader_2.Text = "";
            this.columnHeader_2.Width = 582;
            // 
            // tbMessage
            // 
            this.tbMessage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tbMessage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.tbMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.tbMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbMessage.ForeColor = System.Drawing.Color.White;
            this.tbMessage.Location = new System.Drawing.Point(0, 324);
            this.tbMessage.Multiline = true;
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbMessage.Size = new System.Drawing.Size(677, 313);
            this.tbMessage.TabIndex = 6;
            this.tbMessage.TextChanged += new System.EventHandler(this.tbMessage_TextChanged_1);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(884, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sameToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // sameToolStripMenuItem
            // 
            this.sameToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("sameToolStripMenuItem.Image")));
            this.sameToolStripMenuItem.Name = "sameToolStripMenuItem";
            this.sameToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + Z";
            this.sameToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sameToolStripMenuItem.Text = "Save";
            this.sameToolStripMenuItem.Click += new System.EventHandler(this.sameToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.lvMessages);
            this.panel1.Controls.Add(this.tbMessage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(207, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(677, 637);
            this.panel1.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 318);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(677, 10);
            this.panel2.TabIndex = 10;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(0, 451);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(207, 210);
            this.richTextBox1.TabIndex = 11;
            this.richTextBox1.Text = "  *All strings are provided by DescZ*\n\n{*B*} - Makes a new line";
            this.richTextBox1.Visible = false;
            // 
            // LOCEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(884, 661);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.duohnRabql);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.MaximizeBox = false;
            this.Name = "LOCEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LOC Editor";
            this.Load += new System.EventHandler(this.LOCEditor_Load);
            this.Resize += new System.EventHandler(this.LOCEditor_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Lang.models.LanguagesContainer languagesContainer_0;

        private Lang.models.MessageEntry messageEntry_0;

        private System.Windows.Forms.ListView duohnRabql;
        private System.Windows.Forms.ColumnHeader columnHeader_0;
        private System.Windows.Forms.ListView lvMessages;
        private System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sameToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ColumnHeader columnHeader_1;
        private System.Windows.Forms.ColumnHeader columnHeader_2;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}