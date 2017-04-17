using Payments.Data.Models;

namespace Payments.Messages.Commands
{
	public class SubmitPaymentCommand
	{
		public Payment Payment { get; set; }
	}
}