using System.ComponentModel.DataAnnotations;
using SharedKernel;

namespace Reservations.Api.Models
{
	public class SubmitReservation
	{
		[Required]
		public string ReservationUuid { get; set; }

		[Required]
		public int RoomTypeId { get; set; }

		[Required]
		public DateRange Dates { get; set; }
	}
}