namespace Payments.Messages.Events
{
	public class PaymentMethodSubmittedEvent
	{
		public string PaymentMethodId { get; set; }
		public string PurchaseUuid { get; set; }
	}
}