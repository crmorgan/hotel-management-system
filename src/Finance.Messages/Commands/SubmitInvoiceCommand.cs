using System.Collections.Generic;
using NServiceBus;

namespace Finance.Messages.Commands
{
	public class SubmitInvoiceCommand : ICommand
	{
		public string InvoiceId { get; set; }
		public string CorrelationUuid { get; set; }

		public virtual IList<LineItem> LineItems { get; set; } = new List<LineItem>();

		public class LineItem
		{
			public string Description { get; set; }
			public string Type { get; set; }
			public int Quantity { get; set; }
			public decimal Price { get; set; }
		}
	}
}