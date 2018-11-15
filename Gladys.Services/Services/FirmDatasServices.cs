using Gladys.Models.TableProcess;
using Gladys.Models.TemplateTable;
using Gladys.Services.IServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gladys.Services.Services
{
   public class FirmDatasServices: ConnectWebService, IFirmDatasServices
    {
        public FirmDatasServices():base()
        {

        }

        public async Task<List<TemplateTableModel>> GetDatas( string table)
        {
            TableProcessModel tableProcessModel = new TableProcessModel { TableName = table };
            var objDoc = JsonConvert.SerializeObject(tableProcessModel);
            var content = new StringContent(objDoc, Encoding.UTF8, "application/json");
            var reponse = await _client.PostAsync(_uri + "Process/GetDatas", content);
            // On vérifie l'etat de la requete
            if (!reponse.IsSuccessStatusCode)
            {
                LogService.WriteLog(reponse);
            }
            var result = await reponse.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<TemplateTableModel>>(result);
        }
    }
}
