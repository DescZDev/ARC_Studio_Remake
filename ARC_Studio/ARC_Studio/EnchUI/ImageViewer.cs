using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

public class ImageViewer : Control
{
    public Image currentImage;
    private float opacity = 0f;
    private float glow = 0f;
    private bool glowUp = true;
    private Timer glowTimer;

    public Color GlowColorStart { get; set; } = Color.Blue;
    public Color GlowColorEnd { get; set; } = Color.Bisque;
    public int BorderSize { get; set; } = 4;

    public ImageViewer()
    {
        DoubleBuffered = true;

        glowTimer = new Timer { Interval = 20 };
        glowTimer.Tick += (s, e) => { GlowTick(); };
        glowTimer.Start();
    }

    public async void SetImage(Image img, int fadeMs = 200)
    {
        currentImage = img;
        opacity = 0f;

        var sw = System.Diagnostics.Stopwatch.StartNew();
        while (opacity < 1f)
        {
            opacity = Math.Min(1f, sw.ElapsedMilliseconds / (float)fadeMs);
            Invalidate();
            await System.Threading.Tasks.Task.Delay(10);
        }
    }

    private void GlowTick()
    {
        glow += glowUp ? 0.04f : -0.04f;

        if (glow >= 1f) glowUp = false;
        if (glow <= 0f) glowUp = true;

        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        if (currentImage != null)
        {
            using (var ia = new ImageAttributes())
            {
                var cm = new ColorMatrix { Matrix33 = opacity };
                ia.SetColorMatrix(cm);

  
                int availableW = Width - BorderSize * 2;
                int availableH = Height - BorderSize * 1;

                float imgW = currentImage.Width;
                float imgH = currentImage.Height;

                float ratio = Math.Min(availableW / imgW, availableH / imgH);

                int drawW = (int)(imgW * ratio);
                int drawH = (int)(imgH * ratio);

                int drawX = (Width - drawW) / 2;
                int drawY = (Height - drawH) / 2;

                // Draw the image with opacity and correct aspect ratio
                using (var ia1 = new ImageAttributes())
                {
                    cm = new ColorMatrix { Matrix33 = opacity };
                    ia1.SetColorMatrix(cm);

                    e.Graphics.DrawImage(
                        currentImage,
                        new Rectangle(drawX, drawY, drawW, drawH),
                        0, 0, currentImage.Width, currentImage.Height,
                        GraphicsUnit.Pixel,
                        ia1
                    );
                }

            }
        

        // GLOW
        using (var lg = new LinearGradientBrush(ClientRectangle,
            GlowColorStart,
            GlowColorEnd,
            LinearGradientMode.Horizontal))
        {
            int alpha = (int)(30 + glow * 50);
            var glowPen = new Pen(Color.FromArgb(alpha, GlowColorStart), BorderSize);
            e.Graphics.DrawRectangle(glowPen,
                new Rectangle(BorderSize / 1, BorderSize / 1,
                Width - BorderSize, Height - BorderSize));
        }
        }
    }
}
