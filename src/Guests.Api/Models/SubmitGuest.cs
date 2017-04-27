using System.ComponentModel.DataAnnotations;

namespace Guests.Api.Models
{
	public class SubmitGuest
	{
		[Required]
		public string GuestUuid { get; set; }

		[Required]
		public string ReservationUuid { get; set; }

		public string Title { get; set; }

		[Required]
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }

		[Required, EmailAddress]
		public string Email { get; set; }

		[Required]
		public Address Address { get; set; }
	}

	public class Address
	{
		[Required]
		public string Line1 { get; set; }
		public string Line2 { get; set; }

		[Required]
		public string City { get; set; }

		[Required]
		public string State { get; set; }

		[Required]
		public string Zip { get; set; }
	}
}