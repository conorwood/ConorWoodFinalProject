using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ConorWoodFinalProject
{
    /// <summary>
    /// Controller to implement MVC Architecture. 
    /// Contains data for current weather 
    /// </summary>
    [Serializable]
    class Controller
    {
        public ObservableCollection<Weather> favorites { get; set; }
        private Weather weather;

        public Controller()
        {
            favorites = new ObservableCollection<Weather>();
            weather = new Weather();
        }

        public Weather Weather
        {
            get
            {
                return weather;
            }

            set { weather = value; }
        }

        public bool CheckWeatherInFavorites(Weather weather)
        {
            foreach (var v in favorites)
            {
                if (v.CityName == weather.CityName && v.Region == weather.Region)
                {
                    return true; 
                }
            }

            return false;
        }

        /// <summary>
        /// Adds a weather object to the ObservableCollection that is binded to the UI ListBox
        /// Sorts collection by alphabetical order 
        /// </summary>
        /// <param name="weather"> Weather object to add</param>
        public void AddWeather(Weather weather)
        {
            favorites.Add(weather);
            favorites = new ObservableCollection<Weather>(favorites.OrderBy(x => x.CityName));
        }

        /// <summary>
        /// Removes a weather object from the ObservableCollection that is binded to the UI ListBox
        /// </summary>
        /// <param name="weather"> Weather object to remove </param>
        public void DeleteWeather(Weather weather)
        {
            favorites.Remove(weather);
        }


        /// <summary>
        /// Serializes the class state so that state persists when application is closed and opened 
        /// </summary>
        public void SerializeData ()
        {
            string filePath = "favorites.dat";
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, this);
            }
        }


        /// <summary>
        /// Deserializes the class state so that state persists when application is closed and opened 
        /// </summary>
        public void DeserializeData ()
        {
            string filePath = "favorites.dat";
            if (File.Exists(filePath))
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    Controller newController = binaryFormatter.Deserialize(fileStream) as Controller;
                    if (favorites != null)
                    {
                        this.favorites = newController.favorites;
                    }
                }
            }
        }

    }
}
