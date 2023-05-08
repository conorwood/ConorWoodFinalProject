using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConorWoodFinalProject
{
    /// <summary>
    /// Represents astronomy objects and provides methods to retrieve data
    /// </summary>
    [Serializable]
    public class AstronomyInfo
    {

        private string key;
        private AstronomyResponse astronomyResponse;
        [NonSerialized]
        HttpClient client;
        private string sunrise;
        private string sunset;
        private string moon_phase;

        public string Sunrise
        {
            get
            {
                return sunrise;
            }
        }

        public string Sunset
        {
            get
            {
                return sunset;
            }
        }

        public string MoonPhase
        {
            get
            {
                return moon_phase;
            }
        }

        public AstronomyInfo() 
        {
            astronomyResponse = new AstronomyResponse();
            client = new();
            key = "b06f9ec5e918401889a152444230604";
        }


        /// <summary>
        /// Uses HTTP Client to make an API call to astronomy endpoint. 
        /// Parses JSON object and populates class members
        /// </summary>
        /// <param name="location"> Location to retrieve astronomy data for </param>
        /// <returns> Task to await in calling function </returns>
        public async Task getAstroInfo(string location)
        {
            
            string? dt = getCurrentDate();

            HttpClient client = new();

            // call and endpoint, parse JSON
            var json = await client.GetStringAsync($"http://api.weatherapi.com/v1/astronomy.json?key={this.key}&q={location}&dt={dt}");
            astronomyResponse = JsonSerializer.Deserialize<AstronomyResponse?>(json);

            Astro astro = astronomyResponse.astronomy.astro;

            // set object properties
            this.sunrise = astro.sunrise;
            this.sunset = astro.sunset;
            this.moon_phase = astro.moon_phase;

        }


        /// <summary>
        /// Format the current date into the proper format to use in endpoint call 
        /// </summary>
        /// <returns> Correctly formatted string </returns>
        private static string getCurrentDate()
        {
            DateTime currentDate = DateTime.Now;
            return currentDate.ToString("yyyy-MM-dd");
        }
    }

}
