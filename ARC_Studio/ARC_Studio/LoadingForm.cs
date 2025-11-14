using DarkModeForms;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace ARC_Studio
{
    public partial class LoadingForm : Form
    {
        private ProgressBar pb = null;

        private DarkModeCS ng = null;

        public LoadingForm(string message)
        {
            InitializeComponent();

            loadingLabel.Text = message;
            flatProgressBar1.MarqueeAnimationSpeed = 30;

            ng = new DarkModeCS(this)
            { 
                ColorMode = DarkModeCS.DisplayMode.SystemDefault
            };
        }

        private void LoadingForm_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(
                  this.ClientRectangle,
                  Color.FromArgb(40, 40, 40),   // top color
                  Color.FromArgb(25, 25, 25),   // bottom color
            LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }

        }

        private void flatProgressBar1_Click(object sender, EventArgs e)
        {
          
        }

        private void ShowFakeProgress() // Loading Not Finished Yet
        {
            flatProgressBar1.Value = 0;
            flatProgressBar1.Minimum = 0;
            flatProgressBar1.Maximum = 100;
            flatProgressBar1.Step = 1;

            Timer t = new Timer();
            t.Interval = 50; // ms (speed of fill)
            t.Tick += (s, e) =>
            {
                if (flatProgressBar1.Value < 100)
                    flatProgressBar1.PerformStep();
                else
                    t.Stop(); // stop when done
            };
            t.Start();
        }


        public void UpdateProgress(int value)
        {
            if (value >= pb.Minimum && value <= pb.Maximum)
            {
                pb.Value = value;
            }
        }

        private void loadingLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
