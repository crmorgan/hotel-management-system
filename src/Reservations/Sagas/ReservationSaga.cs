using System;
using System.Configuration;
using System.Threading.Tasks;
using Finance.Messages.Events;
using Guests.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using Reservations.Messages.Commands;
using Reservations.Messages.Events;

namespace Reservations.Sagas
{
	public class ReservationSaga : Saga<ReservationSagaData>, 
		IAmStartedByMessages<ReservationSubmittedEvent>,
		IHandleMessages<PaymentMethodSubmittedEvent>,
		IHandleMessages<GuestSubmittedEvent>,
		IHandleMessages<ReservationRateSelectedEvent>,
		IHandleTimeouts<HoldReservationTimeout>
	{
		private static readonly ILog Log = LogManager.GetLogger<ReservationSaga>();

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

			await ProcessReservation(context);
		}


		public async Task Handle(GuestSubmittedEvent message, IMessageHandlerContext context)
		{
			Log.Info($"Handle PaymentMethodSubmittedEvent for reservation {message.ReservationUuid}");

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


		protected override void ConfigureHowToFindSaga(SagaPropertyMapper<ReservationSagaData> mapper)
		{
			mapper.ConfigureMapping<ReservationSubmittedEvent>(p => p.ReservationUuid).ToSaga(s => s.ReservationUuid);
			mapper.ConfigureMapping<PaymentMethodSubmittedEvent>(p => p.PurchaseUuid).ToSaga(s => s.ReservationUuid);
			mapper.ConfigureMapping<GuestSubmittedEvent>(p => p.ReservationUuid).ToSaga(s => s.ReservationUuid);
			mapper.ConfigureMapping<ReservationRateSelectedEvent>(p => p.ReservationUuid).ToSaga(s => s.ReservationUuid);
		}

		private async Task ProcessReservation(IMessageHandlerContext context)
		{
			if (IsReservationDataCollected())
			{
				Log.Info($"Booking reservation {Data.ReservationUuid}.");

				await context.Send<BookReservationCommand>(e =>
				{
					e.ReservationUuid = Data.ReservationUuid;
				});

				MarkAsComplete();
			}
		}

		private async Task SendAbandonReservationCancellation(IMessageHandlerContext context)
		{
			Log.Info($"Abandoning reservation {Data.ReservationUuid}.");

			await context.Send<AbandonReservationCommand>(e =>
			{
				e.ReservationUuid = Data.ReservationUuid;
			});

			MarkAsComplete();
		}

		private bool IsReservationDataCollected()
		{
			return Data.IsReservationSubmitted && Data.HasPaymentMethod && Data.HasGuest && Data.HasRate;
		}
	}

	public class HoldReservationTimeout
	{
	}
}