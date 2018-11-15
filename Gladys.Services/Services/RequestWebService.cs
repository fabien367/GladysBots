using Gladys.Services.IServices;
using Gladys.Services.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gladys.Services.Services
{
    public class RequestWebService : ConnectWebService, IRequestWebService
    {
        public RequestWebService() : base() { }
        /// <summary>
        /// Envoi une demande Post
        /// </summary>
        /// <param name="path">URL</param>
        /// <param name="content">Paramètre</param>
        /// <returns></returns>
        public async Task<string> RequestAsync(string message)
        {
            try
            {
                var objDoc = JsonConvert.SerializeObject(message);
                var content = new StringContent(objDoc, Encoding.UTF8, "application/json");
                var reponse = await _client.PostAsync(_uri + "Speak/GetReponse", content);
                // On vérifie l'etat de la requete
                if (!reponse.IsSuccessStatusCode)
                {
                    LogService.WriteLog(reponse);
                }
                return
                    await reponse.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                LogService.WriteLog(ex);
                return string.Empty;
            }
        }


    }
}
