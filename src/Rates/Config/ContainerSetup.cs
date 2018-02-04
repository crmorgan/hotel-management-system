using Autofac;
using NServiceBus.Logging;
using Rates.Data.Context;

namespace Rates.Config
{
	public class ContainerSetup
	{
		private static readonly ILog Log = LogManager.GetLogger<ContainerSetup>();

		public static IContainer Create()
		{
			Log.Info("Initializing dependency injection...");

			var builder = new ContainerBuilder();
			builder.RegisterType<RatesContext>().As<IRatesContext>();

			return builder.Build();
		}
	}
}