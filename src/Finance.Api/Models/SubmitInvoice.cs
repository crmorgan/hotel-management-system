using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Finance.Api.Models
{
	public class SubmitInvoice
	{
		[Required]
		public string InvoiceUuid { get; set; }

		[Required]
		public string CorrelationUuid { get; set; }
		public IEnumerable<SubmitLineItem> LineItems { get; set; } = new List<SubmitLineItem>();

		public class SubmitLineItem
		{
			public string Description { get; set; }
			public string Type { get; set; }
			public int Quantity { get; set; }
			public decimal Price { get; set; }
		}
	}
}