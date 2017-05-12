using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Weather_Bot.Functions.Weather
{
    public class WeatherHandler
    {
        static HttpClient client;
        static WeatherData jsd;

        internal static void CreateConnection()
        {
            //Setting up the client.
            client = new HttpClient();
            client.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/weather");
        }

        internal static async Task GetWeatherDataAsync(string city)
        {
            string key = "Your weather api key";

            //Sending the request and getting Response.
            HttpResponseMessage response = await client.GetAsync("?q=" + city + "&APPID=" + key);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                jsd = await response.Content.ReadAsAsync<WeatherData>();
            }
        }

        internal static String GenerateResponse()
        {
            String response = "Name = " + jsd.name + "\n Tempreture = " + jsd.main.temp + "\tPressure = " + jsd.main.pressure + "\tHumidity = " + jsd.main.humidity; ;
            return response;
        }
    }
}