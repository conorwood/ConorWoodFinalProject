using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace ConorWoodFinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Weather weather;
        //ForecastWeather forecastWeather;
        List<Weather> favorites = new List<Weather>();

        public MainWindow()
        {
            InitializeComponent();
            //currentWeather = new Weather();
            //forecastWeather = new ForecastWeather();
            //currentWeather.Forecast = forecastWeather;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (locations.SelectedIndex== 0) 
            {
                return;
            }
            Weather selectedWeather = (Weather)locations.SelectedItem;
            updateCurrnetWeather(selectedWeather);
            //MessageBox.Show($"{selectedWeather.CityName}\n{selectedWeather.TempF}\n{selectedWeather.WeatherCondition}");
            //currentWeather = (Weather)locations.SelectedItem;
            //MessageBox.Show(currentWeather.CityName);
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {

            //Weather currentWeather = new Weather();

            //await currentWeather.getWeatherInfo(ZipCodeTextBox.Text);
            //await forecastWeather.getWeatherInfo(ZipCodeTextBox.Text);

            Weather currentWeather = new Weather();
            ForecastWeather forecastWeather = new ForecastWeather();
            currentWeather.Forecast = forecastWeather;
            weather = currentWeather;
            weather.ZipCode = newLocationTextBox.Text;


            await currentWeather.getWeatherInfo(newLocationTextBox.Text);
            // await forecastWeather.getWeatherInfo(newLocationTextBox.Text);
            await currentWeather.Forecast.getWeatherInfo(newLocationTextBox.Text);
            

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


            setBackground(currentWeather);


            ForecastBorder.Visibility = Visibility.Visible;
            newLocationTextBox.Text = " ";
            NewLocationBox.Visibility = Visibility.Collapsed;
        }

        private void newLocationButton_Click(object sender, RoutedEventArgs e)
        {
            NewLocationBox.Visibility = Visibility.Visible;
        }

        private void addLocationButton_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(newLocationTextBox.Text);
            SubmitButton_Click(sender, e);
        }

        
        private async void updateCurrnetWeather(Weather newWeather)
        {
            //await newWeather.getWeatherInfo(newLocationTextBox.Text);

            await newWeather.getWeatherInfo(newWeather.ZipCode);
            await newWeather.Forecast.getWeatherInfo(newWeather.ZipCode);
            // await forecastWeather.getWeatherInfo(newLocationTextBox.Text);
            //await newWeather.Forecast.getWeatherInfo(newLocationTextBox.Text);


            CityTextBlock.Text = $"{newWeather.CityName}, {newWeather.Region}";
            ConditionTextBlock.Text = newWeather.WeatherCondition;
            TempTextBlock.Text = $"{newWeather.TempF} °F";
            //rounded_border.Background = Brushes.DarkGray;
            weather_icon.Source = newWeather.Bitmap;


            Day1Text.Text = $"{newWeather.Forecast.Days[0].day.maxtemp_f} °F";
            Day2Text.Text = $"{newWeather.Forecast.Days[1].day.maxtemp_f} °F";
            Day3Text.Text = $"{newWeather.Forecast.Days[2].day.maxtemp_f} °F";


            Day1Icon.Source = newWeather.Forecast.Icons[0];
            Day2Icon.Source = newWeather.Forecast.Icons[1];
            Day3Icon.Source = newWeather.Forecast.Icons[2];

            Date1.Text = newWeather.Forecast.formattedDate(newWeather.Forecast.Days[0]);
            Date2.Text = newWeather.Forecast.formattedDate(newWeather.Forecast.Days[1]);
            Date3.Text = newWeather.Forecast.formattedDate(newWeather.Forecast.Days[2]);

            feelslikeText.Text = $"Feels Like: {newWeather.Feelslike_temp} °F";
            windText.Text = $"Wind: {newWeather.Wind} MPH";
            uvText.Text = $"UV Index: {newWeather.UV}";

            setBackground(newWeather);

            ForecastBorder.Visibility = Visibility.Visible;
            newLocationTextBox.Text = " ";
            NewLocationBox.Visibility = Visibility.Collapsed;
        }


        private void setBackground(Weather w)
        {
            if (w.IsDay == 0)
            {
                grid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2c2c44"));
            }

            else
            {
                grid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3171af"));
            }
        }

        private void addToFavoritesButton_Click(object sender, RoutedEventArgs e)
        {
            
            favorites.Add(weather);
            locations.Items.Add(weather);
            //locations.Items.Clear();

            //foreach (var v in favorites)
            //{
            //    locations.Items.Add(v);
            //}
            

            
            //locations.Items.Add(currentWeather);
            //students.Add(student);
            //List<Person> displayList = students.OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();

            //StudentsListBox.Items.Clear();

            //foreach (var s in displayList)
            //{
            //    StudentsListBox.Items.Add(s);
            //}
        }

        


        //private void addStudent(Person student)
        //{
        //    student.FirstName = FirstNameTextBox.Text;
        //    student.LastName = LastNameTextBox.Text;
        //    student.ID = int.Parse(StudentIDTextBox.Text);
        //    student.Age = int.Parse(AgeTextBox.Text);
        //    student.Gender = getGender();
        //}

    }
}
