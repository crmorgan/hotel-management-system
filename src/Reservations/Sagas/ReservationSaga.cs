using Finance.Messages.Events;
using Guests.Messages.Events;
using ITOps.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using Reservations.Messages.Commands;
using Reservations.Messages.Events;
using System;
using System.Configuration;
using System.Threading.Tasks;
using ITOps.Messages.Commands;

namespace Reservations.Sagas
{
	public class ReservationSaga : Saga<ReservationSagaData>, 
		IAmStartedByMessages<ReservationSubmittedEvent>,
		IHandleMessages<PaymentMethodSubmittedEvent>,
		IHandleMessages<GuestSubmittedEvent>,
		IHandleMessages<ReservationRateSelectedEvent>,
		IHandleMessages<CreditCardHoldPlacedEvent>,
		IHandleTimeouts<HoldReservationTimeout>
	{
		private static readonly ILog Log = LogManager.GetLogger<ReservationSaga>();

		protected override void ConfigureHowToFindSaga(SagaPropertyMapper<ReservationSagaData> mapper)
		{
			mapper.ConfigureMapping<ReservationSubmittedEvent>(p => p.ReservationUuid).ToSaga(s => s.ReservationUuid);
			mapper.ConfigureMapping<PaymentMethodSubmittedEvent>(p => p.PurchaseUuid).ToSaga(s => s.ReservationUuid);
			mapper.ConfigureMapping<GuestSubmittedEvent>(p => p.ReservationUuid).ToSaga(s => s.ReservationUuid);
			mapper.ConfigureMapping<ReservationRateSelectedEvent>(p => p.ReservationUuid).ToSaga(s => s.ReservationUuid);
			mapper.ConfigureMapping<CreditCardHoldPlacedEvent>(p => p.PurchaseUuid).ToSaga(s => s.ReservationUuid);
		}

		public async Task Handle(ReservationSubmittedEvent message, IMessageHandlerContext context)
		{
			Log.Info($"Handle ReservationSubmittedEvent for reservation {message.ReservationUuid}");

			Data.IsReservationSubmitted = true;
			Data.ReservationUuid = message.ReservationUuid;

			var minutes = int.Parse(ConfigurationManager.AppSettings["ReservationHoldTimeoutMinutes"]);
			await RequestTimeout<HoldReservationTimeout>(context, TimeSpan.FromMinutes(minutes));
			await ProcessReservation(context);
		} 

		public async Task Timeout(HoldReservationTimeout state, IMessageHandlerContext context)
		{
			if (!Data.HasGuest && !Data.HasPaymentMethod)
			{
				await SendAbandonReservationCancellation(context);

				MarkAsComplete();
			}

		}

		public async Task Handle(PaymentMethodSubmittedEvent message, IMessageHandlerContext context)
		{
			Log.Info($"Handle PaymentMethodSubmittedEvent for reservation {message.PurchaseUuid}");

			Data.ReservationUuid = message.PurchaseUuid;
			Data.HasPaymentMethod = true;

			await context.Send<PlaceHoldOnCreditCardCommand>(e =>
			{
				e.PurchaseUuid = message.PurchaseUuid;
				e.PaymentMethodId = message.PaymentMethodId;
				e.Amount = 80m; // TODO: Need to calculate hold amount
			});

			await ProcessReservation(context);
		}


		public async Task Handle(GuestSubmittedEvent message, IMessageHandlerContext context)
		{
			Log.Info($"Handle GuestSubmittedEvent for reservation {message.ReservationUuid}");

			Data.ReservationUuid = message.ReservationUuid;
			Data.HasGuest = true;

			await ProcessReservation(context);
		}

		public async Task Handle(ReservationRateSelectedEvent message, IMessageHandlerContext context)
		{
			Log.Info($"Handle ReservationRateSelectedEvent for reservation {message.ReservationUuid}");

			Data.ReservationUuid = message.ReservationUuid;
			Data.HasRate = true;

			await ProcessReservation(context);
		}

		public async Task Handle(CreditCardHoldPlacedEvent message, IMessageHandlerContext context)
		{
			Log.Info($"Handle CreditCardHoldPlacedEvent for reservation {message.PurchaseUuid}");

			Data.ReservationUuid = message.PurchaseUuid;
			Data.HasCancellationFeeHold = true;

			await ProcessReservation(context);
		}

		private async Task ProcessReservation(IMessageHandlerContext context)
		{
			if (Data.HasCancellationFeeHold)
			{
				Log.Info($"Hold for cancellation fee payment approved for reservation '{Data.ReservationUuid}' sending confirmation email to guest.");
			}
			else if (Data.HasRequiredData)
			{
				Log.Info($"All reservation data has been collected and reservation is being booked {Data.ReservationUuid}.");

				await context.Send<BookReservationCommand>(e =>
				{
					e.ReservationUuid = Data.ReservationUuid;
				});
			}
		}

		private async Task SendAbandonReservationCancellation(IMessageHandlerContext context)
		{
			Log.Warn($"Abandoning reservation {Data.ReservationUuid}.");

			await context.Send<AbandonReservationCommand>(e =>
			{
				e.ReservationUuid = Data.ReservationUuid;
			});

			MarkAsComplete();
		}
	}

	public class HoldReservationTimeout
	{
	}
}