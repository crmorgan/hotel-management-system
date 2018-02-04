using NServiceBus;
using Reservations.Api.Models;
using Reservations.Data;
using Reservations.Messages.Commands;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Reservations.Api.Controllers
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
			using (var session = DocumentStoreHolder.Store.OpenSession())
			{
				var reservations = session.Query<Reservation>().ToList();
				return Ok(reservations);
			}
		}


		public IHttpActionResult Get(string uuid)
		{
			using (var session = DocumentStoreHolder.Store.OpenSession())
			{
				var reservation = session.Query<Reservation>()
					.SingleOrDefault(r => r.Uuid == uuid);

				return Ok(reservation);
			}
		}


		public async Task<IHttpActionResult> Post(SubmitReservation reservation)
		{
			if(!ModelState.IsValid)
				return BadRequest(ModelState);

			await _endpoint.Send(new SubmitReservationCommand
			{
				ReservationUuid = reservation.ReservationUuid,
				RoomTypeId = reservation.RoomTypeId,
				Dates = reservation.Dates
			});

			return CreatedAtRoute("DefaultApi",
				new {controller = "reservations", id = reservation.ReservationUuid},
				$"Reservation {reservation.ReservationUuid} created.");
		}
	}
}