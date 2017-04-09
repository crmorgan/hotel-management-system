using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Marketing.Api.Models;
using Marketing.Data.Context;

namespace Marketing.Api.Controllers
{
	[RoutePrefix("api/collateral")]
	public class RoomTypeCollateralController : ApiController
	{
		[HttpGet, Route("roomtypes/{ids=ids}")]
		public IHttpActionResult Get([FromUri] int[] ids)
		{
			var collateral = GetCollateral(ids);

			return Ok(collateral);
		}

		private static IEnumerable<RoomTypeCollateral> GetCollateral(IEnumerable<int> roomTypeIds)
		{
			using (var db = new MarketingContext())
			{
				return db.RoomTypeCollateral
					.Where(collateral => roomTypeIds.Contains(collateral.RoomTypeId))
					.ToList()
					.Select(collateral => new RoomTypeCollateral(collateral.RoomTypeId, collateral.Description, collateral.ImageUrl));
			}
		}
	}
}