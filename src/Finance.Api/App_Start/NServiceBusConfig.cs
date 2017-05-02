using Autofac;
using NServiceBus;

namespace Finance.Api
{
	public class NServiceBusConfig
	{
		internal static void Configure(ContainerBuilder containerBuilder)
		{
			var endpointConfiguration = new EndpointConfiguration("HMS.Finance.API");

			endpointConfiguration.SendOnly();
			endpointConfiguration.UseTransport<MsmqTransport>().ConnectionString("deadLetter=false;journal=false");
			endpointConfiguration.UseSerialization<JsonSerializer>();
			endpointConfiguration.UsePersistence<InMemoryPersistence>();
			endpointConfiguration.SendFailedMessagesTo("error");
			endpointConfiguration.AuditProcessedMessagesTo("audit");

			var endpoint = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();

			containerBuilder.RegisterInstance(endpoint)
				.As<IEndpointInstance>()
				.SingleInstance();
		}
	}
}