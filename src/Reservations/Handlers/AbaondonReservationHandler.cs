using System.Linq;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Reservations.Data.Context;
using Reservations.Messages.Commands;

namespace Reservations.Handlers
{
	public class AbaondonReservationHandler : IHandleMessages<AbandonReservationCommand>
	{
		private readonly IReservationsContext _reservationsContext;
		private static readonly ILog Log = LogManager.GetLogger<BookReservationHandler>();

		public AbaondonReservationHandler(IReservationsContext reservationsContext)
		{
			_reservationsContext = reservationsContext;
		}

		public async Task Handle(AbandonReservationCommand message, IMessageHandlerContext context)
		{
			Log.Info($"Handle AbandonReservationCommand for reservation {message.ReservationUuid}");

			var reservation = _reservationsContext.Reservations.Single(r => r.Uuid == message.ReservationUuid);
			reservation.Status = "Abandoned";

			await _reservationsContext.SaveChangesAsync();
		}
	}
}