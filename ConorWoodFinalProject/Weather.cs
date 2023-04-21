using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace ConorWoodFinalProject
{
    internal class Weather
    {
        private RootObject? rootObject;
        private string? cityName;
        private string? weatherCondition;
        private string? tempF;

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

        //private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        private async void getWeatherInfo(string zipCode)
        {
            //string zipCode = ZipCodeTextBox.Text;
            HttpClient client = new();

            var json = await client.GetStringAsync($"http://api.weatherapi.com/v1/current.json?key={key}&q={zipCode}");

            RootObject? root = JsonSerializer.Deserialize<RootObject?>(json);

            this.cityName = root?.location?.name;
            this.weatherCondition = root?.location?.localtime; // for now
            this.tempF = root?.current?.temp_f.ToString();


            /*
            ConditionTextBlock.Text = root?.location?.localtime;
            TempTextBlock.Text = root?.current?.temp_f.ToString();
            rounded_border.Background = Brushes.AliceBlue;
            */
        }
    }
}
