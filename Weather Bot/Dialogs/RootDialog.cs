using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Weather_Bot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            Functions.Weather_api.WeatherHandler.CreateConnection();
            context.Wait(MessageReceivedAsync);
            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            var city = activity.Text.ToString();
            await Functions.Weather_api.WeatherHandler.GetWeatherDataAsync(city);
            string res = Functions.Weather_api.WeatherHandler.GenerateResponse();
            await context.PostAsync(res);
            context.Wait(MessageReceivedAsync);
        }
    }
}