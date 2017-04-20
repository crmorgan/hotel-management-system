using Autofac;
using Guests.Data.Context;
using NServiceBus.Logging;

namespace Guests.Config
{
	public class ContainerSetup
	{
		private static readonly ILog Log = LogManager.GetLogger<ContainerSetup>();

		public static IContainer Create()
		{
			Log.Info("Initializing dependency injection...");

			var builder = new ContainerBuilder();
			builder.RegisterType<GuestsContext>().As<IGuestsContext>();

			return builder.Build();
		}
	}
}