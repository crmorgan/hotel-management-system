using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using NServiceBus;
using Reservations.Api.Models;
using Reservations.Data.Context;
using Reservations.Messages.Commands;

namespace Reservations.Api.Controllers
{
	public class ReservationsController : ApiController
	{
		private readonly IReservationsContext _context;
		private readonly IEndpointInstance _endpoint;

		public ReservationsController(IReservationsContext context, IEndpointInstance endpoint)
		{
			_context = context;
			_endpoint = endpoint;
		}

		public IHttpActionResult Get()
		{
			var reservations = _context.Reservations.ToList();
			return Ok(reservations);
		}

		public IHttpActionResult Get(int id)
		{
			return Ok(id);
		}

		public async Task<IHttpActionResult> Post(SubmitReservation reservation)
		{
			if(!ModelState.IsValid) return BadRequest(ModelState);

			await _endpoint.Send(new SubmitReservationCommand
			{
				ReservationUuid = reservation.ReservationUuid,
				RoomTypeId = reservation.RoomTypeId,
				Dates =reservation.Dates
			});

			return CreatedAtRoute("DefaultApi",
				new {controller = "reservations", id = reservation.ReservationUuid},
				$"Reservation {reservation.ReservationUuid} created.");
		}
	}
}