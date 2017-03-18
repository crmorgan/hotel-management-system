using System.Linq;
using System.Web.Http;
using RoomOccupancy.Api.Models;
using RoomOccupancy.Data.Context;

namespace RoomOccupancy.Api.Controllers
{
	public class RoomTypeOccupancyController : ApiController
	{
		[HttpGet]
		public IHttpActionResult Get(SearchCriteria criteria)
		{
			using (var db = new RoomOccupancyContext())
			{
				var roomTypes = db.RoomTypes
					.ToList()
					.Select(o => new
				{
					o.Id,
					o.Name,
					o.NumAvailable	
				});

				return Ok(roomTypes);
			}
		}
	}
}