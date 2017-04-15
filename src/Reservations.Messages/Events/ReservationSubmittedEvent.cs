using NServiceBus;

namespace Reservations.Messages.Events
{
	public class ReservationSubmittedEvent : IEvent
	{
		public string ReservationUuid { get; set; }
	}
}