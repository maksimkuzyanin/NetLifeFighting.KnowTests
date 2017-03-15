using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace NetLifeFighting.KnowTests.Web
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{

			config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

			// Web API routes
			config.MapHttpAttributeRoutes();

			/*config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);*/
		}
	}
}
