using System.Linq;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Reservations.Data.Context;
using Reservations.Messages.Commands;
using Reservations.Messages.Events;

namespace Reservations.Handlers
{
	public class BookReservationHandler : IHandleMessages<BookReservationCommand>
	{
		private readonly IReservationsContext _reservationsContext;
		private static readonly ILog Log = LogManager.GetLogger<BookReservationHandler>();

		public BookReservationHandler(IReservationsContext reservationsContext)
		{
			_reservationsContext = reservationsContext;
		}

		public async Task Handle(BookReservationCommand message, IMessageHandlerContext context)
		{
			Log.Info($"Handle BookReservationCommand for reservation {message.ReservationUuid}");

			var reservation = _reservationsContext.Reservations.Single(r => r.Uuid == message.ReservationUuid);
			reservation.Status = "Booked";

			await _reservationsContext.SaveChangesAsync();

			await context.Publish<ReservationBookedEvent>(e =>
			{
				e.ReservationUuid = message.ReservationUuid;
			});
		}
	}
}
