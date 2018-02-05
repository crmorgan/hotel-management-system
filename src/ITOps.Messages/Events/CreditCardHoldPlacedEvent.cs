using NServiceBus;

namespace ITOps.Messages.Events
{
	public class CreditCardHoldPlacedEvent : IEvent
	{
		public string PurchaseUuid { get; set; }
	}
}