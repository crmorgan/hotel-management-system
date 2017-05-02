using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Threading.Tasks;
using Finance.Data.Migrations;
using Finance.Data.Models;

namespace Finance.Data.Context
{
	public interface IFinanceContext
	{
		IDbSet<Invoice> Invoices { get; set; }
		IDbSet<LineItem> LineItems { get; set; }

		Task<int> SaveChangesAsync();
	}


	[DbConfigurationType(typeof(SqLiteConfig))]
	public class FinanceContext : DbContext, IFinanceContext
	{
		public FinanceContext() : base("HMS.Finance")
		{
		}

		public IDbSet<Invoice> Invoices { get; set; }
		public IDbSet<LineItem> LineItems { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			Database.SetInitializer(new DatabaseInitializer(modelBuilder));

			modelBuilder.Entity<Invoice>()
				.HasKey(p => p.Id)
				.Property(p => p.Id)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

			modelBuilder.Entity<Invoice>()
				.Ignore(p => p.Total);

			modelBuilder.Entity<LineItem>()
				.HasKey(p => p.Id);

			modelBuilder.Entity<LineItem>()
				.Ignore(p => p.Extension);


			base.OnModelCreating(modelBuilder);
		}
	}
}