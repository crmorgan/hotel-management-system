using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;

namespace Reservations.Api
{
	public class Global : HttpApplication
	{
		protected void Application_Start()
		{
			var config = GlobalConfiguration.Configuration;
			var builder = new ContainerBuilder();

			//builder.RegisterInstance(new ReservationsContext())
			//	.As<IReservationsContext>()
			//	.SingleInstance();

			builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

			NServiceBusConfig.Configure(builder);

			var container = builder.Build();
			config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

			GlobalConfiguration.Configure(WebApiConfig.Register);
		}
	}
}