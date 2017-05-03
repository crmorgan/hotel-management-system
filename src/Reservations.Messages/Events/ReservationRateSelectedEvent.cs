using NServiceBus;

namespace Reservations.Messages.Events
{
	public class ReservationRateSelectedEvent : IEvent
	{
		public string ReservationUuid { get; set; }
		public decimal Rate { get; set; }
	}
}