using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galdys.WebServices.Luis
{
    public interface ILuisService
    {
        Task<LuisResult> SendRequest(string talk);
    }
}
