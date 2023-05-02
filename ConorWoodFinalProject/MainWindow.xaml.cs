using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace ConorWoodFinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        Weather weather;
        Controller myController;
       

        public MainWindow()
        {
            InitializeComponent();
            myController = new Controller();
            weather = new Weather();
            DataContext = this.myController;

           
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Weather selectedWeather = (Weather)locations.SelectedItem;
            if (selectedWeather != null)
            {
                updateWeatherDisplay(selectedWeather.CityName + " " + selectedWeather.Region);
            }
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        { 
            updateWeatherDisplay(newLocationTextBox.Text); 
        }

        private void newLocationButton_Click(object sender, RoutedEventArgs e)
        {
            NewLocationBox.Visibility = Visibility.Visible;
        }

        private void addLocationButton_Click(object sender, RoutedEventArgs e)
        {
            SubmitButton_Click(sender, e);
        }

        
        


        private void setBackground(Weather w)
        {
            var gradientBrush = new LinearGradientBrush();
            gradientBrush.StartPoint = new Point(0, 0);
            gradientBrush.EndPoint = new Point(0, 1);

            if (w.IsDay == 0)
            {
                gradientBrush.GradientStops.Add(new GradientStop(Color.FromRgb(44, 44, 68), 0));
                gradientBrush.GradientStops.Add(new GradientStop(Color.FromRgb(128, 128, 128), 1));
            }

            else
            {
                gradientBrush.GradientStops.Add(new GradientStop(Color.FromRgb(49, 113, 175), 0));
                gradientBrush.GradientStops.Add(new GradientStop(Color.FromRgb(128, 128, 128), 1));
            }

            grid.Background = gradientBrush;

            
        }

        private void addToFavoritesButton_Click(object sender, RoutedEventArgs e)
        {
            myController.AddWeather(weather);

            locations.SetBinding(ListBox.ItemsSourceProperty, new Binding("favorites") { Source = myController });
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            myController.SerializeData();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            myController.DeserializeData();
            locations.SetBinding(ListBox.ItemsSourceProperty, new Binding("favorites") { Source = myController });
        }


        private async void updateWeatherDisplay(string location)
        {
            Weather currentWeather = new Weather();
            ForecastWeather forecastWeather = new ForecastWeather();
            currentWeather.Forecast = forecastWeather;
            weather = currentWeather;
            weather.ZipCode = newLocationTextBox.Text;

            try
            {
                await currentWeather.getWeatherInfo(location);
                await currentWeather.Forecast.getWeatherInfo(location);
                await currentWeather.Astronomy.getAstroInfo(location);
            }

            catch (HttpRequestException ex)
            {
                MessageBox.Show("Error Retrieving Data.\n" + ex.Message);
                return;
            }


            CityTextBlock.Text = $"{currentWeather.CityName}, {currentWeather.Region}";
            ConditionTextBlock.Text = currentWeather.WeatherCondition;
            TempTextBlock.Text = $"{currentWeather.TempF} °F";
            //rounded_border.Background = Brushes.DarkGray;
            weather_icon.Source = currentWeather.Bitmap;


            Day1Text.Text = $"{forecastWeather.Days[0].day.maxtemp_f} °F";
            Day2Text.Text = $"{forecastWeather.Days[1].day.maxtemp_f} °F";
            Day3Text.Text = $"{forecastWeather.Days[2].day.maxtemp_f} °F";


            Day1Icon.Source = forecastWeather.Icons[0];
            Day2Icon.Source = forecastWeather.Icons[1];
            Day3Icon.Source = forecastWeather.Icons[2];

            Date1.Text = forecastWeather.formattedDate(forecastWeather.Days[0]);
            Date2.Text = forecastWeather.formattedDate(forecastWeather.Days[1]);
            Date3.Text = forecastWeather.formattedDate(forecastWeather.Days[2]);


            feelslikeText.Text = $"Feels Like: {currentWeather.Feelslike_temp} °F";
            windText.Text = $"Wind: {currentWeather.Wind} MPH";
            uvText.Text = $"UV Index: {currentWeather.UV}";

            sunriseText.Text = $"Sunrise: {currentWeather.Astronomy.Sunrise}";
            sunsetText.Text = $"Sunset: {currentWeather.Astronomy.Sunset}";
            moonPhaseText.Text = $"Moon Phase: {currentWeather.Astronomy.MoonPhase}";

            setBackground(currentWeather);


            ForecastBorder.Visibility = Visibility.Visible;
            newLocationTextBox.Text = " ";
            NewLocationBox.Visibility = Visibility.Collapsed;
        }

        private void deleteLocationButton_Click(object sender, RoutedEventArgs e)
        {
            Weather selectedWeather = (Weather)locations.SelectedItem;
            myController.DeleteWeather(selectedWeather);
            
        }

         

    }
}
