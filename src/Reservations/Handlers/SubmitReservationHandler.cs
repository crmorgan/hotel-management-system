using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Reservations.Data.Context;
using Reservations.Data.Models;
using Reservations.Messages;
using Reservations.Messages.Commands;
using Reservations.Messages.Events;

namespace Reservations.Handlers
{
	public class SubmitReservationHandler : IHandleMessages<SubmitReservationCommand>
	{
		private readonly IReservationsContext _reservationsContext;
		private static readonly ILog Log = LogManager.GetLogger<SubmitReservationHandler>();

		public SubmitReservationHandler(IReservationsContext reservationsContext)
		{
			_reservationsContext = reservationsContext;
		}

		public async Task Handle(SubmitReservationCommand message, IMessageHandlerContext context)
		{
			Log.Info("Handle SubmitReservationCommand");

			var reservation = new Reservation
			{
				Uuid = message.ReservationId,
				RoomTypeId = message.RoomTypeId,
				Dates = message.Dates
			};

			_reservationsContext.Reservations.Add(reservation);
			await _reservationsContext.SaveChangesAsync();

			await context.Publish<ReservationSubmittedEvent>(e =>
			{
				e.ReservationId = reservation.Uuid;
			});
		}
	}
}