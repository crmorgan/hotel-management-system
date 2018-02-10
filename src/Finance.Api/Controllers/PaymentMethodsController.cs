using Finance.Api.Models;
using Finance.Data.Context;
using Finance.Messages.Commands;
using NServiceBus;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

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

		public async Task<IHttpActionResult> Put(PaymentMethod paymentMethod)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			await _endpoint.Send(new SubmitPaymentMethodCommand
			{
				Id = paymentMethod.PaymentMethodUuid,
				PurchaseUuid = paymentMethod.PurchaseUuid,
				PaymentMethod = new Messages.Commands.CreditCard
				{
					CardHolderName = paymentMethod.Card.CardHolderName,
					AccountNumber = paymentMethod.Card.Number,
					TypeId = paymentMethod.Card.TypeId,
					Expiration = paymentMethod.Card.Expiration
				}
			});

			return CreatedAtRoute(
				"DefaultApi",
				new {controller = "paymentMethods", id = paymentMethod.PaymentMethodUuid},
				$"Payment method {paymentMethod.PaymentMethodUuid} created.");
		}
	}
}
