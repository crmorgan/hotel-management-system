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
			Log.Info("Handle ReservationSubmittedEvent");

			Data.IsReservationSubmitted = true;
			Data.ReservationId = message.ReservationId;

			await ProcessReservation(context);
		}

		public async Task Handle(PaymentSucceededEvent message, IMessageHandlerContext context)
		{
			Log.Info("Handle PaymentSucceededEvent");

			Data.ReservationId = message.ReservationId;
			Data.RoomTypeId = message.RoomTypeId;
			Data.IsPaymentProcessed = true;

			await ProcessReservation(context);
		}


		protected override void ConfigureHowToFindSaga(SagaPropertyMapper<ReservationSagaData> mapper)
		{
			mapper.ConfigureMapping<ReservationSubmittedEvent>(p => p.ReservationId).ToSaga(s => s.ReservationId);
			mapper.ConfigureMapping<PaymentSucceededEvent>(p => p.ReservationId).ToSaga(s => s.ReservationId);
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