using System.Data.Entity;
using System.Threading.Tasks;
using Divergent.Finance.Data.Context;
using RoomOccupancy.Data.Migrations;
using RoomOccupancy.Data.Models;

namespace RoomOccupancy.Data.Context
{
	public interface IRoomOccupancyContext
	{
		IDbSet<RoomType> RoomTypes { get; set; }

		Task<int> SaveChangesAsync();
	}

	[DbConfigurationType(typeof(SqLiteConfig))]
	public class RoomOccupancyContext : DbContext, IRoomOccupancyContext
	{
		public RoomOccupancyContext() : base("HMS.RoomOccupancy")
        {
		}

		public IDbSet<RoomType> RoomTypes { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			Database.SetInitializer(new DatabaseInitializer(modelBuilder));

			modelBuilder.Entity<RoomType>()
				.HasKey(p => p.Id);

			base.OnModelCreating(modelBuilder);
		}
	}
}