
using NServiceBus;

namespace Rates.Messages.Commands
{
	public class BookReservationCommand : ICommand
	{
		public string ReservationUuid { get; set; }
		public int RateId { get; set; }
		public decimal TotalAmount { get; set; }
	}
}