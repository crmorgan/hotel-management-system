using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Rates.Data.Context;
using Rates.Data.Models;
using Rates.SharedKernel;
using SQLite.CodeFirst;

namespace Rates.Data.Migrations
{
	public class DatabaseInitializer : SqliteCreateDatabaseIfNotExists<RatesContext>
	{
		public DatabaseInitializer(DbModelBuilder modelBuilder) : base(modelBuilder)
		{
		}

		protected override void Seed(RatesContext context)
		{
			context.RoomRates.AddOrUpdate(k => k.Id, SeedData.GetOneYearOfRates(1).ToArray());
			context.RoomRates.AddOrUpdate(k => k.Id, SeedData.GetOneYearOfRates(2).ToArray());
			context.RoomRates.AddOrUpdate(k => k.Id, SeedData.GetOneYearOfRates(3).ToArray());
			context.RoomRates.AddOrUpdate(k => k.Id, SeedData.GetOneYearOfRates(4).ToArray());

			base.Seed(context);
		}
	}

	internal static class SeedData
	{
		internal static IEnumerable<RoomRate> GetOneYearOfRates(int roomTypeId)
		{
				yield return new RoomRate
				{
					RoomTypeId = roomTypeId,
					Amount = roomTypeId * 20m + 100.99m,
					CurrencyCode = CurrencyCodes.USD,
					StartDate = DateTime.Today,
					EndDate = DateTime.Today.AddYears(1)
				};
		}
	}
}