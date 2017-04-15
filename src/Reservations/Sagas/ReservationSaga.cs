using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Payments.Messages.Events;
using Reservations.Messages.Events;

namespace Reservations.Sagas
{
	public class ReservationSaga : Saga<ReservationSagaData>, 
		IAmStartedByMessages<ReservationSubmittedEvent>,
		IAmStartedByMessages<PaymentSucceededEvent>
	{
		private static readonly ILog Log = LogManager.GetLogger<ReservationSaga>();

		public async Task Handle(ReservationSubmittedEvent message, IMessageHandlerContext context)
		{
			Log.Info($"Handle ReservationSubmittedEvent for reservation {message.ReservationUuid}");

			Data.IsReservationSubmitted = true;
			Data.ReservationUuid = message.ReservationUuid;

			await ProcessReservation(context);
		}

		public async Task Handle(PaymentSucceededEvent message, IMessageHandlerContext context)
		{
			Log.Info($"Handle PaymentSucceededEvent for reservation {message.ReservationUuid}");

			Data.ReservationUuid = message.ReservationUuid;
			Data.RoomTypeId = message.RoomTypeId;
			Data.IsPaymentProcessed = true;

			await ProcessReservation(context);
		}


		protected override void ConfigureHowToFindSaga(SagaPropertyMapper<ReservationSagaData> mapper)
		{
			mapper.ConfigureMapping<ReservationSubmittedEvent>(p => p.ReservationUuid).ToSaga(s => s.ReservationUuid);
			mapper.ConfigureMapping<PaymentSucceededEvent>(p => p.ReservationUuid).ToSaga(s => s.ReservationUuid);
		}

		private async Task ProcessReservation(IMessageHandlerContext context)
		{
			if (IsReservationComplete())
			{
				//TODO: Reservation complete so we need to check/update occupancy level for the room type
				await context.Send<RoomTypeReservedEvent>(e =>
				{
					e.RoomTypeId = Data.RoomTypeId;
				});

				MarkAsComplete();
			}
		}

		private bool IsReservationComplete()
		{
			return Data.IsReservationSubmitted && Data.IsPaymentProcessed;
		}
	}

	public class RoomTypeReservedEvent
	{
		public int RoomTypeId { get; set; }
	}
}