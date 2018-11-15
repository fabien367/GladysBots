using Gladys.WebService.Services.IServices;
using Gladys.WebService.Services.Services;
using Gladys.WebServices.Models.TableProcess;
using System.Web.Mvc;

namespace Gladys.WebService.Controllers
{
    public class ProcessController : Controller
    {
        // GET: Process
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetDatas(TableProcessModel table)
        {
            IDataTableService dataTableService = new DataTableService();
            var result = dataTableService.GetData(table.TableName);
            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}