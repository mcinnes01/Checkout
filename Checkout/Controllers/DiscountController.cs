using System.Collections.Generic;
using System.Threading.Tasks;
using Checkout.Service;
using Checkout.Service.Models;
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
		public async Task<IList<QuantityDiscount>> GetDiscounts()
		{
			return await _discountService.GetDiscounts();
		}

		// GET api/discount/apple
		[HttpGet]
		[Route("{product}")]
		public async Task<IList<QuantityDiscount>> GetDiscountsByProduct(string product)
		{
			return await _discountService.GetDiscountsByProduct(product);
		}

		// GET api/discount/apple/3
		[HttpGet("{product}/eligible/{quantity}")]
		public async Task<IList<QuantityDiscount>> GetEligableDiscount(string product, int quantity)
		{
			return await _discountService.GetEligibleDiscounts(product, quantity);
		}

		// GET api/discount/apple/3
		[HttpGet("{product}/quantity/{quantity}")]
		public async Task<QuantityDiscount> Get(string product, int quantity)
		{
			return await _discountService.GetDiscountByProductQuantity(product, quantity);
		}
	}
}
