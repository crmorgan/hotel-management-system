using System;
using System.Linq;
using Guests.Data.Context;
using NServiceBus;
using NServiceBus.Logging;

namespace Guests.Config
{
	[EndpointName("HMS.Guests")]
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Server
	{
		private static readonly ILog Log = LogManager.GetLogger<EndpointConfig>();

		public EndpointConfig()
		{
			LogManager.Use<DefaultFactory>();

			if (Environment.UserInteractive)
				Console.Title = "HMS.Guests";

			InitializeDatbase();
		}

		private void InitializeDatbase()
		{
			Log.Info("Initializing database");

			var context = new GuestsContext();
			var count = context.Guests.Count();

			Log.InfoFormat($"Database initialized with {count} guests");
		}

		public void Customize(EndpointConfiguration endpointConfiguration)
		{
			Log.Info("Customize...");

			var container = ContainerSetup.Create();

			endpointConfiguration.UseSerialization<JsonSerializer>();
			endpointConfiguration.Recoverability().Delayed(c => c.NumberOfRetries(0));
			endpointConfiguration.UseContainer<AutofacBuilder>(c => c.ExistingLifetimeScope(container));
			endpointConfiguration.UseTransport<MsmqTransport>().ConnectionString("deadLetter=false;journal=false");
			endpointConfiguration.UsePersistence<InMemoryPersistence>();
			endpointConfiguration.SendFailedMessagesTo("error");
			endpointConfiguration.AuditProcessedMessagesTo("audit");
			endpointConfiguration.EnableInstallers();
		}

		
	}
}