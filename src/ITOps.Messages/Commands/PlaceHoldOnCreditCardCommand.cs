using NServiceBus;

namespace ITOps.Messages.Commands
{
	public class PlaceHoldOnCreditCardCommand : ICommand
	{
		public string PaymentMethodId { get; set; }
		public string PurchaseUuid { get; set; }
		public decimal Amount { get; set; }
	}
}