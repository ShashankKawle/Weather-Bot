using Newtonsoft.Json;
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
    public class LUISHandler
    {
        static Rootobject luisData = new Rootobject();
        static HttpClient client2;
        static string luisUri = @"https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/c5782777-be74-4e47-a40d-eefee342b7f3?subscription-key=8eb9bdfa3cdd47efa9361cd4e1ea1c0b&timezoneOffset=0&verbose=true&q=";

        internal static void ConnectLUIS()
        {
            client2 = new HttpClient();
        }

        internal static async Task<string> GetCityNameAsync(string query)
        {
            luisUri = luisUri + query;
            HttpResponseMessage msg = await client2.GetAsync(luisUri);
            if(msg.IsSuccessStatusCode)
            {
                luisData = await msg.Content.ReadAsAsync<Rootobject>();
            }
            return luisData.entities[0].entity;
        }

        
    }
}