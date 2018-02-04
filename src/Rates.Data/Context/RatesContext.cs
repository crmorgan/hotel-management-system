using Rates.Data.Migrations;
using Rates.Data.Models;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Rates.Data.Context
{
	public interface IRatesContext
	{
		IDbSet<RoomRate> RoomRates { get; set; }
		IDbSet<Reservation> Reservations { get; set; }

		Task<int> SaveChangesAsync();
	}

	[DbConfigurationType(typeof(SqLiteConfig))]
	public class RatesContext : DbContext, IRatesContext
	{
		public RatesContext() : base("HMS.Rates")
        {
		}

		public IDbSet<RoomRate> RoomRates { get; set; }
		public IDbSet<Reservation> Reservations { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			Database.SetInitializer(new DatabaseInitializer(modelBuilder));

			modelBuilder.Entity<RoomRate>()
				.HasKey(p => p.Id);

			base.OnModelCreating(modelBuilder);
		}
	}
}