namespace Finance.Data.Models
{
	public class CreditCard
	{
		public int Id { get; set; }
		public string CardHolderName { get; set; }
		public string AccountNumberLast4 { get; set; }
		public int TypeId { get; set; }
		public string Expiration { get; set; }
	}
}