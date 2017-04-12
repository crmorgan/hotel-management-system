using System.ComponentModel.DataAnnotations;
using SharedKernel;

namespace Availability.Api.Models
{
	public class SearchCriteria
	{
		[Required]
		public DateRange Dates { get; set; }
	}
}