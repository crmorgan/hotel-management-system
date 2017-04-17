using System;

namespace Payments.Data.Models
{
	public class Payment
	{
		public string Id { get; set; }
		public string PurchaseUuid { get; set; }
		public decimal Amount { get; set; }
		public CreditCard Card { get; set; }
	}
}