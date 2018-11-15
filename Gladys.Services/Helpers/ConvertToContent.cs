using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Gladys.Services.Helpers
{
    public static class ConvertToContent
    {
        public static StringContent ToStringContent(this object obj)
        {
            var reponse = JsonConvert.SerializeObject(obj);
            return new StringContent(reponse, Encoding.UTF8, "application/json");
        }
    }
}
