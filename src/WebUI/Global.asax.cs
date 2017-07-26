using System.Net.Http.Formatting;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Http;
using System.Web.Routing;
using OfficeLocationMicroservice.DependencyManagement;
using OfficeLocationMicroservice.WebUi.Helpers;
using Swashbuckle.Application;
using WebUI;

namespace OfficeLocationMicroservice.WebUi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var websiteSettings = new WebSiteSettings();
            DependencyManager.BootstrapForSystem("Web Site", websiteSettings, websiteSettings, websiteSettings, 
                websiteSettings);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            //            GlobalConfiguration.Configuration.Formatters.Clear();             
            //            GlobalConfiguration.Configuration.Formatters.Add(new JsonMediaTypeFormatter());
        }
    }
}
