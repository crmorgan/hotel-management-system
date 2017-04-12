using Autofac;
using NServiceBus.Logging;
using Reservations.Data.Context;

namespace Reservations.Config
{
	public class ContainerSetup
	{
		private static readonly ILog Log = LogManager.GetLogger<ContainerSetup>();

		public static IContainer Create()
		{
			Log.Info("Initializing dependency injection...");

			var builder = new ContainerBuilder();
			builder.RegisterType<ReservationsContext>().As<IReservationsContext>();

			return builder.Build();
		}
	}
}