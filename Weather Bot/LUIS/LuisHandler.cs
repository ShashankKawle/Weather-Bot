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

    [LuisModel("LUIS App ID", "Subscription-key")]
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