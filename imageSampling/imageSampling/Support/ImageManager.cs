using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;


namespace imageSampling
{
    class ImageManager
    {
        private CascadeClassifier haarCascade;

        private ImageManager()
        {
            haarCascade = new CascadeClassifier("haarcascade_frontalface_alt_tree.xml");
            filemanager = FileManager.Instance;
        }
        private static volatile ImageManager instance;
        private static object syncRoot = new Object();
        FileManager filemanager;

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
        public void filterImagesForSize(List<ImageModel> images,Size size) { 
  
            List<ImageModel> filteredImages = getImagesWithSize(images,size);
            if(filteredImages != null)
            {
                string path = filemanager.getDirectoryPath(filteredImages[0].path);
                string directoryName = size.Width.ToString()+"x" + size.Height.ToString();
                string folderPath = filemanager.createFolder(directoryName, path);
                filemanager.addImagesToFolder(filteredImages, folderPath);
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
        public void filterImagesForColor(List<ImageModel> images, Color color)
        {
            List<ImageModel> filteredImages = getImagesWithColor(images,color);
            if (filteredImages != null && filteredImages.Count > 0)
            {
                string path = filemanager.getDirectoryPath(filteredImages[0].path);
                string directoryName = color.R.ToString() + "," + color.G.ToString() + "," + color.B.ToString();
                string folderPath = filemanager.createFolder(directoryName, path);
                filemanager.addImagesToFolder(filteredImages, folderPath);
            }
        }

        public List<ImageModel> getImagesWithColor(List<ImageModel> images, Color color)
        {


            List<ImageModel> filteredImages = new List<ImageModel>();
            foreach (ImageModel image in images)
            {
                Color imageColor = image.getImageColor();
                if (isColorsSimilar(color, imageColor))
                {
                    filteredImages.Add(image);
                }
            }
            return filteredImages;
        }
        public bool isColorsSimilar(Color color, Color ImageColor)
        {
            int delta = 60;
            var distance = Math.Sqrt(Math.Pow(color.R - ImageColor.R,2) + Math.Pow(color.G - ImageColor.G, 2) + Math.Pow(color.B - ImageColor.B, 2));
            return distance < delta;
            
        }

        public void filterImagesForTime(List<ImageModel> images, DateTime time)
        {
            List<ImageModel> filteredImages = getImagesWithTime(images, time);
            if (filteredImages != null && filteredImages.Count > 0)
            {
                string path = filemanager.getDirectoryPath(filteredImages[0].path);
                string directoryName = time.Year.ToString() + "." + time.Month.ToString() + "." + time.Day.ToString();
                string folderPath = filemanager.createFolder(directoryName, path);
                filemanager.addImagesToFolder(filteredImages, folderPath);
            }
        }

        private List<ImageModel>getImagesWithTime(List<ImageModel> images, DateTime time)
        {
            List<ImageModel> filteredImages = new List<ImageModel>();
            foreach (ImageModel image in images)
            {
                if (image.time.Year == time.Year && image.time.Month == time.Month && image.time.Day == time.Day)
                {
                    filteredImages.Add(image);
                }
            }
            return filteredImages;
        }

        public void filterImagesForSimilarImage(List<ImageModel> images, ImageModel image)
        {
            List<ImageModel> filteredImages = getImagesWithImage(images, image);
            if (filteredImages != null && filteredImages.Count > 0)
            {
                string path = filemanager.getDirectoryPath(filteredImages[0].path);
                string directoryName = Path.GetFileNameWithoutExtension(image.Title);
                string folderPath = filemanager.createFolder(directoryName, path);
                filemanager.addImagesToFolder(filteredImages, folderPath);
            }
        }

        private List<ImageModel> getImagesWithImage(List<ImageModel> images, ImageModel imageToCompare)
        {
            List<ImageModel> filteredImages = new List<ImageModel>();
            foreach (ImageModel image in images)
            {
                if (imageCompare(imageToCompare.bitmap,image.bitmap) < 5)
                {
                    filteredImages.Add(image);
                }
            }
            return filteredImages;
        }

        public void filterImagesWithFaces(List<ImageModel> images)
        {
            List<ImageModel> filteredImages = getImagesWithFaces(images);
            if (filteredImages != null && filteredImages.Count > 0)
            {
                string path = filemanager.getDirectoryPath(filteredImages[0].path);
                string directoryName = "Faces";
                string folderPath = filemanager.createFolder(directoryName, path);
                filemanager.addImagesToFolder(filteredImages, folderPath);
            }
        }

        private List<ImageModel> getImagesWithFaces(List<ImageModel> images)
        {
            List<ImageModel> filteredImages = new List<ImageModel>();
            foreach (ImageModel image in images)
            {
                Image<Gray, Byte> grayFrame = new Image<Gray, byte>(image.fullSizeBitmap);
                Rectangle[] dtc = null;

                dtc = haarCascade.DetectMultiScale(grayFrame,1.4,1);
                if (dtc != null && dtc.Count() > 0)
                {
                    filteredImages.Add(image);
                }
            }
            return filteredImages;
        }


        private int imageCompare(Bitmap first, Bitmap second)
        {
            int DiferentPixels = 0;
            float avr1 = 0, avr2 = 0;
            int[] clrs1 = new int[64];
            int[] clrs2 = new int[64];
            int[] clrs3 = new int[64];
            int[] clrs4 = new int[64];
            Bitmap container = new Bitmap(first.Width, first.Height);
            for (int i = 0; i < first.Width; ++i)
            {
                for (int j = 0; j < first.Height; ++j)
                {
                    Color secondColor = second.GetPixel(i, j);
                    Color firstColor = first.GetPixel(i, j);
                    clrs1[i * j] = firstColor.R + firstColor.G + firstColor.B;
                    clrs2[i * j] = secondColor.R + secondColor.G + secondColor.B;
                    avr1 += clrs1[i * j];
                    avr2 += clrs2[i * j];
                }
            }
            avr1 = (float)(avr1 / 64);
            avr2 = (float)(avr2 / 64);
            for (int i = 0; i < first.Width; ++i)
            {
                for (int j = 0; j < first.Height; ++j)
                {
                    if (clrs1[i * j] >= avr1)
                        clrs3[i * j] = 1;
                    else
                        clrs3[i * j] = 0;

                    if (clrs2[i * j] >= avr2)
                        clrs4[i * j] = 1;
                    else
                        clrs4[i * j] = 0;
                }
            }
            for (int i = 0; i < first.Width; ++i)
            {
                for (int j = 0; j < first.Height; ++j)
                {
                    if (clrs3[i * j] != clrs4[i * j])
                        DiferentPixels++;

                }
            }
            return DiferentPixels;
        }
    }
}
