using Checkout.Service.Models;

namespace Checkout.Service
{
	public interface ICheckoutService
	{
		CheckoutBasket Checkout();
	}
}
