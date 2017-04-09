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
				SeedData.GetCollateral(1, "Standard Room", "/pub/media/1/1a.jpg").ToArray());
			context.RoomTypeCollateral.AddOrUpdate(k => k.Id,
				SeedData.GetCollateral(2, "Two Queen Beds", "/pub/media/2/2a.jpg").ToArray());
			context.RoomTypeCollateral.AddOrUpdate(k => k.Id,
				SeedData.GetCollateral(3, "1 King Executive", "/pub/media/3/3a.jpg").ToArray());
			context.RoomTypeCollateral.AddOrUpdate(k => k.Id,
				SeedData.GetCollateral(4, "1 King Bed Suite", "/pub/media/4/4a.jpg").ToArray());

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