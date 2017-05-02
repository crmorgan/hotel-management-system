using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;
using Autofac;
using Autofac.Integration.WebApi;
using Finance.Data.Context;

namespace Finance.Api
{
	public class Global : System.Web.HttpApplication
	{

		protected void Application_Start()
		{
			var config = GlobalConfiguration.Configuration;
			var builder = new ContainerBuilder();

			builder.RegisterInstance(new FinanceContext())
				.As<IFinanceContext>()
				.SingleInstance();

			builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

			NServiceBusConfig.Configure(builder);

			var container = builder.Build();
			config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

			GlobalConfiguration.Configure(WebApiConfig.Register);
		}
	}
}