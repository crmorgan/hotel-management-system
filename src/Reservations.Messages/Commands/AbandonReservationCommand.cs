using NServiceBus;

namespace Reservations.Messages.Commands
{
	public class AbandonReservationCommand : ICommand
	{
		public string ReservationUuid { get; set; }
	}
}