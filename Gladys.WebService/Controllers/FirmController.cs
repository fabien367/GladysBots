using Gladys.WebService.Services.IServices;
using Gladys.WebService.Services.Services;
using System.Linq;
using System.Web.Mvc;

namespace Gladys.WebService.Controllers
{
    public class FirmController : Controller
    {
        // GET: Firm
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetFirms()
        {
            IFirmService fs = new FirmService();
            var resultFirms = fs.GetFirms().ToList();
            return new JsonResult { Data = resultFirms, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}