using System;
using NServiceBus;

namespace Reservations.Messages.Events
{
	public class ReservationSubmittedEvent : IEvent
	{
		public Guid ReservationId { get; set; }
	}
}