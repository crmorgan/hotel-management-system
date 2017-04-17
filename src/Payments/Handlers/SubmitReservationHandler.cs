using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Payments.Data.Context;
using Payments.Data.Models;
using Payments.Messages.Commands;
using Payments.Messages.Events;

namespace Payments.Handlers
{
	public class SubmitPaymentHandler : IHandleMessages<SubmitPaymentCommand>
	{
		private readonly IPaymentsContext _paymentsContext;
		private static readonly ILog Log = LogManager.GetLogger<SubmitPaymentHandler>();

		public SubmitPaymentHandler(IPaymentsContext paymentsContext)
		{
			_paymentsContext = paymentsContext;
		}

		public async Task Handle(SubmitPaymentCommand message, IMessageHandlerContext context)
		{
			Log.Info($"Handle SubmitPaymentCommand for purchase {message.Payment.PurchaseUuid}");

			var payment = new Payment
			{
				Id = message.Payment.Id,
				PurchaseUuid = message.Payment.PurchaseUuid,
				Amount = message.Payment.Amount,
				Card = message.Payment.Card
			};

			_paymentsContext.CreditCards.Add(payment.Card);

			await _paymentsContext.SaveChangesAsync();

			await context.Publish<PaymentSubmittedEvent>(e =>
			{
				e.PaymentId = payment.Id;
				e.PurchaseUuid = payment.PurchaseUuid;
			});
		}
	}
}