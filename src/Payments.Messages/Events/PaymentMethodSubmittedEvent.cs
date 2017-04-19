using NServiceBus;

namespace Payments.Messages.Events
{
	public class PaymentMethodSubmittedEvent : IEvent
	{
		public string PaymentMethodId { get; set; }
		public string PurchaseUuid { get; set; }
	}
}