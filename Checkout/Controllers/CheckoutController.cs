using System.Threading.Tasks;
using Checkout.Service;
using Checkout.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.Controllers
{
	[Route("api/[controller]")]
	public class CheckoutController : Controller
	{
		private readonly ICheckoutService _checkoutService;

		public CheckoutController(ICheckoutService checkoutService)
		{
			_checkoutService = checkoutService;
		}

		// Post api/checkout
		[HttpPost]
		public async Task<CheckoutBasket> Checkout()
		{
			return await _checkoutService.Checkout();
		}
	}
}
