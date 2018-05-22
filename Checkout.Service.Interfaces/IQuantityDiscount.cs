namespace Checkout.Service
{
	public interface IQuantityDiscount : IDiscount
	{
		int Quantity { get; set; }
		int Price { get; set; }
	}
}
