using NServiceBus;

namespace Reservations.Messages.Events
{
	public class ReservationBookedEvent : IEvent
	{
		public string ReservationUuid { get; set; }
	}
}