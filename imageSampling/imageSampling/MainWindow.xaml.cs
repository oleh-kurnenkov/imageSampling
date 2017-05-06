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
using System.Drawing;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace imageSampling
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        FileManager filemanager;
        List<ImageModel> images;
        List<Grid> criteriaGrids;
        ImageModel sampleImage;
        int selectedCreterionIndex;
        public MainWindow()
        {
            InitializeComponent();
            filemanager = FileManager.Instance;
            selectedCreterionIndex = 0;
            criteriaGrids = new List<Grid>();
            criteriaGrids.Add(sizeGrid);
            criteriaGrids.Add(colorGrid);
            criteriaGrids.Add(timeGrid);
            criteriaGrids.Add(facesGrid);
            criteriaGrids.Add(hashGrid);
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
            selectedCreterionIndex = comboBox.SelectedIndex;
            showCriterionGrid(selectedCreterionIndex);
        }

        private void sampleImageButton_Click(object sender, RoutedEventArgs e)
        {
            sampleImage = filemanager.chooseImage();
            sampleImageView.Source = sampleImage.bitmapImage;
        }

        private void showCriterionGrid(int index)
        {
            if (criteriaGrids != null) {
                for (int i = 0; i < criteriaGrids.Count; i++)
                {
                    Grid grid = criteriaGrids[i];
                    if (i == selectedCreterionIndex)
                    {
                        grid.Visibility = Visibility.Visible;
                    } else
                    {
                        grid.Visibility = Visibility.Hidden;
                    }
                }
            }
        }
    }
}
