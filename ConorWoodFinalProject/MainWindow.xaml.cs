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
        Controller myController;
       

        public MainWindow()
        {
            // initialize window and controller
            InitializeComponent();
            myController = new Controller();
            DataContext = this.myController;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // update UI elements with selected object
            Weather selectedWeather = (Weather)locations.SelectedItem;
            if (selectedWeather != null)
            {
                updateWeatherDisplay(selectedWeather.CityName + " " + selectedWeather.Region);
            }
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        { 
            // update UI elements
            updateWeatherDisplay(newLocationTextBox.Text); 
        }

        private void newLocationButton_Click(object sender, RoutedEventArgs e)
        {
            // Show box to enter new location
            NewLocationBox.Visibility = Visibility.Visible;
        }

        private void addLocationButton_Click(object sender, RoutedEventArgs e)
        {
            // update UI elements
            SubmitButton_Click(sender, e);
        }

        
        


        private void setBackground(Weather w)
        {
            // Create gradient background
            var gradientBrush = new LinearGradientBrush();
            gradientBrush.StartPoint = new Point(0, 0);
            gradientBrush.EndPoint = new Point(0, 1);

            if (w.IsDay == 0)
            {
                // set dark background if it is night
                gradientBrush.GradientStops.Add(new GradientStop(Color.FromRgb(44, 44, 68), 0));
                gradientBrush.GradientStops.Add(new GradientStop(Color.FromRgb(128, 128, 128), 1));
            }

            else
            {
                // set light background if it is day
                gradientBrush.GradientStops.Add(new GradientStop(Color.FromRgb(49, 113, 175), 0));
                gradientBrush.GradientStops.Add(new GradientStop(Color.FromRgb(128, 128, 128), 1));
            }

            grid.Background = gradientBrush;

            
        }

        private void addToFavoritesButton_Click(object sender, RoutedEventArgs e)
        {
            // add weather object
            myController.AddWeather(myController.Weather);

            // Re establish binding so that UI udpdates
            locations.SetBinding(ListBox.ItemsSourceProperty, new Binding("favorites") { Source = myController });
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // save application state
            myController.SerializeData();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // restore application state
            myController.DeserializeData();
            // restablish bindings 
            locations.SetBinding(ListBox.ItemsSourceProperty, new Binding("favorites") { Source = myController });
        }


        private async void updateWeatherDisplay(string location)
        {

            // create new object 
            Weather currentWeather = new Weather();
            ForecastWeather forecastWeather = new ForecastWeather();
            currentWeather.Forecast = forecastWeather;
            
            // update controller class 
            myController.Weather = currentWeather;
            myController.Weather.ZipCode = newLocationTextBox.Text;

            // try to retrieve updated data 
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


            
            // update UI elements 
            CityTextBlock.Text = $"{myController.Weather.CityName}, {myController.Weather.Region}";
            ConditionTextBlock.Text = myController.Weather.WeatherCondition;
            TempTextBlock.Text = $"{myController.Weather.TempF} °F";

            weather_icon.Source = myController.Weather.Bitmap;


            

            Day1Text.Text = $"{myController.Weather.Forecast.Days[0].day.maxtemp_f} °F";
            Day2Text.Text = $"{myController.Weather.Forecast.Days[1].day.maxtemp_f} °F";
            Day3Text.Text = $"{myController.Weather.Forecast.Days[2].day.maxtemp_f} °F";


            

            Day1Icon.Source = myController.Weather.Forecast.Icons[0];
            Day2Icon.Source = myController.Weather.Forecast.Icons[1];
            Day3Icon.Source = myController.Weather.Forecast.Icons[2];

            

            Date1.Text = myController.Weather.Forecast.formattedDate(myController.Weather.Forecast.Days[0]);
            Date2.Text = myController.Weather.Forecast.formattedDate(myController.Weather.Forecast.Days[1]);
            Date3.Text = myController.Weather.Forecast.formattedDate(myController.Weather.Forecast.Days[2]);



            


            feelslikeText.Text = $"Feels Like: {myController.Weather.Feelslike_temp} °F";
            windText.Text = $"Wind: {myController.Weather.Wind} MPH";
            uvText.Text = $"UV Index: {myController.Weather.UV}";

            

            sunriseText.Text = $"Sunrise: {myController.Weather.Astronomy.Sunrise}";
            sunsetText.Text = $"Sunset: {myController.Weather.Astronomy.Sunset}";
            moonPhaseText.Text = $"Moon Phase: {myController.Weather.Astronomy.MoonPhase}";


            setBackground(myController.Weather);



            ForecastBorder.Visibility = Visibility.Visible;
            newLocationTextBox.Text = " ";
            NewLocationBox.Visibility = Visibility.Collapsed;
        }

        private void deleteLocationButton_Click(object sender, RoutedEventArgs e)
        {
            // Delete selected weather object 
            Weather selectedWeather = (Weather)locations.SelectedItem;
            myController.DeleteWeather(selectedWeather);
            
        }
   

    }
}
