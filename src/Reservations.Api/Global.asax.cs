using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Reservations.Data.Context;

namespace Reservations.Api
{
	public class Global : HttpApplication
	{
		protected void Application_Start()
		{
			var config = GlobalConfiguration.Configuration;
			var builder = new ContainerBuilder();

			builder.RegisterInstance(new ReservationsContext())
				.As<IReservationsContext>()
				.SingleInstance();

			//builder.RegisterType<ReservationsContext>()
			//	.As<IReservationsContext>()
			//	.InstancePerRequest();

			builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

			NServiceBusConfig.Configure(builder);

			var container = builder.Build();
			config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

			GlobalConfiguration.Configure(WebApiConfig.Register);
		}
	}
}