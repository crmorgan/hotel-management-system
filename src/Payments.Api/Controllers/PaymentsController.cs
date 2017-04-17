using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using NServiceBus;
using Payments.Api.Models;
using Payments.Data.Context;
using Payments.Data.Models;
using Payments.Messages.Commands;

namespace Payments.Api.Controllers
{
	public class PaymentsController : ApiController
	{
		private readonly IPaymentsContext _context;
		private readonly IEndpointInstance _endpoint;

		public PaymentsController(IPaymentsContext context, IEndpointInstance endpoint)
		{
			_context = context;
			_endpoint = endpoint;
		}

		public IHttpActionResult Get()
		{
			var payments = _context.Payments.ToList();

			return Ok(payments);
		}

		public IHttpActionResult Get(string id)
		{
			var payment = _context.Payments.Where(p => p.Id == id);

			return Ok(payment);
		}

		public async Task<IHttpActionResult> Post(SubmitPayment payment)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			await _endpoint.Send(new SubmitPaymentCommand
			{
				Payment = new Payment
				{
					Id = payment.PaymentUuid,
					PurchaseUuid = payment.PurchaseUuid,
					Amount = payment.Amount,
					Card = payment.Card
				}
			});

			return CreatedAtRoute(
				"DefaultApi",
				new {controller = "payments", id = payment.PaymentUuid},
				$"Payment {payment.PaymentUuid} created.");
		}
	}
}
