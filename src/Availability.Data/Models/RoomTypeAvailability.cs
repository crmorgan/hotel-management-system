using System;

namespace Availability.Data.Models
{
	public class RoomTypeAvailability
	{
		public int Id{ get; set; }
		public int RoomTypeId{ get; set; }
		public DateTime Date { get; set; }
		public bool IsAvailable { get; set; }
	}
}