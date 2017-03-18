using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using RoomOccupancy.Data.Context;
using RoomOccupancy.Data.Models;
using SQLite.CodeFirst;

namespace RoomOccupancy.Data.Migrations
{
	public class DatabaseInitializer : SqliteCreateDatabaseIfNotExists<RoomOccupancyContext>
	{
		public DatabaseInitializer(DbModelBuilder modelBuilder) : base(modelBuilder)
		{
		}

		protected override void Seed(RoomOccupancyContext context)
		{
			context.RoomTypes.AddOrUpdate(k => k.Id, SeedData.RoomTypes().ToArray());
			base.Seed(context);
		}
	}

	internal static class SeedData
	{
		internal static List<RoomType> RoomTypes()
		{
			return new List<RoomType>
			{
				new RoomType { Id = 1, Name = "Double", NumAvailable = 34},
				new RoomType { Id = 2, Name = "Queen", NumAvailable = 28},
				new RoomType { Id = 3, Name = "King", NumAvailable = 40},
				new RoomType { Id = 4, Name = "King Suite", NumAvailable = 3}
			};
		}
	}
}