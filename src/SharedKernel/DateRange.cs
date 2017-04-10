using System;

namespace SharedKernel
{
	public class DateRange
	{
		public DateRange()
		{
		}

		public DateRange(DateTime startDate, DateTime endDate)
		{
			StartDate = startDate;
			EndDate = endDate;
		}

		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
	}
}