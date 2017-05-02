namespace Finance.Data.Models
{
	public class LineItem
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public string Type { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public decimal Extension => Quantity * Price;
	}
}