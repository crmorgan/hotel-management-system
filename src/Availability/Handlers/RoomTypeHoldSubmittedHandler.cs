using System.Linq;
using System.Threading.Tasks;
using Availability.Data.Context;
using Availability.Messages.Events;
using Common.Logging;
using NServiceBus;

namespace Availability.Handlers
{
	public class RoomTypeHoldSubmittedHandler : IHandleMessages<RoomTypeHoldSubmittedEvent>
	{
		private static readonly ILog Log = LogManager.GetLogger<RoomTypeHoldSubmittedEvent>();
		public async Task Handle(RoomTypeHoldSubmittedEvent message, IMessageHandlerContext context)
		{
			Log.Info("Handle OrderSubmittedEvent");

			using (var db = new RoomAvailabilityContext())
			{
				var roomType = db.RoomTypeAvailability
					.First(rt => rt.Id == message.RoomTypeId);

				//roomType.NumAvailable--;

				await db.SaveChangesAsync();
			}
		}
	}
}