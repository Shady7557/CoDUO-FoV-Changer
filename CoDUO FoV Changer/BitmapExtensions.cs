using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BitmapExtension
{
    internal class BitmapHelper
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

        public static void ScalePictureBox(PictureBox pictureBox, Image initialImage = null)
        {
            Bitmap finalImg = null;
            if (pictureBox.Image != null) finalImg = new Bitmap(pictureBox.Image, pictureBox.Width, pictureBox.Height);
            if (initialImage != null) finalImg = new Bitmap(initialImage, pictureBox.Width, pictureBox.Height);
            if (finalImg != null)
            {
                pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
                pictureBox.Image = finalImg;
            }
        }
    }
}
