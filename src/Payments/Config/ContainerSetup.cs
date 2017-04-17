using Autofac;
using NServiceBus.Logging;
using Payments.Data.Context;

namespace Payments.Config
{
	public class ContainerSetup
	{
		private static readonly ILog Log = LogManager.GetLogger<ContainerSetup>();

		public static IContainer Create()
		{
			Log.Info("Initializing dependency injection...");

			var builder = new ContainerBuilder();
			builder.RegisterType<PaymentsContext>().As<IPaymentsContext>();

			return builder.Build();
		}
	}
}