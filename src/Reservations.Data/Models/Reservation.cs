using System;
using SharedKernel;

namespace Reservations.Data.Models
{
	public class Reservation
	{
		public string Uuid { get; set; }
		public int RoomTypeId { get; set; }
		public DateRange Dates { get; set; }
	}
}