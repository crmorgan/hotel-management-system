using System;
using Payments.Data.Models;

namespace Payments.Api.Models
{
	public class SubmitPayment
	{
		public string PaymentUuid { get; set; }
		public string PurchaseUuid { get; set; }
		public decimal Amount { get; set; }
		public CreditCard Card { get; set; }
	}
}