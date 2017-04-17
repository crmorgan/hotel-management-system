using System.Data.Entity;
using Payments.Data.Context;
using SQLite.CodeFirst;

namespace Payments.Data.Migrations
{
	public class DatabaseInitializer : SqliteDropCreateDatabaseWhenModelChanges<PaymentsContext>
	{
		public DatabaseInitializer(DbModelBuilder modelBuilder) : base(modelBuilder)
		{
		}
	}
}