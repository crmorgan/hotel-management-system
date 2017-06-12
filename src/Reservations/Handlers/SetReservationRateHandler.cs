using System.Linq;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Reservations.Data;
using Reservations.Messages.Commands;
using Reservations.Messages.Events;

namespace Reservations.Handlers
{
	public class SetReservationRateHandler 
		: IHandleMessages<SetReservationRateCommand>
	{
		private static readonly ILog Log = LogManager.GetLogger<SubmitReservationHandler>();

		public async Task Handle(SetReservationRateCommand message, IMessageHandlerContext context)
		{
			Log.Info($"Handle SubmitReservationCommand for reservation {message.ReservationUuid}");

			using (var session = DocumentStoreHolder.Store.OpenSession())
			{
				var reservation = session.Query<Reservation>()
					.Single(r => r.Uuid == message.ReservationUuid);

				reservation.Rate = message.Rate;

				session.SaveChanges();

				await context.Publish<ReservationRateSelectedEvent>(e =>
				{
					e.ReservationUuid = reservation.Uuid;
				});
			}
		}
	}
}