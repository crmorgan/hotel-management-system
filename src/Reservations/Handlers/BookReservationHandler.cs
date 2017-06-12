using System.Linq;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Reservations.Data;
using Reservations.Messages.Commands;
using Reservations.Messages.Events;

namespace Reservations.Handlers
{
	public class BookReservationHandler : IHandleMessages<BookReservationCommand>
	{
		private static readonly ILog Log = LogManager.GetLogger<BookReservationHandler>();

		public async Task Handle(BookReservationCommand message, IMessageHandlerContext context)
		{
			Log.Info($"Handle BookReservationCommand for reservation {message.ReservationUuid}");

			using (var session = DocumentStoreHolder.Store.OpenSession())
			{
				var reservation = session.Query<Reservation>()
					.Single(r => r.Uuid == message.ReservationUuid);

				reservation.Status = "Booked";

				session.SaveChanges();
			}

			await context.Publish<ReservationBookedEvent>(e =>
			{
				e.ReservationUuid = message.ReservationUuid;
			});
		}
	}
}
