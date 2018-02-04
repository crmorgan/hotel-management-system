using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Rates.Data.Context;
using Rates.Data.Models;
using Rates.Messages.Commands;

namespace Rates.Handlers
{
	public class SubmitReservationHandler : IHandleMessages<BookReservationCommand>
	{
		private readonly IRatesContext _ratesContext;
		private static readonly ILog Log = LogManager.GetLogger<SubmitReservationHandler>();

		public SubmitReservationHandler(IRatesContext ratesContext)
		{
			_ratesContext = ratesContext;
		}

		public async Task Handle(BookReservationCommand message, IMessageHandlerContext context)
		{
			Log.Info($"Handle BookReservationCommand for reservation {message.ReservationUuid}");

			var reservation = new Reservation
			{
				ReservationUuid = message.ReservationUuid,
				RateId = message.RateId,
				TotalAmount = message.TotalAmount
			};

			_ratesContext.Reservations.Add(reservation);
			await _ratesContext.SaveChangesAsync();
		}
	}
}