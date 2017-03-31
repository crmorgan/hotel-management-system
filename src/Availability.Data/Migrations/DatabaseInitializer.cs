using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Availability.Data.Context;
using Availability.Data.Models;
using SQLite.CodeFirst;

namespace Availability.Data.Migrations
{
	public class DatabaseInitializer : SqliteCreateDatabaseIfNotExists<RoomAvailabilityContext>
	{
		public DatabaseInitializer(DbModelBuilder modelBuilder) : base(modelBuilder)
		{
		}

		protected override void Seed(RoomAvailabilityContext context)
		{
			context.RoomTypeAvailability.AddOrUpdate(k => k.Id, SeedData.GetOneYearAvailability(1).ToArray());
			context.RoomTypeAvailability.AddOrUpdate(k => k.Id, SeedData.GetOneYearAvailability(2).ToArray());
			context.RoomTypeAvailability.AddOrUpdate(k => k.Id, SeedData.GetOneYearAvailability(3).ToArray());
			context.RoomTypeAvailability.AddOrUpdate(k => k.Id, SeedData.GetOneYearAvailability(4).ToArray());

			base.Seed(context);
		}
	}

	internal static class SeedData
	{
		internal static IEnumerable<RoomTypeAvailability> GetOneYearAvailability(int roomTypeId)
		{
			for (var i = 0; i < 366; i++)
			{
				yield return new RoomTypeAvailability
				{
					RoomTypeId = roomTypeId,
					Date = DateTime.Today.AddDays(i),
					IsAvailable = true
				};
			}
		}
	}
}