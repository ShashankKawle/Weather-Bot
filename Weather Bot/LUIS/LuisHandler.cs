using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Weather_Bot.Functions.Weather;

namespace Weather_Bot.LUIS
{

    [LuisModel("c5782777-be74-4e47-a40d-eefee342b7f3", "8eb9bdfa3cdd47efa9361cd4e1ea1c0b")]
    [Serializable]
    public class LuisHandler : LuisDialog<object>
    {
        static bool ConEstablished = false;

        [LuisIntent("Weather")]
        public async Task GetWeatherData(IDialogContext context, LuisResult result)
        {

            var city = result.Entities[0].Entity;
            if (ConEstablished == false)
            {
                WeatherHandler.CreateConnection();
                ConEstablished = true;
            }
            await WeatherHandler.GetWeatherDataAsync(city);
            await context.PostAsync(WeatherHandler.GenerateResponse());
        }
    }
}