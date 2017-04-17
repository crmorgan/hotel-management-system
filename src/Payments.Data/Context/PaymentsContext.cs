using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Threading.Tasks;
using Payments.Data.Migrations;
using Payments.Data.Models;

namespace Payments.Data.Context
{
	public interface IPaymentsContext
	{
		IDbSet<Payment> Payments { get; set; }
		IDbSet<CreditCard> CreditCards { get; set; }
		Task<int> SaveChangesAsync();
	}

	[DbConfigurationType(typeof(SqLiteConfig))]
	public class PaymentsContext : DbContext, IPaymentsContext
	{
		public PaymentsContext() : base("hms.Payments")
		{
		}

		public IDbSet<Payment> Payments { get; set; }
		public IDbSet<CreditCard> CreditCards { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			Database.SetInitializer(new DatabaseInitializer(modelBuilder));

			modelBuilder.Entity<Payment>()
				.HasKey(p => p.Id)
				.Property(p => p.Id)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

			modelBuilder.Entity<CreditCard>()
				.HasKey(p => p.Id);

			modelBuilder.Entity<Payment>()
				.HasRequired(p => p.Card)
				.WithRequiredPrincipal(p => p.Payment);

			base.OnModelCreating(modelBuilder);
		}
	}
}