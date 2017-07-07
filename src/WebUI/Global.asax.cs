using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Http;
using System.Web.Routing;
using DependencyManagement;
using OfficeLocationMicroservice.WebUi.Helpers;
using WebUI;

namespace OfficeLocationMicroservice.WebUi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            DependencyManager.BootstrapForSystem("Web Site", new WebSiteSettings());

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
