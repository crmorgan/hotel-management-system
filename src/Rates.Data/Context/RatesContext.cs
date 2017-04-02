using System.Data.Entity;
using System.Threading.Tasks;
using Rates.Data.Migrations;
using Rates.Data.Models;

namespace Rates.Data.Context
{
	public interface IRatesContext
	{
		IDbSet<RoomRate> RoomRates { get; set; }

		Task<int> SaveChangesAsync();
	}

	[DbConfigurationType(typeof(SqLiteConfig))]
	public class RatesContext : DbContext, IRatesContext
	{
		public RatesContext() : base("HMS.Rates")
        {
		}

		public IDbSet<RoomRate> RoomRates { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			Database.SetInitializer(new DatabaseInitializer(modelBuilder));

			modelBuilder.Entity<RoomRate>()
				.HasKey(p => p.Id);

			base.OnModelCreating(modelBuilder);
		}
	}
}