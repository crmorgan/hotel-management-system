using NServiceBus;
using NServiceBus.Logging;

namespace Rates.Integration
{
	public class Bootstrapper : INeedInitialization
	{
		private static readonly ILog Log = LogManager.GetLogger<Bootstrapper>();

		// ReSharper disable once EmptyConstructor : NServiceBus wil create this class using Activator.CreateInstance() which requires ctor.
		public Bootstrapper() {}

		public void Customize(EndpointConfiguration configuration)
		{
			Log.Info("Customizing the IoC container");

			configuration.RegisterComponents(
				components =>
				{
					components.ConfigureComponent<IProvideReservationRateTotal>(
						() => new ReservationRateTotalProvider(),
						DependencyLifecycle.SingleInstance);
				});
		}
	}
}
