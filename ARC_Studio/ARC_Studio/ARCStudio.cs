using DarkModeForms;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARC_Studio
{
    /* Most of the methods here are by DescZ, please make sure to credit me (Minecraft PS3) */

    public partial class ARCStudio : Form
    {
        public ARCStudio()
        {
            InitializeComponent();

            #region DarkMode
            dm = new DarkModeCS(this)
            {
                ColorMode = DarkModeCS.DisplayMode.DarkMode
            };
            #endregion
        }

        #region RPC 

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            RPC.SetArcEditorPresence();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            RPC.SetArcEditorPresence(arcfile);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            RPC.ClearPresence();
        }
        #endregion

        #region Shortcut keys
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case (Keys.Control | Keys.Z):
                    openToolStripMenuItem_Click(null, null);
                    return true;
                case (Keys.Alt | Keys.S):
                    closeFileToolStripMenuItem_Click(null, null);
                    return true;
                case (Keys.Control | Keys.S):
                    saveToolStripMenuItem1_Click(null, null);
                    return true;
                case (Keys.Alt | Keys.X):
                    closeFileToolStripMenuItem_Click(null, null);
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }
        #endregion

        #region Variables

        private DarkModeCS dm = null;
        private Color borderColor = Color.Blue;
        private Image loadedImage;
        public string arcfile = "";
        string currentFilePath;
        private bool closingInProgress = false;
        public string appdata = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ARC_Data", "Media");

        bool IsPortable = false;

        int fileCount = 0; // Variable for number of minefiles

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Messenger.MessageBox("ARC Studio Remake v2.0\n\nDeveloper/Designer: DescZ\nHelper/Designer: EternalModz\nARC Source code: PhoenixARC\nFUI Source code: kzpns", "Credits", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        #endregion

        #region Saving and opening and closing

        public async void StartOneClick()
        {
            try
            {
                using (var ofd = new OpenFileDialog())
                {
                    ofd.CheckFileExists = true;
                    ofd.RestoreDirectory = true;
                    ofd.Filter = "ARC (Minecraft Console Archive)|*.arc";

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        //await OpenArcFromPath(ofd.FileName);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("The ARC you're trying to use currently isn't supported");
            }
        }

        public async void OpenArcFromPath(string arcPath)
        {
            if (string.IsNullOrEmpty(arcPath) || !File.Exists(arcPath))
            {
                MessageBox.Show("ARC File not found", "Error");
                return;
            }

            try
            {
                using (var loading = new LoadingForm("Opening the file..."))
                {
                    loading.Show();
                    loading.Refresh();
                    arcPicture.Hide();

                    await Task.Run(() =>
                    {
                        try
                        {

                            this.Invoke(new Action(() =>
                            {
                                EntryList.Nodes.Clear();
                                ARC_Studio.Workers.ARC.PS3ARCWorker ps3ARCWorker = new ARC_Studio.Workers.ARC.PS3ARCWorker();
                                ps3ARCWorker.ExtractArchive(arcPath, appdata);
                                openPck(arcPath);
                                UpdateFileMenuState(true);
                            }));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString(), "Error");
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async void OpenFile(string file)
        {
            StartForm sf = new StartForm();
            //sf.Rl(arcfile);

            try
            {
                await Task.Run(() =>
                {
                    if (File.Exists(arcfile))
                    {
                        //arc.Show();
                        ARC_Studio.Workers.ARC.PS3ARCWorker ps3ARCWorker = new ARC_Studio.Workers.ARC.PS3ARCWorker();
                        ps3ARCWorker.ExtractArchive(arcfile, appdata);

                        //arcfile = file;
                        //RPC.SetArcEditorPresence(arcfile);

                        // Update UI on UI thread
                        this.Invoke(new Action(() =>
                        {
                            EntryList.Nodes.Clear();
                            openPck(arcfile);
                            UpdateFileMenuState(true);
                        }));
                        //this.Hide();
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            MessageBox.Show("Check Data", "Data Error");
                        }));
                    }
                });
            }
            catch (Exception err)
            {
                this.Invoke(new Action(() =>
                {
                    MessageBox.Show("error\n" + err.ToString());
                }));
            }
        }

        public async void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (var ofd = new OpenFileDialog())
                {
                    ofd.CheckFileExists = true;
                    ofd.RestoreDirectory = true;
                    ofd.Filter = "ARC (Minecraft Console Archive)|*.arc";

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        RPC.SetLoadingFormPresence();
                        using (var loading = new LoadingForm("Opening the file..."))
                        {
                            arcPicture.Hide();
                            loading.Show();
                            loading.Refresh();

                            try
                            {
                                await Task.Run(() =>
                                {
                                    if (ofd.FileName.EndsWith(".arc"))
                                    {

                                        ARC_Studio.Workers.ARC.PS3ARCWorker ps3ARCWorker = new ARC_Studio.Workers.ARC.PS3ARCWorker();
                                        ps3ARCWorker.ExtractArchive(ofd.FileName, appdata);

                                        arcfile = ofd.FileName;
                                        SetStatus(Path.GetFileName(arcfile));
                                        RPC.SetArcEditorPresence(arcfile);

                                        // Update UI on UI thread
                                        this.Invoke(new Action(() =>
                                        {
                                            EntryList.Nodes.Clear();
                                            openPck(ofd.FileName);
                                            UpdateFileMenuState(true);
                                        }));
                                    }
                                    else
                                    {
                                        this.Invoke(new Action(() =>
                                        {
                                            MessageBox.Show("Check Data", "Data Error");
                                        }));
                                    }
                                });
                            }
                            catch (Exception err)
                            {
                                this.Invoke(new Action(() =>
                                {
                                    MessageBox.Show("error\n" + err.ToString());
                                }));
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("The ARC you're trying to use currently isn't supported");
            }
        }

        private async void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DisposeImages();

            using (var loading = new LoadingForm("Saving the file..."))
            {
                try
                {
                    loading.Show();
                    loading.Refresh();
                    ARC_Studio.Workers.ARC.PS3ARCWorker ps3ARCWorker = new ARC_Studio.Workers.ARC.PS3ARCWorker();
                    ps3ARCWorker.BuildArchive(arcfile, appdata);
                    loading.Hide();
                    Messenger.MessageBox("Saved Successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception err)
                {
                    loading.Hide();
                    MessageBox.Show("Error: " + err.Message);
                    statusStrip1.Visible = true;
                    SizeLabel2.Text = "Save failed!";
                }
                await Task.Delay(100);
            }
        }

        private async void closeFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var loading = new LoadingForm("Closing the file..."))
            {
                CloseCurrentFile();
                loading.Show();
                loading.Hide();

                await Task.Delay(500);
            }

        }

        private void CloseCurrentFile()
        {
            try
            {
                // Clear the current file path
                currentFilePath = string.Empty;
                arcfile = string.Empty;

                // Clear the TreeView
                EntryList.Nodes.Clear();

                // Clear the display areas
                richTextBox1.Text = string.Empty;
                richTextBox1.Hide();
                statusStrip1.Hide();
                viewOnly1.Hide();
                arcPicture.Show();
                this.Text = "ARC Studio";

                if (pictureBoxWithInterpolationMode2 != null)
                {
                    pictureBoxWithInterpolationMode2.Image = null;
                    pictureBoxWithInterpolationMode2.Invalidate();
                }

                tabControl1.Hide();

                // Clear extracted files directory
                ClearExtractedFiles();

                // Update RPC presence
                RPC.SetArcEditorPresence();

                // Update UI state
                UpdateFileMenuState(false);

                Messenger.MessageBox("File closed successfully.", "Close File", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error closing file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearExtractedFiles()
        {
            try
            {
                if (Directory.Exists(appdata))
                {
                    DirectoryInfo di = new DirectoryInfo(appdata);

                    foreach (FileInfo file in di.GetFiles())
                    {
                        try { file.Delete(); } catch { }
                    }
                    foreach (DirectoryInfo dir in di.GetDirectories())
                    {
                        try { dir.Delete(true); } catch { }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: Could not clear extracted files: {ex.Message}");
            }
        }

        public void UpdateFileMenuState(bool fileOpen)
        {
            // Enable/disable menu items based on whether a file is open

            saveToolStripMenuItem1.Enabled = fileOpen;
            closeFileToolStripMenuItem.Enabled = fileOpen;
            SaveAsBtn.Enabled = fileOpen;

        }

        private void SaveFile(string file)
        {
            try
            {
                ARC_Studio.Workers.ARC.PS3ARCWorker ps3ARCWorker = new ARC_Studio.Workers.ARC.PS3ARCWorker();
                ps3ARCWorker.BuildArchive(file, appdata);
                Messenger.MessageBox("Saved Successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception err)
            {
                MessageBox.Show("Error: " + err.Message);
            }
        }

        #endregion

        #region Loading form

        private async void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.Hide();
            tabControl1.Hide();
            UpdateFileMenuState(false);

            string Dir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\PhoenixApplications\\ARCStudio";
            try // Extract Files
            {
                Directory.CreateDirectory(Dir + "\\FUIEditor");
                if (!File.Exists(Dir + "\\settings.ini"))
                    File.WriteAllText(Dir + "\\settings.ini", "**Settings** \nyou can change a variable here!\n**true / false does not accept capitals, 'True' and 'TRUE' do not work, ony 'true'\nIsPortable=" + IsPortable.ToString().Replace("T", "t").Replace("F", "f"));
            }
            catch { }
            try // Checks if portable flag is checked in settings
            {
                string Data = File.ReadAllText(Dir + "\\settings.ini");
                string[] Lines = Data.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                foreach (string Line in Lines)
                {
                    try
                    {
                        if (!Line.Contains("=")) continue;
                        string Param = Line.Split('=')[0];
                        string Value = Line.Split('=')[1];
                        Console.WriteLine(Param + "=" + Value);
                        switch (Param)
                        {
                            case ("IsPortable"):
                                IsPortable = (Value == "true");
                                break;

                        }
                    }
                    catch { }

                }
            }
            catch { }
            try // Determine Location based on portable status
            {
                if (!IsPortable)
                    appdata = Dir + "\\ARC_Data\\Media\\";
                else
                    appdata = Environment.CurrentDirectory + "\\ARC_Data\\Media\\";
            }
            catch
            {

            }
            try // Create Folders
            {
                if (!Directory.Exists(appdata))
                    Directory.CreateDirectory(appdata);
            }
            catch { }
            await Task.Delay(1000);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (var pen = new Pen(borderColor, 3)) 
            e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, Width - 3, Height - 3));
        }

        private void SetStatus(string message)
        {
            if (message != null)
            {
                Text = message + " - ARC Studio Remastered";
            }
            else
            {
                Text = "ARC Studio Remastered";
            }
        }

        #endregion

        #region Delete files when program close

        private async void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (closingInProgress) return;

            e.Cancel = true;
            closingInProgress = true;

            using (var loading = new LoadingForm("Cleaning up"))
            {
                loading.Show(this);
                await Task.Delay(5000);

                try
                {
                    if (Directory.Exists(appdata))
                    {
                        Directory.Delete(appdata, true);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Cleanup warning: {ex.Message}");
                }
                closingInProgress = false;
                loading.Hide();
                Application.ExitThread();
            }
        }

        #endregion

        #region Load ARC

        public async void openPck(string filePath)
        {
            // Build image list
            ImageList icons = new ImageList();
            icons.ColorDepth = ColorDepth.Depth32Bit;
            icons.ImageSize = new Size(20, 20);

            icons.Images.Add(ARC_Studio.Properties.Resources.ZZFolder);   // 0
            icons.Images.Add(ARC_Studio.Properties.Resources.IMAGE_ICON); // 1
            icons.Images.Add(ARC_Studio.Properties.Resources.LOC_ICON);   // 2
            icons.Images.Add(ARC_Studio.Properties.Resources.PCK_ICON);   // 3
            icons.Images.Add(ARC_Studio.Properties.Resources.FUI_ICON);   // 4
            icons.Images.Add(ARC_Studio.Properties.Resources.NBT_ICON);   // 5
            icons.Images.Add(ARC_Studio.Properties.Resources.TXT_ICON);   // 6
            icons.Images.Add(ARC_Studio.Properties.Resources.ZUnknown);   // 7

            EntryList.ImageList = icons; // Sets files icon image list

            // If extracted folder doesn't exist, return
            if (!Directory.Exists(appdata))
            {
                MessageBox.Show("No extracted files found. Extraction may have failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            EntryList.BeginUpdate();
            EntryList.Nodes.Clear();

            // Add directories (top-level) and their content recursively
            try
            {
                // Add top-level directories
                foreach (string topDir in Directory.GetDirectories(appdata))
                {
                    TreeNode topNode = new TreeNode(Path.GetFileName(topDir))
                    {
                        Tag = topDir,
                        ImageIndex = 0,
                        SelectedImageIndex = 0
                    };

                    // Add subdirectories and files recursively
                    AddDirectoryNodes(topDir, topNode);

                    EntryList.Nodes.Add(topNode);
                }

                // Add files in root of appdata
                foreach (string file in Directory.GetFiles(appdata))
                {
                    string ext = Path.GetExtension(file).ToLowerInvariant();
                    TreeNode fileNode = new TreeNode(Path.GetFileName(file))
                    {
                        Tag = file
                    };

                    // Set icons based on extension
                    switch (ext)
                    {
                        case ".png":
                        case ".jpg":
                            fileNode.ImageIndex = 1;
                            fileNode.SelectedImageIndex = 1;
                            break;
                        case ".loc":
                            fileNode.ImageIndex = 2;
                            fileNode.SelectedImageIndex = 2;
                            break;
                        case ".pck":
                        case ".fui":
                            fileNode.ImageIndex = 4;
                            fileNode.SelectedImageIndex = 4;
                            break;
                        case ".nbt":
                            fileNode.ImageIndex = 5;
                            fileNode.SelectedImageIndex = 5;
                            break;
                        case ".col":
                            fileNode.ImageIndex = 7;
                            fileNode.SelectedImageIndex = 7;
                            break;
                        case ".txt":
                            fileNode.ImageIndex = 6;
                            fileNode.SelectedImageIndex = 6;
                            break;
                        default:
                            fileNode.ImageIndex = 6;
                            fileNode.SelectedImageIndex = 6;
                            break;
                    }
                    EntryList.Nodes.Add(fileNode);
                }

                // enable related menus
                foreach (ToolStripMenuItem item in fileToolStripMenuItem.DropDownItems)
                {
                    item.Enabled = true;
                    item.Checked = true;
                }

                fileCount = EntryList.GetNodeCount(true);
            }
            catch (Exception)
            {
               // MessageBox.Show("Error building file tree: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                EntryList.EndUpdate();
            }

            await Task.Delay(500);
        }

        /// <summary>
        /// Recursively add directory nodes to a parent TreeNode
        /// </summary>
        private void AddDirectoryNodes(string dirPath, TreeNode parentNode)
        {
            // Add files in this directory
            try
            {
                foreach (string file in Directory.GetFiles(dirPath))
                {
                    string ext = Path.GetExtension(file).ToLowerInvariant();
                    TreeNode node = new TreeNode(Path.GetFileName(file))
                    {
                        Tag = file
                    };

                    switch (ext)
                    {
                        case ".png":
                        case ".jpg":
                            node.ImageIndex = 1;
                            node.SelectedImageIndex = 1;
                            break;
                        case ".loc":
                            node.ImageIndex = 2;
                            node.SelectedImageIndex = 2;
                            break;
                        case ".pck":
                        case ".fui":
                            node.ImageIndex = 4;
                            node.SelectedImageIndex = 4;
                            break;
                        case ".nbt":
                            node.ImageIndex = 5;
                            node.SelectedImageIndex = 5;
                            break;
                        case ".col":
                            node.ImageIndex = 7;
                            node.SelectedImageIndex = 7;
                            break;
                        case ".txt":
                            node.ImageIndex = 6;
                            node.SelectedImageIndex = 6;
                            break;
                        default:
                            node.ImageIndex = 6;
                            node.SelectedImageIndex = 6;
                            break;
                    }
                    parentNode.Nodes.Add(node);
                }

                // Add subdirectories
                foreach (string subDir in Directory.GetDirectories(dirPath))
                {
                    TreeNode subNode = new TreeNode(Path.GetFileName(subDir))
                    {
                        Tag = subDir,
                        ImageIndex = 0,
                        SelectedImageIndex = 0
                    };
                    // Recurse
                    AddDirectoryNodes(subDir, subNode);
                    parentNode.Nodes.Add(subNode);
                }
            }
            catch (Exception ex)
            {
                // ignore per-file errors, but log
                Console.WriteLine($"Error reading directory {dirPath}: {ex.Message}");
            }
        }

        #endregion

        #region Select file

        private void EntryList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (EntryList.SelectedNode == null || EntryList.SelectedNode.Tag == null)
                    return;

                string selectedPath = EntryList.SelectedNode.Tag.ToString();
                string ext = Path.GetExtension(selectedPath).ToLowerInvariant();

                if (ext == ".png" || ext == ".jpg")
                {
                    statusStrip1.Visible = true;
                    viewOnly1.Hide();
                    SizeLabel2.Visible = true;
                    richTextBox1.Hide();
                    tabControl1.Hide();
                    pictureBoxWithInterpolationMode2.Show();
                    pictureBoxWithInterpolationMode2.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBoxWithInterpolationMode2.InterpolationMode = InterpolationMode.NearestNeighbor;

                    using (var fs = new FileStream(selectedPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (Image skinPicture = Image.FromStream(fs))
                    {
                        pictureBoxWithInterpolationMode2.SetImageWithFade(skinPicture, 200);
                        SizeLabel2.Text = $"{skinPicture.Width} x {skinPicture.Height}";
                    }

                }

                else if (ext == ".fui")
                {
                    viewOnly1.Show();
                    statusStrip1.Hide();
                    richTextBox1.Text = "";
                    richTextBox1.Hide();
                    pictureBoxWithInterpolationMode2.Hide();
                    tabControl1.Hide();
                    return;
                }

                else if (ext == ".txt")
                {
                    viewOnly1.Hide();
                    richTextBox1.Show();
                    statusStrip1.Hide();
                    tabControl1.Show();
                    pictureBoxWithInterpolationMode2.Hide();
                    richTextBox1.Text = File.ReadAllText(selectedPath);
                    return;
                }
                else if (Directory.Exists(selectedPath))
                {
                    // Selected node is a folder - show nothing special
                    richTextBox1.Hide();
                    viewOnly1.Hide();
                    pictureBoxWithInterpolationMode2.Hide();
                    tabControl1.Hide();
                    statusStrip1.Visible = false;
                    return;
                }
                else
                {
                    // Other file types
                    richTextBox1.Text = "";
                    viewOnly1.Hide();
                    richTextBox1.Hide();
                    pictureBoxWithInterpolationMode2.Hide();
                    tabControl1.Hide();
                    statusStrip1.Visible = false;
                }
            }
            catch (Exception exep)
            {
                string errormsg = DateTime.Today.Day + "-" + DateTime.Today.Month + "-" + DateTime.Today.Year + "::" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + " -- " + exep.Message.ToString() + "\n\n" + exep.StackTrace.ToString();
                string logDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\PhoenixApplications\\ARCStudio" + "\\LOGS\\";
                try
                {
                    Directory.CreateDirectory(logDir);
                    File.AppendAllText(Path.Combine(logDir, "logFile-" + DateTime.Today.Day + "-" + DateTime.Today.Month + "-" + DateTime.Today.Year + ".log"), errormsg + "\n\n===============NEWLOG===============n");
                }
                catch { }
            }
        }

        private void DisposeImages()
        { 
            try
            {
                if (pictureBoxWithInterpolationMode2.Image != null)
                {
                    pictureBoxWithInterpolationMode2.Image.Dispose();
                    pictureBoxWithInterpolationMode2.Image = null;
                }

                if (loadedImage != null)
                {
                    pictureBoxWithInterpolationMode2.Image.Dispose();
                    pictureBoxWithInterpolationMode2.Image = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch { }
        }

        
        #endregion

        #region When Form Changes Size

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (pictureBoxWithInterpolationMode2 != null && metroTabPage1 != null && tabControl1 != null)
            {
                pictureBoxWithInterpolationMode2.Height = metroTabPage1.Height / 2;
                tabControl1.Height = metroTabPage1.Height / 2;
            }
        }

        #endregion

        #region Edit text when edited

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (EntryList.SelectedNode != null && EntryList.SelectedNode.Tag != null && Path.GetExtension(EntryList.SelectedNode.Tag.ToString()).ToLowerInvariant() == ".txt")
            {
                try
                {
                    File.WriteAllText(EntryList.SelectedNode.Tag.ToString(), richTextBox1.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving text file:  {ex.Message}", "Saving error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        #endregion

        #region Open file when double clicked

        private void EntryList_DoubleClick(object sender, EventArgs e)
        {
            if (EntryList.SelectedNode.Tag != null)
            {
                string selected = EntryList.SelectedNode.Tag.ToString();
                //Checks to see if selected minefile is a loc file
                switch (Path.GetExtension(selected).ToLowerInvariant())
                {
                    case (".loc"):
                        ARC.LOCEditor le = new ARC.LOCEditor(selected);
                        le.Show();
                        le.BringToFront();
                        break;

                    //Checks to see if selected minefile is a col file
                    case (".col"):
                        MessageBox.Show(".COL Editor Coming Soon!");
                        break;


                    //Checks to see if selected minefile is a fui file
                    case (".fui"):
                            ARC_Studio.FUIForm fui = new FUIForm(selected);
                            fui.Show();
                            fui.BringToFront();
                            break;


                    // NBT or no extension - attempt to launch NBTExplorer if present
                    case (""):
                    default:
                        try
                        {
                            string nbtExe = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\PhoenixApplications\\ARCStudio" + "\\NBTEditor\\NBTExplorer.exe";
                            if (File.Exists(nbtExe))
                            {
                                Process proc = new Process();
                                proc.StartInfo.FileName = nbtExe;
                                proc.StartInfo.Arguments = selected;
                                proc.Start();
                            }
                            else
                            {
                                MessageBox.Show(".NBT Editor Coming Soon!");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Could not open editor: " + ex.Message);
                        }
                        break;
                }


            }
        }

        #endregion

        #region Extract folder

        private void extractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EntryList.SelectedNode == null) return;

            if (EntryList.SelectedNode.ImageIndex == 0)
            {
                using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                {
                    fbd.Description = "Select where to extract the folder";
                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        string sourceDir = EntryList.SelectedNode.Tag.ToString();
                        string targetDir = Path.Combine(fbd.SelectedPath, EntryList.SelectedNode.Text);

                        try
                        {
                            CopyDirectory(sourceDir, targetDir);
                            Messenger.MessageBox($"Folder extracted successfully to:\n{targetDir}");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Extraction failed:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                return;
            }

            else
            {
                SaveFileDialog sfd = new SaveFileDialog();

                string ext = Path.GetExtension(EntryList.SelectedNode.Tag.ToString()).ToLowerInvariant();
                switch (ext)
                {
                    case ".png":
                        sfd.Filter = "PNG Image | *.png";
                        break;
                    case ".loc":
                        sfd.Filter = "Localization | *.loc";
                        break;
                    case ".fui":
                        sfd.Filter = "Fuscated Universal Image | *.fui";
                        break;
                    case ".col":
                        sfd.Filter = "Color file | *.col";
                        break;
                    case "":
                        sfd.Filter = "NBT Data | *.nbt";
                        break;
                    default:
                        sfd.Filter = "All Files | *.*";
                        break;
                }
                sfd.FileName = EntryList.SelectedNode.Text;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (ext == ".binka")
                        {
                            System.Diagnostics.Process binkman = new System.Diagnostics.Process();
                            binkman.StartInfo.FileName = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\PhoenixApplications\\ARCStudio" + "\\BinkMan\\BinkMan.exe";
                            binkman.StartInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\PhoenixApplications\\ARCStudio" + "\\BinkMan";
                            binkman.Start();
                            binkman.WaitForExit();
                            File.Copy(EntryList.SelectedNode.Tag.ToString(), sfd.FileName, true);
                        }
                        else
                            File.Copy(EntryList.SelectedNode.Tag.ToString(), sfd.FileName, true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Extract failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void CopyDirectory(string sourceDir, string destinationDir)
        {
            // Create destination if it doesn't exist
            Directory.CreateDirectory(destinationDir);

            // Copy all files
            foreach (var file in Directory.GetFiles(sourceDir))
            {
                string destFile = Path.Combine(destinationDir, Path.GetFileName(file));
                File.Copy(file, destFile, true);
            }

            // Copy all subdirectories
            foreach (var subDir in Directory.GetDirectories(sourceDir))
            {
                string destSubDir = Path.Combine(destinationDir, Path.GetFileName(subDir));
                CopyDirectory(subDir, destSubDir);
            }
        }


        #endregion

        #region Replace file

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EntryList.SelectedNode == null || EntryList.SelectedNode.Tag == null) return;

            string arcFolderPath = EntryList.SelectedNode.Tag.ToString();

            if (EntryList.SelectedNode.ImageIndex == 0)
            {
                MessageBox.Show("Select a folder not a file", "Invaild Selection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            /*using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Select where to replace the folder";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    string sourceFolder = fbd.SelectedPath;

                    ReplaceFolder(sourceFolder, arcFolderPath);
                    Messenger.MessageBox($"Folder replaced successfully from:\n{sourceFolder}");
                    ReloadTree();
                }*/


                else
                {
                    OpenFileDialog sfd = new OpenFileDialog();

                    string ext = Path.GetExtension(EntryList.SelectedNode.Tag.ToString()).ToLowerInvariant();
                    switch (ext)
                    {
                        case ".png":
                            sfd.Filter = "PNG Image | *.png";
                            break;
                        case ".loc":
                            sfd.Filter = "Localization | *.loc";
                            break;
                        case ".fui":
                            sfd.Filter = "Fuscated Universal Image | *.fui";
                            break;
                        case ".col":
                            sfd.Filter = "Color file | *.col";
                            break;
                        case "":
                            sfd.Filter = "NBT Data | *.nbt";
                            break;
                        default:
                            sfd.Filter = "All Files | *.*";
                            break;
                    }
                    sfd.FileName = EntryList.SelectedNode.Text;
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            File.Copy(sfd.FileName, EntryList.SelectedNode.Tag.ToString(), true);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Replace failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        

        private void ReloadTree()
        {
            EntryList.Nodes.Clear();
            openPck(currentFilePath);
        }

        private void ReplaceFolder(string sourceDir, string targetDir)
        { 
            if (Directory.Exists(targetDir))
                Directory.Delete(targetDir, true);
            Directory.CreateDirectory(targetDir);

            CopyFolder(sourceDir, targetDir);
          
        }

        private void CopyFolder(string sourceDir, string targetDir)
        {
            foreach (string file in Directory.GetFiles(sourceDir))
            {
                string name = Path.GetFileName(file);
                File.Copy(file, Path.Combine(targetDir, name), true);
            }

            foreach (string dir in Directory.GetDirectories(sourceDir))
            {
                string name = Path.GetFileName(dir);
                string newPath = Path.Combine(targetDir, name);
                Directory.CreateDirectory(newPath);
                CopyFolder(dir, newPath);
            }
        }

        #endregion

        #region Extract full arc

        private void extractFullArcToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (EntryList.Nodes.Count == 0)
            {
                MessageBox.Show("No ARC file loaded");
                return;
            }

            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    string output = fbd.SelectedPath;
                    TreeNode root = EntryList.Nodes[0];
                    ExtractFromTree(root, output);
                    Messenger.MessageBox("Successfully Extracted!");
                }
            }
        }

        private void ExtractFromTree(TreeNode node, string outputRoot)
        {
            string srcPath = node.Tag.ToString();
            string destPath = Path.Combine(outputRoot, node.FullPath);
            if (node.ImageIndex == 0)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(destPath));
                File.Copy(srcPath, destPath, true);
            }
            else
            {
                Directory.CreateDirectory(destPath);
                foreach (TreeNode child in node.Nodes)
                    ExtractFromTree(child, outputRoot);
            }
        }

        #endregion

        #region Private methods

        private void SaveAsBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog()
            {
                Filter = "ARC File (*.arc)|*.arc",
                RestoreDirectory = true
            };

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = fileDialog.FileName;
                SaveFile(filePath);
            }
        }

        private void arcPicture_Click(object sender, EventArgs e)
        {
            openToolStripMenuItem_Click(sender, e);
        }

        #endregion
    }
}
