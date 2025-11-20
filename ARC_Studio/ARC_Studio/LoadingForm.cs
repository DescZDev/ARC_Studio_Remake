using DarkModeForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ARC_Studio
{
    public partial class LoadingForm : Form
    {
        private ProgressBar pb = null;
        private Color borderColor = Color.Purple;

        private DarkModeCS ng = null;

        public LoadingForm(string message)
        {
            InitializeComponent();

            loadingLabel.Text = message;
            flatProgressBar1.MarqueeAnimationSpeed = 30;

            ng = new DarkModeCS(this)
            {
                ColorMode = DarkModeCS.DisplayMode.DarkMode
            };
        }

        private void LoadingForm_Load(object sender, EventArgs e)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Pen pen = new Pen(borderColor, 3);
            e.Graphics.DrawRectangle(pen, new Rectangle(1, 1, Width - 3, Height - 3));
        }

        private void flatProgressBar1_Click(object sender, EventArgs e)
        {
          
        }

        private void ShowFakeProgress() 
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
