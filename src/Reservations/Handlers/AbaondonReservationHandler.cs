using System.Linq;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Reservations.Data;
using Reservations.Messages.Commands;

namespace Reservations.Handlers
{
	public class AbaondonReservationHandler : IHandleMessages<AbandonReservationCommand>
	{
		private static readonly ILog Log = LogManager.GetLogger<BookReservationHandler>();

		public async Task Handle(AbandonReservationCommand message, IMessageHandlerContext context)
		{
			Log.Info($"Handle AbandonReservationCommand for reservation {message.ReservationUuid}");

			using (var session = DocumentStoreHolder.Store.OpenSession())
			{
				var reservation = session.Query<Reservation>()
					.Single(r => r.Uuid == message.ReservationUuid);

				reservation.Status = "Abandoned";

				session.SaveChanges();
			}
		}
	}
}