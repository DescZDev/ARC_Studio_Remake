using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ARC_Studio
{
    public class ChangableMenuListView : ListView
    {
        public ChangableMenuListView()
        {
            //SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.None;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = PixelOffsetMode.Half;
            g.CompositingQuality = CompositingQuality.HighSpeed;

            using (var brush = new SolidBrush(Color.FromArgb(40, Color.White))) 
            g.FillRectangle(brush, Bounds);

            e.Graphics.Clear(BackColor);
        }

        protected override void OnItemSelectionChanged(ListViewItemSelectionChangedEventArgs e)
        {
            base.OnItemSelectionChanged(e);

            int selectedCount = SelectedIndices.Count;

            if(selectedCount > 1)
            {
                ContextMenuStrip = MultiSelectedContextMenuStrip;
            }
            else
            {
                ContextMenuStrip = SingleSelectedContextMenuStrip;
            }
        }

        [Browsable(true)]
        [Category("Behavior")]
        public ContextMenuStrip MultiSelectedContextMenuStrip
        {
            get;
            set;
        }

        [Browsable(true)]
        [Category("Behavior")]
        public ContextMenuStrip SingleSelectedContextMenuStrip
        {
            get;
            set;
        }
    }
}
