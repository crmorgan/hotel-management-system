using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Rates.Api
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			var cors = new EnableCorsAttribute("http://localhost:9000", "*", "*");
			config.EnableCors(cors);

			config.Formatters.Clear();
			config.Formatters.Add(new JsonMediaTypeFormatter());

			config.Formatters
				.JsonFormatter
				.SerializerSettings
				.ContractResolver = new CamelCasePropertyNamesContractResolver();

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				"DefaultApi",
				"api/{controller}/{id}",
				new { id = RouteParameter.Optional }
			);
		}
	}
}