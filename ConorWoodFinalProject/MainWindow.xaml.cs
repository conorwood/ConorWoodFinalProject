﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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
        private string? key = "b06f9ec5e918401889a152444230604";


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

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string zipCode = ZipCodeTextBox.Text;
            HttpClient client = new();

            var json = await client.GetStringAsync($"http://api.weatherapi.com/v1/current.json?key={key}&q={zipCode}");

            RootObject? root = JsonSerializer.Deserialize<RootObject?>(json);

            CityTextBlock.Text = root?.location?.name;
            ConditionTextBlock.Text = root?.location?.localtime;
            TempTextBlock.Text = root?.current?.temp_f.ToString();
            rounded_border.Background = Brushes.AliceBlue;


            
        }

    }
}