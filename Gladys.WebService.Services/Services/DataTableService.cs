using Gladys.WebService.Services.IServices;
using Gladys.WebServices.DB;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Gladys.WebService.Services.Services
{
    public class DataTableService : IDataTableService
    {
        public List<TemplateTable> GetData(string table)
        {
            Context c = new Context();
            var sql = @"Exec ReadTable @TableName";
            var result = c.Database.SqlQuery<TemplateTable>(
                 sql,
                 new SqlParameter("@TableName", table)).ToList();
            return result;
        }
    }
}
