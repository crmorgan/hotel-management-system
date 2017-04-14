using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Marketing.Data.Context;
using Marketing.Data.Models;
using SQLite.CodeFirst;

namespace Marketing.Data.Migrations
{
	public class DatabaseInitializer : SqliteCreateDatabaseIfNotExists<MarketingContext>
	{
		public DatabaseInitializer(DbModelBuilder modelBuilder) : base(modelBuilder)
		{
		}

		protected override void Seed(MarketingContext context)
		{
			context.RoomTypeCollateral.AddOrUpdate(k => k.Id,
				SeedData.GetCollateral(1, "Standard Room", "src/pub/media/standard.jpg").ToArray());
			context.RoomTypeCollateral.AddOrUpdate(k => k.Id,
				SeedData.GetCollateral(2, "Two Queen Beds", "src/pub/media/queen.jpg").ToArray());
			context.RoomTypeCollateral.AddOrUpdate(k => k.Id,
				SeedData.GetCollateral(3, "1 King Executive", "src/pub/media/king.jpg").ToArray());
			context.RoomTypeCollateral.AddOrUpdate(k => k.Id,
				SeedData.GetCollateral(4, "1 King Bed Suite", "src/pub/media/king-suite.jpg").ToArray());

			base.Seed(context);
		}
	}

	internal static class SeedData
	{
		internal static IEnumerable<RoomTypeCollateral> GetCollateral(int roomTypeId, string desc, string imageUrl)
		{
			yield return new RoomTypeCollateral
			{
				RoomTypeId = roomTypeId,
				Description = desc,
				ImageUrl = imageUrl
			};
		}
	}
}