using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Guests.Api.Models;
using Guests.Data.Context;
using Guests.Messages.Commands;
using NServiceBus;

namespace Guests.Api.Controllers
{
    public class GuestsController : ApiController
    {
		private readonly IGuestsContext _context;
		private readonly IEndpointInstance _endpoint;

	    public GuestsController(IGuestsContext context, IEndpointInstance endpoint)
	    {
		    _context = context;
			_endpoint = endpoint;
		}

	    public IHttpActionResult Get()
	    {
		    var guests = _context.Guests.ToList();

		    return Ok(guests);
	    }


	    public IHttpActionResult Get(string id)
	    {
		    var guest = _context.Guests.SingleOrDefault(g => g.Id == id);

			if(guest == null ) return NotFound();

		    return Ok(guest);
	    }

	    public async Task<IHttpActionResult> Put(SubmitGuest model)
	    {
		    if(!ModelState.IsValid) return BadRequest(ModelState);

			await _endpoint.Send(new SubmitGuestCommand
			{
				GuestUuid = model.GuestUuid,
				ReservationUuid = model.ReservationUuid,
				Title = model.Title,
				FirstName = model.FirstName,
				LastName = model.LastName,
				Email = model.Email,
				Address = new Messages.Commands.Address
				{
					Line1 = model.Address.Line1,
					Line2 = model.Address.Line2,
					City = model.Address.City,
					State = model.Address.State,
					Zip = model.Address.Zip
				}
			});

			return CreatedAtRoute(
				"DefaultApi",
				new { controller = "payments", id = model.GuestUuid },
				$"Charge {model.GuestUuid} created.");
	    }
    }
}
