using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gladys.WebService.App_Start
{
    public class Authorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // Il a déjà etait connecté
            if (HttpContext.Current.Request.Headers["Authorization"].Any())
            {
                var token = HttpContext.Current.Request.Headers["Authorization"].Replace("Bearer", string.Empty).Replace(@"\", string.Empty);
                var userAgent = HttpContext.Current.Request.Headers["User-Agent"];
                var first = HttpContext.Current.Request.Headers["First"];
                //return true;
                return GenerationToken.IsTokenValid(token, first, userAgent);
            }
            else
            {
                return false;
            }

        }

    }
}