using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApiChrysallis
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            //Quitar el formateador XML y dejar el JSON.
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            //Quitar las referencias circulares

            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;

            // Configuración y servicios de API web

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
