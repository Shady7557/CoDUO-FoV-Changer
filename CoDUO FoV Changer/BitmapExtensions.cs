using System.Drawing;
using System.Drawing.Drawing2D;

namespace BitmapExtension
{
    class BitmapHelper
    {
        public static Bitmap ResizeImage(Bitmap imgToResize, Size size, InterpolationMode interopMode = InterpolationMode.HighQualityBicubic)
        {
            var b = new Bitmap(size.Width, size.Height);
            using (var g = Graphics.FromImage(b))
            {
                g.InterpolationMode = interopMode;
                g.DrawImage(imgToResize, 0, 0, size.Width, size.Height);
            }
            return b;
        }
    }
}
