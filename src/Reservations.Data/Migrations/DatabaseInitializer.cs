using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Reservations.Data.Context;
using Reservations.Data.Models;
using SharedKernel;
using SQLite.CodeFirst;

namespace Reservations.Data.Migrations
{
	public class DatabaseInitializer : SqliteCreateDatabaseIfNotExists<ReservationsContext>
	{
		public DatabaseInitializer(DbModelBuilder modelBuilder) : base(modelBuilder)
		{
		}

		protected override void Seed(ReservationsContext context)
		{
			context.Reservations.AddOrUpdate(k => k.Id, SeedData.GetReservations(1).ToArray());
			context.Reservations.AddOrUpdate(k => k.Id, SeedData.GetReservations(2).ToArray());

			base.Seed(context);
		}
	}

	internal static class SeedData
	{
		internal static IEnumerable<Reservation> GetReservations(int roomTypeId)
		{
			yield return new Reservation
			{
				Id = Guid.NewGuid(),
				RoomTypeId = roomTypeId,
				Dates = new DateRange(DateTime.Today.AddDays(14), DateTime.Today.AddDays(18))
			};
		}
	}
}