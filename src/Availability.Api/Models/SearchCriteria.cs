using System.ComponentModel.DataAnnotations;

namespace Availability.Api.Models
{
	public class SearchCriteria
	{
		[Required]
		public DateRange Dates { get; set; }
	}
}