using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Rates.Api.Models;

namespace Rates.Api.Controllers
{
	public class RoomTypeRatesController : ApiController
	{
		[HttpGet]
		public IHttpActionResult Get([FromUri] int[] ids)
		{
			var rates = GetRatesForRoomTypes(ids);
			
			return Ok(rates);
		}

		private static IEnumerable<RoomTypeRate> GetRatesForRoomTypes(IEnumerable<int> roomTypeIds)
		{
			return roomTypeIds.Select(roomTypeId => new RoomTypeRate(roomTypeId, 199.99m, CurrencyCodes.USD));
		}
	}
}