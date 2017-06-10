using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Finance.Data.Context;
using NServiceBus;
using Finance.Api.Models;
using Finance.Messages.Commands;

namespace Finance.Api.Controllers
{
	public class PaymentMethodsController : ApiController
	{
		private readonly IFinanceContext _context;
		private readonly IEndpointInstance _endpoint;

		public PaymentMethodsController(IFinanceContext context, IEndpointInstance endpoint)
		{
			_context = context;
			_endpoint = endpoint;
		}

		public IHttpActionResult Get()
		{
			var payments = _context.PaymentMethods.ToList();

			return Ok(payments);
		}

		public IHttpActionResult Get(string id)
		{
			var payment = _context.PaymentMethods.SingleOrDefault(p => p.Id == id);

			if (payment == null) return NotFound();

			return Ok(payment);
		}

		public async Task<IHttpActionResult> Put(PaymentMethod charge)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			await _endpoint.Send(new SubmitPaymentMethodCommand
			{
				Id = charge.PaymentMethodUuid,
				PurchaseUuid = charge.PurchaseUuid,
				PaymentMethod = new Messages.Commands.CreditCard
				{
					CardHolderName = charge.Card.CardHolderName,
					AccountNumber = charge.Card.Number,
					TypeId = charge.Card.TypeId,
					Expiration = charge.Card.Expiration
				}
			});

			return CreatedAtRoute(
				"DefaultApi",
				new {controller = "payments", id = charge.PaymentMethodUuid},
				$"Charge {charge.PaymentMethodUuid} created.");
		}
	}
}
