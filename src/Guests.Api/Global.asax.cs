using System;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Guests.Data.Context;

namespace Guests.Api
{
	public class Global : HttpApplication
	{
		protected void Application_Start(object sender, EventArgs e)
		{
			var config = GlobalConfiguration.Configuration;
			var builder = new ContainerBuilder();

			builder.RegisterInstance(new GuestsContext())
				.As<IGuestsContext>()
				.SingleInstance();

			builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

			NServiceBusConfig.Configure(builder);

			var container = builder.Build();
			config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

			GlobalConfiguration.Configure(WebApiConfig.Register);
		}
	}
}