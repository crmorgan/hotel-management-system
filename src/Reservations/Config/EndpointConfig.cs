using System;
using System.Linq;
using NServiceBus;
using NServiceBus.Logging;
using Payments.Messages.Events;
using Reservations.Data.Context;

namespace Reservations.Config
{
	[EndpointName("HMS.Reservations")]
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Server
	{
		private static readonly ILog Log = LogManager.GetLogger<EndpointConfig>();

		public EndpointConfig()
		{
			LogManager.Use<DefaultFactory>();

			if (Environment.UserInteractive)
				Console.Title = "HMS.Reservations";

			InitializeDatbase();
		}

		public void Customize(EndpointConfiguration endpointConfiguration)
		{
			Log.Info("Customize...");

			var container = ContainerSetup.Create();

			endpointConfiguration.UseSerialization<JsonSerializer>();
			endpointConfiguration.Recoverability().Delayed(c => c.NumberOfRetries(0));
			endpointConfiguration.UseContainer<AutofacBuilder>(c => c.ExistingLifetimeScope(container));
			endpointConfiguration.UsePersistence<InMemoryPersistence>();
			endpointConfiguration.SendFailedMessagesTo("error");
			endpointConfiguration.AuditProcessedMessagesTo("audit");
			endpointConfiguration.EnableInstallers();

			var transport = endpointConfiguration.UseTransport<MsmqTransport>()
				.ConnectionString("deadLetter=false;journal=false");

			var routing = transport.Routing();
			routing.RegisterPublisher(typeof(PaymentMethodSubmittedEvent), "HMS.Payments");


		}

		private void InitializeDatbase()
		{
			Log.Info("Initializing database");

			var context = new ReservationsContext();
			var reservations = context.Reservations.ToList();

			Log.InfoFormat("Database initialized, first reservation is {0}", reservations.First().Uuid);
		}
	}
}