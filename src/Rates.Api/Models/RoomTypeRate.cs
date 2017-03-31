namespace Rates.Api.Models
{
	public class RoomTypeRate
	{
		public RoomTypeRate(int roomTypeId, decimal amount) : this(roomTypeId, amount, CurrencyCodes.USD) { }

		public RoomTypeRate(int roomTypeId, decimal amount, CurrencyCodes currencyCode)
		{
			RoomTypeId = roomTypeId;
			Amount = amount;
			CurrencyCode = currencyCode.ToString();
		}

		public int RoomTypeId { get; }
		public decimal Amount { get; }
		public string CurrencyCode { get; }
	}
}