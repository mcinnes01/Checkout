using System.Collections.Generic;
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
		public IList<QuantityDiscount> GetDiscounts()
		{
			return _discountService.GetDiscounts();
		}

		// GET api/discount/apple
		[HttpGet]
		[Route("{product}")]
		public IList<QuantityDiscount> GetDiscountsByProduct(string product)
		{
			return _discountService.GetDiscountsByProduct(product);
		}

		// GET api/discount/apple/3
		[HttpGet("{product}/eligible/{quantity}")]
		public IList<QuantityDiscount> GetEligableDiscount(string product, int quantity)
		{
			return _discountService.GetEligibleDiscounts(product, quantity);
		}

		// GET api/discount/apple/3
		[HttpGet("{product}/quantity/{quantity}")]
		public QuantityDiscount Get(string product, int quantity)
		{
			return _discountService.GetDiscountByProductQuantity(product, quantity);
		}
	}
}
