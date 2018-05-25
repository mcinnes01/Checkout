using System.Collections.Generic;
using System.Threading.Tasks;
using Checkout.Service;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.Controllers
{
	[Route("api/[controller]")]
	public class DiscountController : Controller
	{
		private readonly IDiscountService _discountService;

		public DiscountController(IDiscountService discountService)
		{
			_discountService = discountService;
		}


		// GET api/discount
		[HttpGet]
		public async Task<IList<IDiscount>> GetDiscounts()
		{
			return await _discountService.GetDiscounts();
		}

		// GET api/discount/apple
		[HttpGet]
		[Route("{product}")]
		public async Task<IList<IDiscount>> GetDiscountsByProduct(string product)
		{
			return await _discountService.GetDiscountsByProduct(product);
		}
	}
}
