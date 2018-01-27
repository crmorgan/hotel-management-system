using NServiceBus;

namespace ITOps.Messages.Events
{
	public class PaymentMadeEvent : IEvent
	{
		public string PurchaseUuid { get; set; }
	}
}