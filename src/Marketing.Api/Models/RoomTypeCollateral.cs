
namespace Marketing.Api.Models
{
	public class RoomTypeCollateral
	{
		public int RoomTypeId { get; }
		public string Description { get; }
		public string ImageUrl { get; }

		public RoomTypeCollateral(int roomTypeId, string description, string imageUrl)
		{
			RoomTypeId = roomTypeId;
			Description = description;
			ImageUrl = imageUrl;
		}
	}
}