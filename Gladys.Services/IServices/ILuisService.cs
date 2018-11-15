using Gladys.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gladys.Services.IServices
{
   public interface ILuisService
    {
        Task<LuisResult> SendRequest(string talk);
    }
}
