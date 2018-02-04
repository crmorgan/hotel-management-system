using NServiceBus;
using Rates.Data.Context;
using Rates.Data.Models;
using Rates.Messages.Commands;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Rates.Api.Controllers
{
	public class ReservationsController : ApiController
    {
		private readonly IEndpointInstance _endpoint;

		public ReservationsController(IEndpointInstance endpoint)
		{
			_endpoint = endpoint;
		}

		public IHttpActionResult Get()
		{
			var reservations = GetAllReservations();

			return Ok(reservations);
		}

	    public IHttpActionResult Get(string id)
	    {
		    var reservation = GetReservation(id);

		    return Ok(reservation);
	    }

	    public async Task<IHttpActionResult> Put(string id, BookReservation model)
	    {
		    if (!ModelState.IsValid)
			    return BadRequest(ModelState);

		    await _endpoint.Send(new BookReservationCommand
		    {
			    ReservationUuid = id,
			    RateId = model.RateId,
			    TotalAmount = model.TotalAmount
		    });

		    return CreatedAtRoute("DefaultApi",
			    new { controller = "reservations", id },
			    $"Reservation {id} created.");
	    }


		private IEnumerable<Reservation> GetAllReservations()
	    {
		    using (var db = new RatesContext())
		    {
			    return db.Reservations.ToList();
		    }
	    }

		private static Reservation GetReservation(string reservationUuid)
	    {
		    using (var db = new RatesContext())
		    {
			    return db.Reservations
					.SingleOrDefault(r => r.ReservationUuid == reservationUuid);
		    }
	    }
	}

	public class BookReservation
	{
		[Required]
		public int RateId { get; set; }

		[Required]
		public decimal TotalAmount { get; set; }
	}
}
