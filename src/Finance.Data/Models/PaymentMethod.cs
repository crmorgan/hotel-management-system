namespace Finance.Data.Models
{
	public class PaymentMethod
	{
		public string Id { get; set; }
		public string PurchaseUuid { get; set; }
		public CreditCard CreditCard { get; set; }
	}
}