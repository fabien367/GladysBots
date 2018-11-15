using Gladys.JsonCreateur.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gladys.JsonCreateur
{
    class Program
    {
        static void Main(string[] args)
        {
            List<JsonModel> listModel = new List<JsonModel>();
            // Bonjour salut
            listModel.Add(new JsonModel { Intent = "Politesse", Reponse = "Bonjour" ,Action=ActionEnumModel.Reponse});
            // Identité
            listModel.Add(new JsonModel { Intent = "Identite", Reponse = "Je m'appelle Gladys", Action = ActionEnumModel.Reponse });
            // Meteo
            listModel.Add(new JsonModel { Intent = "Meteo", Reponse = "le temps est {0}, il fait {1} degrés", Action = ActionEnumModel.Reponse });

            string result = JsonConvert.SerializeObject(listModel);
            using (StreamWriter writer = File.CreateText(@"C:\Users\fabien.richard\source\repos\Gladys\Gladys.WebService\Json\responses.json"))
            {
                 writer.Write(result);
            }
        }
    }
}
