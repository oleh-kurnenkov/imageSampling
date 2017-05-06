using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
