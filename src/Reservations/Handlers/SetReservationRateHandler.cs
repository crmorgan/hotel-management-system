using System.Linq;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Reservations.Data.Context;
using Reservations.Messages.Commands;
using Reservations.Messages.Events;

namespace Reservations.Handlers
{
	public class SetReservationRateHandler 
		: IHandleMessages<SetReservationRateCommand>
	{
		private readonly IReservationsContext _reservationsContext;
		private static readonly ILog Log = LogManager.GetLogger<SubmitReservationHandler>();

		public SetReservationRateHandler(IReservationsContext reservationsContext)
		{
			_reservationsContext = reservationsContext;
		}

		public async Task Handle(SetReservationRateCommand message, IMessageHandlerContext context)
		{
			Log.Info($"Handle SubmitReservationCommand for reservation {message.ReservationUuid}");

			var reservation = _reservationsContext.Reservations
				.Single(r => r.Uuid == message.ReservationUuid);

			reservation.Rate = message.Rate;

			await _reservationsContext.SaveChangesAsync();

			await context.Publish<ReservationRateSelectedEvent>(e =>
			{
				e.ReservationUuid = reservation.Uuid;
			});
		}
	}
}