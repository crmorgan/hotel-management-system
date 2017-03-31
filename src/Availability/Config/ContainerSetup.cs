using Autofac;
using Availability.Data.Context;
using Common.Logging;

namespace Availability.Config
{
	internal class ContainerSetup
	{
		private static readonly ILog Log = LogManager.GetLogger<ContainerSetup>();

		public static IContainer Create()
		{
			Log.Info("Initializing dependency injection...");

			var builder = new ContainerBuilder();
			builder.RegisterType<RoomAvailabilityContext>().As<IRoomAvailabilityContext>();

			return builder.Build();
		}
	}
}