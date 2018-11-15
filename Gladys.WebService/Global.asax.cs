using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Caching;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Gladys.WebService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            //if (System.Web.HttpContext.Current.Request.Headers["username"].Any())
            //{
            //    //System.Security.Principal.WindowsIdentity identity = (System.Security.Principal.WindowsIdentity)this.Context.User.Identity;
            //    var user = HttpContext.Current.Request.Headers["username"];
            //    this.Context.Cache.Add(user, "test", null, DateTime.MaxValue, new TimeSpan(0, 20, 0), CacheItemPriority.Normal, null);
            //    try
            //    {
            //        var test = new WindowsIdentity(user);
            //    }
            //    catch (Exception ex)
            //    {

            //        throw;
            //    }
            //    var identity = new WindowsIdentity(user);

            //    WindowsIdentity MyIdentity = WindowsIdentity.GetCurrent();

            //    //var principal = new Principal(identity);
            //    //Thread.CurrentPrincipal = principal;
            //    //if (HttpContext.Current != null)
            //    //{
            //    //    HttpContext.Current.User = principal;
            //    //}
                
            //    //Thread.CurrentPrincipal = principal;
            //    //this.Context.Cache.Remove(user);
            //    // Test l'existence d'un user en cache
            //    if (this.Context.Cache[user] == null)
            //    {
            //        // récupération des informations de sécurité en base (Profil)
            //        //CustomPrincipal principal = AuthentificationManager.CreateCustomPrincipal(identity);

            //        //   et  Mise en cache des informations de sécurité
            //        //Context.Cache.Add(identity.Name, principal, null, DateTime.MaxValue, new TimeSpan(0, 20, 0), CacheItemPriority.Normal, null);
            //    }
            //}
        }
    }
}
