using System.Data.Entity;
using System.Threading.Tasks;
using Availability.Data.Migrations;
using Availability.Data.Models;

namespace Availability.Data.Context
{
	public interface IRoomAvailabilityContext
	{
		IDbSet<RoomTypeAvailability> RoomTypeAvailability { get; set; }

		Task<int> SaveChangesAsync();
	}

	[DbConfigurationType(typeof(SqLiteConfig))]
	public class RoomAvailabilityContext : DbContext, IRoomAvailabilityContext
	{
		public RoomAvailabilityContext() : base("HMS.Availability")
        {
		}

		public IDbSet<RoomTypeAvailability> RoomTypeAvailability { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			Database.SetInitializer(new DatabaseInitializer(modelBuilder));

			modelBuilder.Entity<RoomTypeAvailability>()
				.HasKey(p => p.Id);

			base.OnModelCreating(modelBuilder);
		}
	}
}