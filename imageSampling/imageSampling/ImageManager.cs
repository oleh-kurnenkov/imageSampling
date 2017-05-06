using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace imageSampling
{
    class ImageManager
    {
        private ImageManager()
        {

        }
        private static volatile ImageManager instance;
        private static object syncRoot = new Object();

        public static ImageManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ImageManager();
                    }
                }

                return instance;
            }
        }
        public List<ImageModel> getImagesWithSize(List<ImageModel> images,Size imageSize)
        {
            List<ImageModel> filteredImages = new List<ImageModel>();
            foreach (ImageModel image in images)
            {
                if (image.image.Size == imageSize)
                {
                    filteredImages.Add(image);
                }
            }
            return filteredImages;
        }
    }
}
