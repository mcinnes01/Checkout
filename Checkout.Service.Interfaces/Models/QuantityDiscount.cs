namespace Checkout.Service.Models
{
	public class QuantityDiscount : IQuantityDiscount
	{
		public string Name { get; set; }
		public string Product { get; set; }
		public int Quantity { get; set; }
		public int Price { get; set; }
	}
}
