namespace Payments.Messages.Events
{
	public class PaymentSubmittedEvent
	{
		public string PaymentId { get; set; }
		public string PurchaseUuid { get; set; }
	}
}