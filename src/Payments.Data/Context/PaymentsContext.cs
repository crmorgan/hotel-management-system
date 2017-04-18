using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Threading.Tasks;
using Payments.Data.Migrations;
using Payments.Data.Models;

namespace Payments.Data.Context
{
	public interface IPaymentsContext
	{
		IDbSet<PaymentMethod> PaymentMethods { get; set; }
		Task<int> SaveChangesAsync();
	}

	[DbConfigurationType(typeof(SqLiteConfig))]
	public class PaymentsContext : DbContext, IPaymentsContext
	{
		public PaymentsContext() : base("hms.Payments")
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