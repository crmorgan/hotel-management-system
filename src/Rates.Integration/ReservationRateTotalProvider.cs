using System.Linq;
using Rates.Data.Context;

namespace Rates.Integration
{
	public interface IProvideReservationRateTotal
	{
		decimal GetRateTotal(string reservationUuid);
	}

	public class ReservationRateTotalProvider : IProvideReservationRateTotal
	{
		public decimal GetRateTotal(string reservationUuid)
		{
			using (var db = new RatesContext())
			{
				return db.Reservations
					.Single(r => r.ReservationUuid == reservationUuid)
					.TotalAmount;
			}
		}
	}
}