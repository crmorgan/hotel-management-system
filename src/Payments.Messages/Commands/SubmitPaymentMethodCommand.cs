using NServiceBus;

namespace Payments.Messages.Commands
{
	public class SubmitPaymentMethodCommand : ICommand
	{
		public string Id { get; set; }
		public string PurchaseUuid { get; set; }
		public CreditCard PaymentMethod { get; set; }
	}

	public class CreditCard
	{
		public string CardHolderName { get; set; }
		public string AccountNumber { get; set; }
		public string AccountNumberLast4 => AccountNumber.Substring(AccountNumber.Length - 4);
		public int TypeId { get; set; }
		public string Expiration { get; set; }
	}

}