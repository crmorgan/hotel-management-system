using System.Threading.Tasks;
using Guests.Data.Context;
using Guests.Data.Models;
using Guests.Messages.Commands;
using Guests.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace Guests.Handlers
{
	public class SubmitGuestHandler : IHandleMessages<SubmitGuestCommand>
	{
		private readonly IGuestsContext _context;
		private readonly IGuestsContext _paymentsContext;
		private static readonly ILog Log = LogManager.GetLogger<SubmitGuestHandler>();

		public SubmitGuestHandler(IGuestsContext context)
		{
			_context = context;
		}

		public async Task Handle(SubmitGuestCommand message, IMessageHandlerContext context)
		{
			Log.Info($"Handle SubmitGuestCommand for guest {message.GuestUuid}");

			var guest = new Guest
			{
				Id = message.GuestUuid,
				Title = message.Title,
				FirstName = message.FirstName,
				LastName = message.LastName,
				Email = message.Email,
				Address = new Data.Models.Address
				{
					Line1 = message.Address.Line1,
					Line2 = message.Address.Line2,
					City = message.Address.City,
					State = message.Address.State,
					Zip = message.Address.Zip
				}
			};

			_context.Guests.Add(guest);
			await _context.SaveChangesAsync();

			await context.Publish<GuestSubmittedEvent>(e =>
			{
				e.GuestUuid = message.GuestUuid;
				e.ReservationUuid = message.ReservationUuid;
			});
		}
	}
}