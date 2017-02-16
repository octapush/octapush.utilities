using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace octapush.SPEditor
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config
                .Routes
                .MapHttpRoute(
                    "ApplicationApi",
                    "api/{controller}/{id}",
                    new
                    {
                        controller = "application",
                        id = RouteParameter.Optional
                    }
                );

            config
                .Routes
                .MapHttpRoute(
                    "QueryApi",
                    "api/{controller}/{appId}/{id}",
                    new
                    {
                        controller = "query",
                        appId = RouteParameter.Optional,
                        id = RouteParameter.Optional
                    }
                );

            config
                .Formatters
                .XmlFormatter
                .SupportedMediaTypes
                .Remove(
                    config
                        .Formatters
                        .XmlFormatter
                        .SupportedMediaTypes
                        .FirstOrDefault(t => t.MediaType == "application/xml")
                );
        }
    }
}