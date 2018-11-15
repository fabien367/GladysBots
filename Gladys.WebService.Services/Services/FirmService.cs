using Gladys.WebService.Services.IServices;
using Gladys.WebServices.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gladys.WebService.Services.Services
{
   public class FirmService: IFirmService
    {
        public IQueryable<Firm> GetFirms()
        {
            Context c = new Context();
            
              return  c.Firms.AsQueryable();
            
        }
    }
}
