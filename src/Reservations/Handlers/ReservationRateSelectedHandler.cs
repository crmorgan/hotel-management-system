using System.Linq;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Reservations.Data.Context;
using Reservations.Data.Models;
using Reservations.Messages.Commands;
using Reservations.Messages.Events;

namespace Reservations.Handlers
{
	public class ReservationRateSelectedHandler 
		: IHandleMessages<SetReservationRateCommand>
	{
		private readonly IReservationsContext _reservationsContext;
		private static readonly ILog Log = LogManager.GetLogger<SubmitReservationHandler>();

		public ReservationRateSelectedHandler(IReservationsContext reservationsContext)
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