using NServiceBus;

namespace Reservations.Messages.Commands
{
	public class SetReservationRateCommand : ICommand
	{
		public string ReservationUuid { get; set; }
		public decimal Rate { get; set; }
	}
}