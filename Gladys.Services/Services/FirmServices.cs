using Gladys.Models.Firms;
using Gladys.Services.IServices;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gladys.Services.Services
{
    public class FirmServices : ConnectWebService, IFirmServices
    {
        public FirmServices() : base() { }
        public async Task<List<Firm>> GetFirms()
        {
            var result = await _client.GetAsync(_uri + @"Firm\GetFirms");
            if (!result.IsSuccessStatusCode)
            { }
            var firms = await result.Content.ReadAsStringAsync();
           return JsonConvert.DeserializeObject<List<Firm>>(firms);
        }
    }
}
