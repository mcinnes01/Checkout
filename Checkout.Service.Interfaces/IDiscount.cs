namespace Checkout.Service
{
	public interface IDiscount
	{
		string Name { get; set; }
		string Product { get; set; }
	}
}
