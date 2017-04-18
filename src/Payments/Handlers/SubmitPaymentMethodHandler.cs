using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Payments.Data.Context;
using Payments.Data.Models;
using Payments.Messages.Commands;
using Payments.Messages.Events;
using CreditCard = Payments.Data.Models.CreditCard;

namespace Payments.Handlers
{
	public class SubmitPaymentMethodHandler : IHandleMessages<SubmitPaymentMethodCommand>
	{
		private readonly IPaymentsContext _paymentsContext;
		private static readonly ILog Log = LogManager.GetLogger<SubmitPaymentMethodHandler>();

		public SubmitPaymentMethodHandler(IPaymentsContext paymentsContext)
		{
			_paymentsContext = paymentsContext;
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

			_paymentsContext.PaymentMethods.Add(paymentMethod);

			await _paymentsContext.SaveChangesAsync();

			await context.Publish<PaymentMethodSubmittedEvent>(e =>
			{
				e.PaymentMethodId = paymentMethod.Id;
				e.PurchaseUuid = paymentMethod.PurchaseUuid;
			});
		}
	}
}