using System;
using System.Threading.Tasks;
using ITOps.Messages.Commands;
using ITOps.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace ITOps.Handlers
{
	public class MakePaymentHandler : IHandleMessages<MakePaymentCommand>
	{
		private static readonly ILog Log = LogManager.GetLogger<MakePaymentHandler>();

		public async Task Handle(MakePaymentCommand message, IMessageHandlerContext context)
		{
			Log.Info($"Handle MakePaymentCommand for purchase '{message.PurchaseUuid}' in the amount of {message.Amount:C}");
			// you could request/response with other services here to compose the data required to send to payment gateway.
			await ChargeCreditCard(message.PaymentMethodId, message.Amount);
			await context.Publish<PaymentMadeEvent>(e => e.PurchaseUuid = message.PurchaseUuid);
		}

		private async Task ChargeCreditCard(string paymentMethodId, decimal amount)
		{
			// this would call some third-party payment gateway system.
			if (amount == 100m)
			{
				Log.Error("Simulating external payment gateway system being down.");
				await Task.Delay(6000);
				throw new Exception("The payment gateway did not respond.");
			}

			Log.Info("Calling external payment gateway for payment processing.");
			await Task.Delay(3000);
		}
	}
}