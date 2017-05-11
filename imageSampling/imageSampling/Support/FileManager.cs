using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Drawing;
using System.Diagnostics;

namespace imageSampling
{
    class FileManager
    {
        private FileManager()
        {

        }
        private static volatile FileManager instance;
        private static object syncRoot = new Object();

        public static FileManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new FileManager();
                    }
                }

                return instance;
            }
        }

        public List<ImageModel> chooseFiles()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Multiselect = true;
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|All files (*.*)|*.*";

            Nullable<bool> result = dlg.ShowDialog();
            
            List<ImageModel> images = new List<ImageModel>();
            foreach (string path in dlg.FileNames)
            {
                images.Add(new ImageModel(path));
            }
            return images;
        }
        public ImageModel chooseImage()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|All files (*.*)|*.*";

            Nullable<bool> result = dlg.ShowDialog();
            if (dlg.FileName != null)
            {
                return new ImageModel(dlg.FileName);
            }
            else
            {
                return null;
            }
        }
        public string createFolder(string name, string path)
        {
            string folderPath = path + "\\" + name;
            System.IO.Directory.CreateDirectory(folderPath);
            return folderPath;
        }
        public void addImagesToFolder(List<ImageModel> images, string folder)
        {
            foreach (ImageModel image in images)
            {
                try
                {
                    image.image.Save(folder + "\\" + image.Title);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                Process.Start(folder);
            }
        }

        public string getDirectoryPath(string path)
        {
            return Path.GetDirectoryName(path);
        }
    }
}
