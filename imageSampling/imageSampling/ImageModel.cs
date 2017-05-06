using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace imageSampling
{
    class ImageModel
    {
        public string Title { get; set; }
        public Image image { get; set; }
        public BitmapImage bitmapImage { get; set; }
       public ImageModel(string path)
        {
            this.Title = path;
            this.image = Image.FromFile(path);
            this.bitmapImage = new BitmapImage(new Uri(Title, UriKind.RelativeOrAbsolute));
        }
    }
}
