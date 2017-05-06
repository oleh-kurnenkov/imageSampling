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
        public string path;
        public string Title { get; set; }
        public Image image { get; set; }
        public BitmapImage bitmapImage { get; set; }
       public ImageModel(string path)
        {
            this.path = path;
            this.Title = Path.GetFileName(path);
            this.image = Image.FromFile(path);
            this.bitmapImage = new BitmapImage(new Uri(Title, UriKind.RelativeOrAbsolute));
        }
    }
}
