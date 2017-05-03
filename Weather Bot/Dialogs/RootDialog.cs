using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Weather_Bot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            InitializeConnection();
            context.Wait(MessageReceivedAsync);
        }

        private void InitializeConnection()
        {
            Functions.Weather_api.WeatherHandler.CreateConnection();
            Functions.Weather_api.LUISHandler.ConnectLUIS();
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            var query = activity.Text.ToString();
            var city = await Functions.Weather_api.LUISHandler.GetCityNameAsync(query);
            await Functions.Weather_api.WeatherHandler.GetWeatherDataAsync(city);
            string res = Functions.Weather_api.WeatherHandler.GenerateResponse();
            await context.PostAsync(res);
            context.Wait(MessageReceivedAsync);
        }
    }
}