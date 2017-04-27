using NServiceBus;

namespace Guests.Messages.Commands
{
	public class SubmitGuestCommand : ICommand
	{
		public string GuestUuid { get; set; }
		public string ReservationUuid { get; set; }
		public string Title { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public Address Address { get; set; }
	}

	public class Address
	{
		public string Line1 { get; set; }
		public string Line2 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }
	}
}