using NServiceBus;

namespace Guests.Messages.Events
{
	public class GuestSubmittedEvent : IEvent
	{
		public string GuestUuid { get; set; }
		public string ReservationUuid { get; set; }
	}
}