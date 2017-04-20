using System.Data.Entity;
using Guests.Data.Context;
using SQLite.CodeFirst;

namespace Guests.Data.Migrations
{
	public class DatabaseInitializer : SqliteDropCreateDatabaseWhenModelChanges<GuestsContext>
	{
		public DatabaseInitializer(DbModelBuilder modelBuilder) : base(modelBuilder)
		{
		}
	}
}