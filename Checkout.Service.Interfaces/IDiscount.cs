using Checkout.Service.Models;

namespace Checkout.Service
{
	public interface IDiscount
	{
		string Type { get; }
		string Name { get; set; }
		string Product { get; set; }
		CheckoutEntry Process(BasketItem item, IDiscount discount);
	}
}
