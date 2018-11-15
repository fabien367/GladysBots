using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Galdys.WebServices.Luis
{
    public class LuisService : ILuisService
    {
        private string _urLLuis = "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/387955cd-e843-4324-b16c-74b765085b1c?subscription-key=8a999445c05c4b5eba61085a930c48df&verbose=true&timezoneOffset=0&q=";
        HttpClient _client;

        public LuisService()
        {
            _client = new HttpClient();
        }

        public async Task<LuisResult> SendRequest(string talk)
        {
            var reponse = await _client.GetAsync(_urLLuis + talk);
            var result = await reponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<LuisResult>(result);
            //return true;
        }
    }
}
