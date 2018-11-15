using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web;
using System.Linq;

namespace Gladys.WebService.App_Start
{
    public class GladysToken : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (HttpContext.Current.Request.Headers["username"].Any())
            {
                var user = HttpContext.Current.Request.Headers["username"];
                // Je vérifie si l'appel au WebService vient de l'app
                if (HttpContext.Current.Request.Headers["Authorization"].Any())
                {
                    var idApp = HttpContext.Current.Request.Headers["Authorization"];
                    CryptDecrypt c = new CryptDecrypt();
                    var idAppDecrypted = c.DataDencrypted(idApp.Replace("Basic",String.Empty));
                    if (idAppDecrypted==TokenConstants.AppId)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
        
    }
}