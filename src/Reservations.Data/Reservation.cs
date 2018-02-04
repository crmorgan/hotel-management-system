using SharedKernel;

namespace Reservations.Data
{
	public class Reservation
	{
		public int Id { get; set; }
		public string Uuid { get; set; }
		public int RoomTypeId { get; set; }
		public DateRange Dates { get; set; }
		public string Status { get; set; }
	}
}