using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.IO;

namespace imageSampling
{
    class ImageModel
    {
        public string path { get; set; }
        public string Title { get; set; }
        public Image image { get; set; }
        public BitmapImage bitmapImage { get; set; }
        public DateTime time;
        public Bitmap bitmap;
        public Bitmap fullSizeBitmap;
       public ImageModel(string path)
        {
            this.path = path;
            this.Title = Path.GetFileName(path);
            this.image = Image.FromFile(path);
            this.bitmapImage = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
            this.time = System.IO.File.GetLastWriteTime(path);
            this.bitmap = new Bitmap(image, new Size(8, 8));
            this.fullSizeBitmap = new Bitmap(image);
        }
        private Image ScaledImage(int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);
            newWidth = newWidth > 0 ? newWidth : 1;
            newHeight = newHeight > 0 ? newHeight : 1;

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }

        public Color getImageColor()
        {
            Bitmap imageBitmap = new Bitmap(ScaledImage(128, 128));
            Bitmap onePixelImage = new Bitmap(ScaledImage(1, 1));
            Color mostCommonColor = onePixelImage.GetPixel(0,0);
            int height = imageBitmap.Height;
            int width = imageBitmap.Width;
            int red = 0;
            int green = 0;
            int blue = 0;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Color col = imageBitmap.GetPixel(i, j);
                    red += Convert.ToInt32(col.R);
                    green += Convert.ToInt32(col.G);
                    blue += Convert.ToInt32(col.B);
                }
            }
            int pixelsCount = (height * width);
            red /= pixelsCount;
            green /= pixelsCount;
            blue /= pixelsCount;

            Color imageColor = Color.FromArgb(red,green,blue);

            return imageColor;
        }

    }

}
