using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Rates.Api.Models;
using Rates.Data.Context;

namespace Rates.Api.Controllers
{
	public class RoomTypeRatesController : ApiController
	{
		[HttpGet]
		public IHttpActionResult Get([FromUri] int[] ids, DateTime checkin, DateTime checkout)
		{
			var rates = GetRatesForRoomTypes(ids, checkin, checkout);

			return Ok(rates);
		}

		private static IEnumerable<RoomTypeRate> GetRatesForRoomTypes(IEnumerable<int> roomTypeIds, DateTime checkin,
			DateTime checkout)
		{
			using (var db = new RatesContext())
			{
				return db.RoomRates
					.Where(rate => roomTypeIds.Contains(rate.RoomTypeId))
					.Where(rate => rate.StartDate <= checkin && rate.EndDate >= checkout)
					.ToList()
					.Select(rate => new RoomTypeRate(rate.Id, rate.RoomTypeId, rate.Amount, rate.CurrencyCode));
			}
		}
	}
}