using Gladys.WebService.App_Start;
using Gladys.WebServices.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gladys.WebService.Controllers
{
    [GladysToken]
    public class OutlookController : Controller
    {
        // GET: Outlook
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Rendez-vous 
        /// </summary>
        /// <returns></returns>       
        public JsonResult GetAppointments()
        {
           
            return new JsonResult { Data = "ok",JsonRequestBehavior= JsonRequestBehavior.AllowGet };
        }
    }
}