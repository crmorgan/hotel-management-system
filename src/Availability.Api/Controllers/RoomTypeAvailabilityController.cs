using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Availability.Api.Models;
using Availability.Data.Context;

namespace Availability.Api.Controllers
{
	public class RoomTypeAvailabilityController : ApiController
	{
		[HttpGet]
		public IHttpActionResult Get([FromUri] SearchCriteria criteria)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var availableRoomTypeIds = GetAvailableRoomTypeIds(criteria);

			return Ok(availableRoomTypeIds);
		}

		private static List<int> GetAvailableRoomTypeIds(SearchCriteria criteria)
		{
			using (var db = new RoomAvailabilityContext())
			{
				return db.RoomTypeAvailability
					.Where(roomAvailability => roomAvailability.IsAvailable)
					.Where(roomAvailability => roomAvailability.Date >= criteria.Dates.StartDate &&
					                           roomAvailability.Date <= criteria.Dates.EndDate)
					.Select(o => o.RoomTypeId)
					.Distinct()
					.ToList();
			}
		}
	}
}