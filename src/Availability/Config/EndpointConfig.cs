using System;
using System.IO;
using System.Linq;
using Availability.Data.Context;
using NServiceBus;
using NServiceBus.Logging;
using ILog = Common.Logging.ILog;
using LogManager = Common.Logging.LogManager;

namespace Availability.Config
{
	[EndpointName("HotelManagementSystem.Availability")]
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Server
	{
		private static readonly ILog Log = LogManager.GetLogger<EndpointConfig>();

		public EndpointConfig()
		{
			NServiceBus.Logging.LogManager.Use<DefaultFactory>();

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

			var licensePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "License.xml");
			endpointConfiguration.LicensePath(licensePath);
			endpointConfiguration.UseSerialization<JsonSerializer>();
			endpointConfiguration.Recoverability().Delayed(c => c.NumberOfRetries(0));
			endpointConfiguration.UseContainer<AutofacBuilder>(c => c.ExistingLifetimeScope(container));
			endpointConfiguration.UseTransport<MsmqTransport>()
				.ConnectionString("deadLetter=false;journal=false");
			//endpointConfiguration.UsePersistence<NHibernatePersistence>()
			//    .ConnectionString(ConfigurationManager.ConnectionStrings["HotelManagementSystem.Availability"].ToString());
			endpointConfiguration.UsePersistence<InMemoryPersistence>();

			endpointConfiguration.SendFailedMessagesTo("error");
			endpointConfiguration.AuditProcessedMessagesTo("audit");

			var conventions = endpointConfiguration.Conventions();
			conventions.DefiningCommandsAs(t => t.Namespace != null && t.Namespace.StartsWith("HotelManagementSystem") && t.Namespace.EndsWith("Commands") && t.Name.EndsWith("Command"));
			conventions.DefiningEventsAs(t => t.Namespace != null && t.Namespace.StartsWith("HotelManagementSystem") && t.Namespace.EndsWith("Events") && t.Name.EndsWith("Event"));

			endpointConfiguration.EnableInstallers();
		}
	}
}