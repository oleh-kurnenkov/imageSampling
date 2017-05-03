using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imageSampling
{
    class ImageModel
    {
        public string Title { get; set; }
       public ImageModel(string path)
        {
            this.Title = path;
        }
    }
}
