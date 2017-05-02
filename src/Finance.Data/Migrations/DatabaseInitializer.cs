using System.Data.Entity;
using Finance.Data.Context;
using SQLite.CodeFirst;

namespace Finance.Data.Migrations
{
	public class DatabaseInitializer : SqliteDropCreateDatabaseWhenModelChanges<FinanceContext>
	{
		public DatabaseInitializer(DbModelBuilder modelBuilder) : base(modelBuilder)
		{
		}
	}
}