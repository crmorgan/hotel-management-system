using Guests.Data.Context;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Linq;

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
			var context = new GuestsContext();
			var count = context.Guests.Count();
			Log.InfoFormat($"Guest database initialized and has {count} guest records");
		}

		public void Customize(EndpointConfiguration endpointConfiguration)
		{
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