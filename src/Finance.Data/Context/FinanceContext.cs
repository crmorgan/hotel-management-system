using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Threading.Tasks;
using Finance.Data.Models;
using Finance.Data.Migrations;

namespace Finance.Data.Context
{
	public interface IFinanceContext
	{
		IDbSet<PaymentMethod> PaymentMethods { get; set; }
		Task<int> SaveChangesAsync();
	}

	[DbConfigurationType(typeof(SqLiteConfig))]
	public class FinanceContext : DbContext, IFinanceContext
	{
		public FinanceContext() : base("hms.Finance")
		{
		}

		public IDbSet<PaymentMethod> PaymentMethods { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			Database.SetInitializer(new DatabaseInitializer(modelBuilder));

			modelBuilder.Entity<PaymentMethod>()
				.HasKey(p => p.Id)
				.Property(p => p.Id)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

			modelBuilder.ComplexType<CreditCard>();
			base.OnModelCreating(modelBuilder);
		}
	}
}