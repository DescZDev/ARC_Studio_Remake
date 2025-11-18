using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace ARC_Studio
{
    public class PictureBoxWithInterpolationMode : PictureBox
    {
        public InterpolationMode InterpolationMode { get; set; } = InterpolationMode.HighQualityBicubic;
        public bool KeepAspectRatio { get; set; } = true;
        public bool DrawFrame { get; set; } = false;
        public Color FrameColor { get; set; } = Color.FromArgb(255, 200, 160, 30); 
        public int FrameThickness { get; set; } = 4;

        public PictureBoxWithInterpolationMode()
        {
            this.SizeMode = PictureBoxSizeMode.Normal;
            this.DoubleBuffered = true;
        }

        public void SetImageFromFile(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path)) return;

            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (var tmp = Image.FromStream(fs))
                {
                    // Clone to a new bitmap so the Image doesn't reference the stream
                    var clone = new Bitmap(tmp);
                    // dispose previous
                    var prev = this.Image;
                    this.Image = clone;
                    prev?.Dispose();
                }
            }
            catch
            {
                try
                {
                    var prev = this.Image;
                    this.Image = Image.FromFile(path);
                    prev?.Dispose();
                }
                catch { }
            }

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            var g = pe.Graphics;
            g.Clear(this.BackColor);

            g.InterpolationMode = this.InterpolationMode;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;

            if (this.Image != null)
            {
                Rectangle dest;
                if (!KeepAspectRatio)
                {
                    dest = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
                }
                else
                {
                    int boxW = this.ClientSize.Width;
                    int boxH = this.ClientSize.Height;
                    float imgW = this.Image.Width;
                    float imgH = this.Image.Height;
                    float scale = Math.Min((float)boxW / imgW, (float)boxH / imgH);
                    int drawW = Math.Max(1, (int)(imgW * scale));
                    int drawH = Math.Max(1, (int)(imgH * scale));
                    int drawX = (boxW - drawW) / 2;
                    int drawY = (boxH - drawH) / 2;
                    dest = new Rectangle(drawX, drawY, drawW, drawH);
                }

                using (var ia = new ImageAttributes())
                {
                    g.DrawImage(this.Image, dest, 0, 0, this.Image.Width, this.Image.Height, GraphicsUnit.Pixel, ia);
                }

                // draw border/frame (inset so frame doesn't overlap image content)
                if (DrawFrame && FrameThickness > 0)
                {
                    int inset = FrameThickness / 2;
                    using (var pen = new Pen(FrameColor, FrameThickness))
                    {
                        pen.Alignment = PenAlignment.Inset;
                        var r = new Rectangle(inset, inset, this.ClientSize.Width - FrameThickness, this.ClientSize.Height - FrameThickness);
                        g.DrawRectangle(pen, r);
                    }
                }
            }
            else
            {
                // Nothing to draw: optionally draw a placeholder
                base.OnPaint(pe);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Image?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

