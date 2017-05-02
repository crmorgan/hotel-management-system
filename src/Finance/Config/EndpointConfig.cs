using System;
using System.Linq;
using Finance.Data.Context;
using NServiceBus;
using NServiceBus.Logging;

namespace Finance.Config
{
	[EndpointName("HMS.Finance")]
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Server
	{
		private static readonly ILog Log = LogManager.GetLogger<EndpointConfig>();

		public EndpointConfig()
		{
			LogManager.Use<DefaultFactory>();

			if (Environment.UserInteractive)
				Console.Title = "HMS.Finance";

			InitializeDatbase();
		}

		private void InitializeDatbase()
		{
			Log.Info("Initializing database");

			var context = new FinanceContext();
			var count = context.Invoices.Count();

			Log.InfoFormat($"Database initialized with {count} invoices");
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