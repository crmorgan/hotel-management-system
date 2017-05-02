using NServiceBus;

namespace Finance.Messages.Events
{
	public class InvoiceSubmittedEvent : IEvent
	{
		public string InvoiceId { get; set; }
		public string CorrelationUuid { get; set; }
	}
}