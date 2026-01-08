using DarkModeForms;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARC_Studio
{
    public partial class StartForm : Form
    {
        private DarkModeCS dm = null;
        public string arcfile = "";
        string pad = null;
        public string appdata = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ARC_Data", "Media");

        public StartForm()
        {
            InitializeComponent();

            dm = new DarkModeCS(this)
            {
                ColorMode = DarkModeCS.DisplayMode.DarkMode
            };
        }

        private void StartForm_Load(object sender, EventArgs e)
        {
            LoadRecentFiles();
        }

        public void LoadRecentFiles()
        {
            var list = EnchUI.RecentFileManager.GetRecentFiles();

            RecentList.Items.Clear();

            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (!File.Exists(list[i]))
                    RecentList.Items.Remove(list[i]);
            }
            list = EnchUI.RecentFileManager.GetRecentFiles();

            if (list.Count == 0)
            {
                RecentList.Enabled = false;
                RecentList.Items.Add("No recent files");
                RecentList.SelectedIndex = -1;
                return;
            }
            RecentList.Enabled = true;

            foreach (string file in list)
            {
                RecentList.Items.Add(file);
            }
            RecentList.SelectedIndex = -1;
        }

        private async void RecentList_DoubleClick(object sender, EventArgs e)
        {
            if (!RecentList.Enabled) return;
            if (RecentList.SelectedIndex == -1) return;

            //var item = RecentList.SelectedItems[0];
            string arcPath = RecentList.SelectedItems[0].ToString();
            if (arcPath == "No recent files") return;

            if (!File.Exists(arcPath))
            {
                MessageBox.Show("This file no longer exists", "Missing file");

                LoadRecentFiles();
                return;
            }

            ARCStudio arcS = new ARCStudio();
            arcS.OpenArcFromPath(arcPath);
            arcS.Show();
            //arcS.Activate();
            this.Close();
        }

        private async void OpenFileBtn_Click(object sender, EventArgs e)
        {
            await OpenArcAndCloseStart();
        }

        private async Task OpenArcAndCloseStart()
        {
            EnchUI.RecentFileManager.Add(pad);
            ARCStudio studio = new ARCStudio();
            studio.Show();
            studio.StartOneClick();
        }
    }
}
