using System.Data.Entity;
using System.Threading.Tasks;
using Marketing.Data.Migrations;
using Marketing.Data.Models;

namespace Marketing.Data.Context
{
	public interface IMarketingContext
	{
		IDbSet<RoomTypeCollateral> RoomTypeCollateral { get; set; }

		Task<int> SaveChangesAsync();
	}

	[DbConfigurationType(typeof(SqLiteConfig))]
	public class MarketingContext : DbContext, IMarketingContext
	{
		public MarketingContext() : base("HMS.Marketing")
        {
		}

		public IDbSet<RoomTypeCollateral> RoomTypeCollateral { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			Database.SetInitializer(new DatabaseInitializer(modelBuilder));

			modelBuilder.Entity<RoomTypeCollateral>()
				.HasKey(p => p.Id);

			base.OnModelCreating(modelBuilder);
		}
	}
}