using System;
using Rates.SharedKernel;

namespace Rates.Data.Models
{
	public class RoomRate
	{
		public int Id{ get; set; }
		public int RoomTypeId{ get; set; }
		public decimal Amount { get; set; }
		public CurrencyCodes CurrencyCode { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
	}
}