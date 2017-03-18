using Autofac;
using Common.Logging;
using RoomOccupancy.Data.Context;

namespace RoomOccupancy.Config
{
	internal class ContainerSetup
	{
		private static readonly ILog Log = LogManager.GetLogger<ContainerSetup>();

		public static IContainer Create()
		{
			Log.Info("Initializing dependency injection...");

			var builder = new ContainerBuilder();
			builder.RegisterType<RoomOccupancyContext>().As<IRoomOccupancyContext>();

			return builder.Build();
		}
	}
}