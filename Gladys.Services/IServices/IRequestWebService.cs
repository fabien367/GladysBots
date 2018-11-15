using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gladys.Services.IServices
{
    public interface IRequestWebService
    {
        /// <summary>
        /// Envoi une demande Post
        /// </summary>
        /// <param name="path">URL</param>
        /// <param name="content">Paramètre</param>
        /// <returns></returns>
        Task<string> RequestAsync(string content);
    }
}
