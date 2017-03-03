using CDM.Infrastructure.WebApi.ExceptionHandling;
using CDM.Infrastructure.WebApi.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;


namespace BHYF.CDM
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            var jsonFormatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            jsonFormatter.SerializerSettings = new JsonSerializerSettings
            {
                //PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                //DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Local,
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var converters = jsonFormatter.SerializerSettings.Converters;
            converters.Add(new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-ddTHH:mm:sszzzz" });

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Filters.Add(new ValidateModelAttribute());
            config.Services.Replace(typeof(IExceptionLogger), new StemExceptionLogger());
            config.Services.Replace(typeof(IExceptionHandler), new StemExceptionHandler());
        }
    }
}
