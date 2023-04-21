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

namespace ConorWoodFinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Image icon;
        BitmapImage bitmap;

        public MainWindow()
        {
            InitializeComponent();
            //icon = new Image();

            try
            {
                bitmap = LoadBitmap(@"sun.png", 50.0);
                weather_icon.Source = bitmap;
            }

            catch (Exception e)  { MessageBox.Show(e.Message); }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        protected virtual BitmapImage LoadBitmap(String assetsRelativePath, double decodeWidth)
        {
            BitmapImage theBitmap = new BitmapImage();
            theBitmap.BeginInit();
            String basePath = System.IO.Path.Combine(Environment.CurrentDirectory, @"assets\");
            String path = System.IO.Path.Combine(basePath, assetsRelativePath);
            theBitmap.UriSource = new Uri(path, UriKind.Absolute);
            theBitmap.DecodePixelWidth = (int)decodeWidth;
            theBitmap.EndInit();

            return theBitmap;
        }
    }
}
