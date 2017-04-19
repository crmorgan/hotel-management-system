using System;
using System.Linq;
using Availability.Data.Context;
using NServiceBus;
using NServiceBus.Logging;


namespace Availability.Config
{
	[EndpointName("HMS.Availability")]
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Server
	{
		private static readonly ILog Log = LogManager.GetLogger<EndpointConfig>();

		public EndpointConfig()
		{
			LogManager.Use<DefaultFactory>();

			if (Environment.UserInteractive)
				Console.Title = "Availability";

			InitializeDatbase();
		}

		private void InitializeDatbase()
		{
			Log.Debug("Initializing database");

			var context = new RoomAvailabilityContext();
			var roomTypes = context.RoomTypeAvailability.ToList();

			Log.DebugFormat("Database initialized, first room type is {0}", roomTypes.First().Id);
		}

		public void Customize(EndpointConfiguration endpointConfiguration)
		{
			Log.Info("Customizing endpoint...");

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