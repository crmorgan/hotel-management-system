using NServiceBus;
using SharedKernel;

namespace Reservations.Messages.Commands
{
	public class SubmitReservationCommand : ICommand
	{
		public string ReservationId { get; set; }
		public int RoomTypeId { get; set; }
		public DateRange Dates { get; set; }
	}
}