using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Finance.Api.Models;
using Finance.Data.Context;
using Finance.Messages.Commands;
using NServiceBus;

namespace Finance.Api.Controllers
{
	public class InvoicesController : ApiController
	{
		private readonly IFinanceContext _context;
		private readonly IEndpointInstance _endpoint;

		public InvoicesController(IFinanceContext context, IEndpointInstance endpoint)
		{
			_context = context;
			_endpoint = endpoint;
		}

		public IHttpActionResult Get()
		{
			var invoices = _context.Invoices.ToList();
			return Ok(invoices);
		}

		public async Task<IHttpActionResult> Put(SubmitInvoice model)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			await _endpoint.Send(new SubmitInvoiceCommand
			{
				InvoiceId = model.InvoiceUuid,
				CorrelationUuid = model.CorrelationUuid,
				LineItems = model.LineItems.Select(l => new SubmitInvoiceCommand.LineItem
					{
						Description = l.Description,
						Price = l.Price,
						Quantity = l.Quantity,
						Type = l.Type
					})
					.ToList()
			});

			return CreatedAtRoute(
				"DefaultApi",
				new { controller = "invoices", id = model.InvoiceUuid },
				$"Invoice {model.InvoiceUuid} created.");
		}
	}
}