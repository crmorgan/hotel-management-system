using System.Linq;
using System.Threading.Tasks;
using Common.Logging;
using NServiceBus;
using RoomOccupancy.Data.Context;
using RoomOccupancy.Messages.Events;

namespace RoomOccupancy.Handlers
{
	public class RoomTypeHoldSubmittedHandler : IHandleMessages<RoomTypeHoldSubmittedEvent>
	{
		private static readonly ILog Log = LogManager.GetLogger<RoomTypeHoldSubmittedEvent>();
		public async Task Handle(RoomTypeHoldSubmittedEvent message, IMessageHandlerContext context)
		{
			Log.Info("Handle OrderSubmittedEvent");

			using (var db = new RoomOccupancyContext())
			{
				var roomType = db.RoomTypes
					.First(rt => rt.Id == message.RoomTypeId);

				roomType.NumAvailable--;

				await db.SaveChangesAsync();
			}
		}
	}
}