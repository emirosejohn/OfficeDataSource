﻿using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Http;
using System.Web.Routing;
using OfficeLocationMicroservice.DependencyManagement;
using OfficeLocationMicroservice.WebUi.Helpers;
using WebUI;

namespace OfficeLocationMicroservice.WebUi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var websiteSettings = new WebSiteSettings();
            DependencyManager.BootstrapForSystem("Web Site", websiteSettings, websiteSettings);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
