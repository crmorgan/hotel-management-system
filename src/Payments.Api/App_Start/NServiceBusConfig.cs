using Autofac;
using NServiceBus;

namespace Reservations.Api
{
	public class NServiceBusConfig
	{
		internal static void Configure(ContainerBuilder containerBuilder)
		{
			var config = new EndpointConfiguration("Payments.API");

			config.SendOnly();

			config.UseTransport<MsmqTransport>().ConnectionString("deadLetter=false;journal=false");
			config.UseSerialization<JsonSerializer>();
			config.UsePersistence<InMemoryPersistence>();

			config.SendFailedMessagesTo("error");

			config.Conventions()
				.DefiningCommandsAs(t => t.Namespace != null && t.Namespace == "Payments.Messages" || t.Name.EndsWith("Command"))
				.DefiningEventsAs(t => t.Namespace != null && t.Namespace == "Payments.Messages" || t.Name.EndsWith("Event"));

			var endpoint = Endpoint.Start(config).GetAwaiter().GetResult();

			containerBuilder.RegisterInstance(endpoint)
				.As<IEndpointInstance>()
				.SingleInstance();
		}
	}
}