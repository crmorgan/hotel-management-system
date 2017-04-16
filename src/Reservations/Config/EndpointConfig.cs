using System;
using System.Linq;
using NServiceBus;
using NServiceBus.Logging;
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
			endpointConfiguration.UseTransport<MsmqTransport>()
				.ConnectionString("deadLetter=false;journal=false");
			endpointConfiguration.UsePersistence<InMemoryPersistence>();

			endpointConfiguration.SendFailedMessagesTo("error");
			endpointConfiguration.AuditProcessedMessagesTo("audit");

			var conventions = endpointConfiguration.Conventions();
			conventions.DefiningCommandsAs(
				t =>
					t.Namespace != null && t.Namespace.StartsWith("Reservations") && t.Namespace.EndsWith("Commands") &&
					t.Name.EndsWith("Command"));

			conventions.DefiningEventsAs(
				t =>
					t.Namespace != null && t.Namespace.StartsWith("Reservations") && t.Namespace.EndsWith("Events") &&
					t.Name.EndsWith("Event"));

			endpointConfiguration.EnableInstallers();
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