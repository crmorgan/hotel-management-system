using System.Collections.Generic;
using System.Web.Http;
using RoomOccupancy.Api.Models;

namespace RoomOccupancy.Api.Controllers
{
	public class SearchController : ApiController
	{
		[HttpGet]
		public IHttpActionResult Search(SearchCriteria criteria)
		{
			return Ok(new List<int> {1, 2, 3, 4});
		}
	}
}