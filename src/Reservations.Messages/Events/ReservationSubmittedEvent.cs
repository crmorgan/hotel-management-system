using System;
using NServiceBus;

namespace Reservations.Messages.Events
{
	public class ReservationSubmittedEvent : IEvent
	{
		public string ReservationId { get; set; }
	}
}