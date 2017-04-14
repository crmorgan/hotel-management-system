using System.Data.Entity;
using System.Threading.Tasks;
using Reservations.Data.Migrations;
using Reservations.Data.Models;

namespace Reservations.Data.Context
{
	public interface IReservationsContext
	{
		IDbSet<Reservation> Reservations { get; set; }

		Task<int> SaveChangesAsync();
	}


	[DbConfigurationType(typeof(SqLiteConfig))]
	public class ReservationsContext : DbContext, IReservationsContext
	{
		public ReservationsContext() : base("hms.Reservations")
		{
		}

		public IDbSet<Reservation> Reservations { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			Database.SetInitializer(new DatabaseInitializer(modelBuilder));

			modelBuilder.Entity<Reservation>()
				.HasKey(p => p.Uuid);

			base.OnModelCreating(modelBuilder);
		}
	}
}