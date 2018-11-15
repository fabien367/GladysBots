using Gladys.Models.Firms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gladys.Services.IServices
{
   public interface IFirmServices
    {
        Task<List<Firm>> GetFirms();
    }
}
