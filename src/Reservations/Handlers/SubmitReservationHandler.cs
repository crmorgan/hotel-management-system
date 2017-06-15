using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Reservations.Data;
using Reservations.Messages.Commands;
using Reservations.Messages.Events;

namespace Reservations.Handlers
{
	public class SubmitReservationHandler : IHandleMessages<SubmitReservationCommand>
	{
		private static readonly ILog Log = LogManager.GetLogger<SubmitReservationHandler>();

		public async Task Handle(SubmitReservationCommand message, IMessageHandlerContext context)
		{
			Log.Info($"Handle SubmitReservationCommand for reservation {message.ReservationUuid}");

			using (var session = DocumentStoreHolder.Store.OpenSession())
			{
				var reservation = new Reservation
				{
					Uuid = message.ReservationUuid,
					RoomTypeId = message.RoomTypeId,
					Dates = message.Dates,
					Status = "Booking"
				};

				session.Store(reservation);
				session.SaveChanges();

				await context.Publish<ReservationSubmittedEvent>(e =>
				{
					e.ReservationUuid = reservation.Uuid;
				});
			}
		}
	}
}