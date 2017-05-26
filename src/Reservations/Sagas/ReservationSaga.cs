using System.Threading.Tasks;
using Guests.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using Payments.Messages.Events;
using Reservations.Data.Context;
using Reservations.Messages.Commands;
using Reservations.Messages.Events;

namespace Reservations.Sagas
{
	public class ReservationSaga : Saga<ReservationSagaData>, 
		IAmStartedByMessages<ReservationSubmittedEvent>,
		IHandleMessages<PaymentMethodSubmittedEvent>,
		IHandleMessages<GuestSubmittedEvent>,
		IHandleMessages<ReservationRateSelectedEvent>
	{
		private readonly IReservationsContext _reservationsContext;
		private static readonly ILog Log = LogManager.GetLogger<ReservationSaga>();

		public async Task Handle(ReservationSubmittedEvent message, IMessageHandlerContext context)
		{
			Log.Info($"Handle ReservationSubmittedEvent for reservation {message.ReservationUuid}");

			Data.IsReservationSubmitted = true;
			Data.ReservationUuid = message.ReservationUuid;

			await ProcessReservation(context);
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

		private bool IsReservationDataCollected()
		{
			return Data.IsReservationSubmitted && Data.HasPaymentMethod && Data.HasGuest && Data.HasRate;
		}
	}
}