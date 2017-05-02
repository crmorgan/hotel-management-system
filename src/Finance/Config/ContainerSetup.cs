using Autofac;
using Finance.Data.Context;
using NServiceBus.Logging;

namespace Finance.Config
{
	public class ContainerSetup
	{
		private static readonly ILog Log = LogManager.GetLogger<ContainerSetup>();

		public static IContainer Create()
		{
			Log.Info("Initializing dependency injection...");

			var builder = new ContainerBuilder();
			builder.RegisterType<FinanceContext>().As<IFinanceContext>();

			return builder.Build();
		}
	}
}