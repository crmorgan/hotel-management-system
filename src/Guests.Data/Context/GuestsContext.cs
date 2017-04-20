using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Threading.Tasks;
using Guests.Data.Migrations;
using Guests.Data.Models;

namespace Guests.Data.Context
{
	public interface IGuestsContext
	{
		IDbSet<Guest> Guests { get; set; }
		Task<int> SaveChangesAsync();
	}

	[DbConfigurationType(typeof(SqLiteConfig))]
	public class GuestsContext : DbContext, IGuestsContext
	{
		public GuestsContext() : base("hms.Guests")
		{
		}

		public IDbSet<Guest> Guests { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			Database.SetInitializer(new DatabaseInitializer(modelBuilder));

			modelBuilder.Entity<Guest>()
				.HasKey(p => p.Id)
				.Property(p => p.Id)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

			modelBuilder.ComplexType<Address>();
			
			base.OnModelCreating(modelBuilder);
		}
	}
}