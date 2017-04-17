namespace Payments.Data.Models
{
	public class CreditCard
	{
		public int Id { get; set; }
		public string Number { get; set; }
		public int TypeId { get; set; }
		public string Expiration { get; set; }
		public virtual Payment Payment { get; set; }
	}
}