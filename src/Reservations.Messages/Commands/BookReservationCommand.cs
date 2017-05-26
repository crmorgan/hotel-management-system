using NServiceBus;

namespace Reservations.Messages.Commands
{
	public class BookReservationCommand : ICommand
	{
		public string ReservationUuid { get; set; }
	}
}