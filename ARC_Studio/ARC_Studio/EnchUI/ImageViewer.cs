using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

public class ImageViewer : Control
{
    private Image _currentImage;
    private float opacity = 1f;
    private float glow = 0f;
    private bool glowUp = true;
    private readonly Timer glowTimer;

    public Color GlowColorStart { get; set; } = Color.Blue;
    public Color GlowColorEnd { get; set; } = Color.Bisque;
    public int BorderSize { get; set; } = 4;

    public Image CurrentImage
    {
        get => _currentImage;
        set
        {
            _currentImage = value;
            Invalidate();
        }
    }

    public float Opacity
    {
        get => opacity;
        set
        {
            opacity = Math.Max(0f, Math.Min(1f, value));
            Invalidate();
        }
    }

    public ImageViewer()
    {
        SetStyle(
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.UserPaint |
            ControlStyles.ResizeRedraw,
            true);

        glowTimer = new Timer
        {
            Interval = 30
        };
        glowTimer.Tick += GlowTimer_Tick;
        glowTimer.Start();
    }

    private void GlowTimer_Tick(object sender, EventArgs e)
    {
        if (glowUp)
        {
            glow += 0.02f;
            if (glow >= 1f)
                glowUp = false;
        }
        else
        {
            glow -= 0.02f;
            if (glow <= 0f)
                glowUp = true;
        }

        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        e.Graphics.Clear(BackColor);

        DrawImage(e.Graphics);
        DrawGlowBorder(e.Graphics);
    }

    public void DrawImage(Graphics g)
    {
        if (_currentImage == null)
            return;

        Rectangle destRect = ClientRectangle;

        using (var attributes = new ImageAttributes())
        {
            var matrix = new ColorMatrix
            {
                Matrix33 = opacity
            };

            attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            g.DrawImage(
                _currentImage,
                destRect,
                0,
                0,
                _currentImage.Width,
                _currentImage.Height,
                GraphicsUnit.Pixel,
                attributes);
        }
    }

    private void DrawGlowBorder(Graphics g)
    {
        if (BorderSize <= 0)
            return;

        int alpha = (int)(30 + glow * 80);

        using (var brush = new LinearGradientBrush(
            ClientRectangle,
            GlowColorStart,
            GlowColorEnd,
            LinearGradientMode.Horizontal))
        using (var pen = new Pen(Color.FromArgb(alpha, GlowColorStart), BorderSize))
        {
            Rectangle rect = new Rectangle(
                BorderSize / 2,
                BorderSize / 2,
                Width - BorderSize,
                Height - BorderSize);

            g.DrawRectangle(pen, rect);
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            glowTimer?.Dispose();
            _currentImage?.Dispose();
        }

        base.Dispose(disposing);
    }
}