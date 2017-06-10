using System.Threading.Tasks;
using Finance.Data.Context;
using Finance.Data.Models;
using Finance.Messages.Commands;
using Finance.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using CreditCard = Finance.Data.Models.CreditCard;

namespace Finance.Handlers
{
	public class SubmitPaymentMethodHandler : IHandleMessages<SubmitPaymentMethodCommand>
	{
		private readonly IFinanceContext _financeContext;
		private static readonly ILog Log = LogManager.GetLogger<SubmitPaymentMethodHandler>();

		public SubmitPaymentMethodHandler(IFinanceContext financeContext)
		{
			_financeContext = financeContext;
		}

		public async Task Handle(SubmitPaymentMethodCommand message, IMessageHandlerContext context)
		{
			Log.Info($"Handle SubmitPaymentMethodCommand for purchase {message.PurchaseUuid}");

			var card = new CreditCard
			{
				AccountNumberLast4 = message.PaymentMethod.AccountNumberLast4,
				Expiration = message.PaymentMethod.Expiration,
				TypeId = message.PaymentMethod.TypeId,
				CardHolderName = message.PaymentMethod.CardHolderName
			};

			var paymentMethod = new PaymentMethod
			{
				Id = message.Id,
				PurchaseUuid = message.PurchaseUuid,
				CreditCard = card
			};

			_financeContext.PaymentMethods.Add(paymentMethod);

			await _financeContext.SaveChangesAsync();

			await context.Publish<PaymentMethodSubmittedEvent>(e =>
			{
				e.PaymentMethodId = paymentMethod.Id;
				e.PurchaseUuid = paymentMethod.PurchaseUuid;
			});
		}
	}
}