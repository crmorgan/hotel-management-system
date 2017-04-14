using SharedKernel;

namespace Reservations.Api.Models
{
	public class SubmitReservation
	{
		public string ReservationUuid { get; set; }
		public int RoomTypeId { get; set; }
		public DateRange Dates { get; set; }
	}
}