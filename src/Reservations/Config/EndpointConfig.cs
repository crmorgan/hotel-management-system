using Finance.Messages.Events;
using Guests.Messages.Events;
using ITOps.Messages.Commands;
using ITOps.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using Reservations.Messages.Commands;
using Reservations.Messages.Events;
using System;

namespace Reservations.Config
{
	[EndpointName("HMS.Reservations")]
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Server
	{
		public EndpointConfig()
		{
			LogManager.Use<DefaultFactory>();

			if (Environment.UserInteractive)
				Console.Title = "HMS.Reservations";
		}

		public void Customize(EndpointConfiguration endpointConfiguration)
		{
			var container = ContainerSetup.Create();

			endpointConfiguration.UseSerialization<JsonSerializer>();
			endpointConfiguration.Recoverability().Delayed(c => c.NumberOfRetries(0));
			endpointConfiguration.UseContainer<AutofacBuilder>(c => c.ExistingLifetimeScope(container));
			endpointConfiguration.UsePersistence<InMemoryPersistence>();
			// limit saga to single process since InMemoryPersistence has concurrency issues (would not do this in prod app)
			endpointConfiguration.LimitMessageProcessingConcurrencyTo(1);
			endpointConfiguration.SendFailedMessagesTo("error");
			endpointConfiguration.AuditProcessedMessagesTo("audit");
			endpointConfiguration.EnableInstallers();

			var transport = endpointConfiguration.UseTransport<MsmqTransport>()
				.ConnectionString("deadLetter=false;journal=false");

			var routing = transport.Routing();
			routing.RegisterPublisher(typeof(PaymentMethodSubmittedEvent).Assembly, "HMS.Finance");
			routing.RegisterPublisher(typeof(GuestSubmittedEvent).Assembly, "HMS.Guests");
			routing.RegisterPublisher(typeof(ReservationSubmittedEvent).Assembly, "HMS.Reservations");
			routing.RouteToEndpoint(typeof(BookReservationCommand), "HMS.Reservations");
			routing.RouteToEndpoint(typeof(AbandonReservationCommand), "HMS.Reservations");
			routing.RegisterPublisher(typeof(CreditCardHoldPlacedEvent), "HMS.ITOps");
			routing.RouteToEndpoint(typeof(PlaceHoldOnCreditCardCommand), "HMS.ITOps");
		}
	}
}