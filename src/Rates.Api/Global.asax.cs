using System.Web;
using System.Web.Http;
using RoomOccupancy.Api;

namespace Rates.Api
{
	public class WebApiApplication : HttpApplication
	{
		protected void Application_Start()
		{
			GlobalConfiguration.Configure(WebApiConfig.Register);
		}
	}
}