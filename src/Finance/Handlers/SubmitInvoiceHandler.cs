using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using Finance.Data.Context;
using Finance.Data.Models;
using Finance.Messages.Commands;
using Finance.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace Finance.Handlers
{
	public class SubmitInvoiceHandler : IHandleMessages<SubmitInvoiceCommand>
	{
		private readonly IFinanceContext _context;
		private static readonly ILog Log = LogManager.GetLogger<SubmitInvoiceHandler>();

		public SubmitInvoiceHandler(IFinanceContext context)
		{
			_context = context;
		}

		public async Task Handle(SubmitInvoiceCommand message, IMessageHandlerContext context)
		{
			Log.Info($"Handle SubmitInvoiceCommand for invoice {message.InvoiceId}");

			var invoice = new Invoice
			{
				Id = message.InvoiceId,
				CorrelationUuid = message.CorrelationUuid,
				LineItems = message.LineItems.Select(messageLineItem => new LineItem
				{
					Description = messageLineItem.Description,
					Price = messageLineItem.Price,
					Quantity = messageLineItem.Quantity,
					Type = messageLineItem.Type
				})
				.ToList()
			};

			invoice.Balance = invoice.LineItems.Sum(p => p.Extension);

			_context.Invoices.AddOrUpdate(invoice);
			await _context.SaveChangesAsync();

			await context.Publish<InvoiceSubmittedEvent>(e =>
			{
				e.InvoiceId = message.InvoiceId;
				e.CorrelationUuid = message.CorrelationUuid;
			});
		}
	}
}