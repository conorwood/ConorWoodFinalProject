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
    [Serializable]
    class Controller
    {
        public ObservableCollection<Weather> favorites { get; set; }

        public Controller()
        {
            favorites = new ObservableCollection<Weather>();
        }


        public void AddWeather(Weather weather)
        {
            favorites.Add(weather);
            favorites = new ObservableCollection<Weather>(favorites.OrderBy(x => x.CityName));
        }

        public void DeleteWeather(Weather weather)
        {
            favorites.Remove(weather);
        }

        public void SerializeData ()
        {
            string filePath = "favorites.dat";
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, this);
            }
        }


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
