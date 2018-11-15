﻿using Gladys.WebServices.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gladys.WebService.Services.IServices
{
   public interface IFirmService
    {
        IQueryable<Firm> GetFirms();
    }
}
