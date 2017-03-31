using Rates.SharedKernel;

namespace Rates.Api.Models
{
	public class RoomTypeRate
	{
		public RoomTypeRate(int roomTypeId, decimal amount) : this(roomTypeId, amount, CurrencyCodes.USD) { }

		public RoomTypeRate(int rateId, int roomTypeId, decimal amount, CurrencyCodes currencyCode)
		{
			RateId = rateId;
			RoomTypeId = roomTypeId;
			Amount = amount;
			CurrencyCode = currencyCode.ToString();
		}

		public int RateId { get; }
		public int RoomTypeId { get; }
		public decimal Amount { get; }
		public string CurrencyCode { get; }
	}
}