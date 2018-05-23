namespace Checkout.Service.Models
{
	public class CheckoutItem
	{
		public string Product { get; set; }
		public int Quantity { get; set; }
		public QuantityDiscount Discount { get; set; }
		public int Price { get; set; }
	}
}
