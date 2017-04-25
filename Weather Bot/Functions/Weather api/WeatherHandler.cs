using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Weather_Bot.Functions.Weather_api
{
    public class WeatherHandler
    {
        static HttpClient client = new HttpClient();
        static WeatherData jsd;

        // Incomplete Function     [ UNDER DEVELOPMENT ]
        internal static void CreateConnection()
        {
            //Setting up the client.
            client.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/weather");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        internal static async Task GetWeatherDataAsync(string city)
        {
            string key = "022e08de60c87cfa277a2f2af769b9f1";
            
            //Sending the request and getting Response.
            HttpResponseMessage response = await client.GetAsync("?q=" + city + "&APPID=" + key);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                jsd = await response.Content.ReadAsAsync<WeatherData>();
            }
        }

        internal static String GenerateResponse()
        {
            String response = "Name = " + jsd.name + "\n Latitude = " + jsd.coord.lat + "\tLongitude = " + jsd.coord.lon;
            return response;
        }
    }
}