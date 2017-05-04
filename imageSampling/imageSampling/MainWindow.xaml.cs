using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace imageSampling
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        FileManager filemanager;
        List<ImageModel> images;
        public MainWindow()
        {
            InitializeComponent();
            filemanager = FileManager.Instance;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            images = filemanager.chooseFiles();
            imagesList.ItemsSource = images;
        }

        private void samplingButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
