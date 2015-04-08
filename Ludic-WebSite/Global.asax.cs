using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Ludic_website {
  // Remarque : pour obtenir des instructions sur l'activation du mode classique IIS6 ou IIS7, 
  // visitez http://go.microsoft.com/?LinkId=9394801

  public class MvcApplication : System.Web.HttpApplication {
    protected void Application_Start() {
      AreaRegistration.RegisterAllAreas();

      WebApiConfig.Register(GlobalConfiguration.Configuration);
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);
      AuthConfig.RegisterAuth();
    }
    protected void Application_EndRequest(Object sender, EventArgs e) { 
     HttpContext context = HttpContext.Current;
     if (context.Response.Status.Substring(0,3).Equals("401")) {
        context.Response.ClearContent();
        throw new HttpException(401, "You are not authorised");
     } 
    }
  }
}