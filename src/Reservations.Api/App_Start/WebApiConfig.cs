using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace Reservations.Api
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
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
				new {id = RouteParameter.Optional}
			);
		}
	}
}