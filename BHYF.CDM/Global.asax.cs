using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using BHYF.CDM.Models;
using Autofac;

namespace BHYF.CDM
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var config = GlobalConfiguration.Configuration;
            var builder = new ContainerBuilder();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Register(builder);
            Database.SetInitializer<YwtDbContext>(null);
           // builder.RegisterType<YwtDbContext>();
        }

        private static void Register(ContainerBuilder builder)
        {
            MapperConfig.Configure();
        }
    }
}
