using NServiceBus;
using NServiceBus.Logging;
using System;

namespace ITOps.Config
{
	[EndpointName("HMS.ITOps")]
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Server
	{
		private static readonly ILog Log = LogManager.GetLogger<EndpointConfig>();

		public EndpointConfig()
		{
			LogManager.Use<DefaultFactory>();

			if (Environment.UserInteractive)
				Console.Title = "HMS.ITOps";
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

			//var routing = transport.Routing();
			//routing.RouteToEndpoint(typeof(CreditCardHoldPlacedEvent), "HMS.ServiceName");
		}
	}
}
