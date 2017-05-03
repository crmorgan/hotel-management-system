using System;
using SharedKernel;

namespace Reservations.Data.Models
{
	public class Reservation
	{
		public int Id { get; set; }
		public string Uuid { get; set; }
		public int RoomTypeId { get; set; }
		public DateRange Dates { get; set; }
		public decimal Rate { get; set; }
		public string Status { get; set; }
	}
}