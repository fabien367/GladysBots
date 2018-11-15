using Gladys.Models.UserModel;
using Gladys.Services.IServices;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gladys.Services.Services
{
    public class ConnectWebService : IConnectWebService, ILogService
    {
        protected readonly HttpClient _client;
        private readonly ILogService _logService;
#if DEBUG
        //protected readonly string _uri = "http://localhost:55493/";
        protected readonly string _uri = "http://gladyshackathon.azurewebsites.net/";
#else
        private readonly string _uri = "http://localhost:55493/";
#endif
        public ConnectWebService()
        {
            _client = new HttpClient();
            _logService = new LogService();
        }
        public ConnectWebService(User user)
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", user.Token);
            _client.DefaultRequestHeaders.Add("User-Agent", "Gladys");
            _client.DefaultRequestHeaders.Add("First", user.FirstName);
        }

    }
}
