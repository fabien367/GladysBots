using Gladys.Models.TemplateTable;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gladys.Services.IServices
{
   public interface IFirmDatasServices
    {
        Task<List<TemplateTableModel>> GetDatas(string table);
    }
}
