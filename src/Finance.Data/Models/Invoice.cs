using System.Collections.Generic;
using System.Linq;

namespace Finance.Data.Models
{
	public class Invoice
	{
		public string Id { get; set; }
		public string CorrelationUuid { get; set; }

		public decimal Total
		{
			get { return LineItems.Sum(lineItem => lineItem.Extension); }
		}

		public decimal Balance { get; set; }
		public virtual IList<LineItem> LineItems { get; set; } = new List<LineItem>();
	}
}