namespace Payments.Api.Models
{
	public class PaymentMethod
	{
		public string PaymentMethodUuid { get; set; }
		public string PurchaseUuid { get; set; }
		public CreditCard Card { get; set; }
	}

	public class CreditCard
	{
		public string CardHolderName { get; set; }
		public string Number { get; set; }
		public int TypeId { get; set; }
		public string Expiration { get; set; }
	}
}