using Galdys.WebServices.Luis;
using Gladys.ModelsCommon;
using Gladys.WebService.App_Start;
using Gladys.WebService.Helpers;
using Gladys.WebServices.Models;
using Gladys.WebServices.Models.JsonModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Gladys.WebService.Controllers
{
    public class SpeakController : Controller
    {
        private readonly string _urLLuis = "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/387955cd-e843-4324-b16c-74b765085b1c?subscription-key=8a999445c05c4b5eba61085a930c48df&verbose=true&timezoneOffset=0&q=";
        HttpClient _client;
        // GET: Speak
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Demande à luis la comprehension
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public async Task<JsonResult> GetReponse(string result)
        {
            var actions = GetJsonModels();
            //UserConnect.GetContext(HttpContext);
            LuisService luisService = new LuisService();
            var resultLuis = await luisService.SendRequest(result);
            var resultRequest = actions.Where(a => a.Intent == resultLuis.TopScoringIntent.Intent).FirstOrDefault();
            return new JsonResult() { Data = "Test", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public async Task<JsonResult> GetResponseGladysAsync(SpeechModel speech)
        {
            var reponse = await _client.GetAsync(_urLLuis + speech.Text);
            var result = await reponse.Content.ReadAsStringAsync();
            var resultObject = JsonConvert.DeserializeObject<WebServices.Models.LuisResult>(result);

            return new JsonResult();
        }
        public async Task<JsonResult> GetTestAsync()
        {
            _client = new HttpClient();
            var reponse = await _client.GetAsync(_urLLuis + "il s'appelle franck");
            var result = await reponse.Content.ReadAsStringAsync();
            var resultObject = JsonConvert.DeserializeObject<WebServices.Models.LuisResult>(result);
            ChoiceAction choiceAction = new ChoiceAction(GetJsonModels());
            choiceAction.Process(resultObject);
            return new JsonResult() { Data = resultObject, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        private List<JsonModel> GetJsonModels()
        {
            var file = Server.MapPath(@"~/Json/");
            using (var reader = new System.IO.StreamReader(file + "responses.json"))
            {
                var json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<JsonModel>>(json);
            }
        }
    }
}