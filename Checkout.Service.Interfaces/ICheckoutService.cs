using System.Threading.Tasks;
using Checkout.Service.Models;

namespace Checkout.Service
{
	public interface ICheckoutService
	{
		Task<CheckoutBasket> Checkout();
	}
}
