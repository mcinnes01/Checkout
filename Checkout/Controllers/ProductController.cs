using System.Collections.Generic;
using Checkout.Service;
using Checkout.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.Controllers
{
	[Route("api/[controller]")]
	public class ProductController : Controller
	{
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}


		// GET api/product
		[HttpGet]
		public IEnumerable<Product> GetAlphabetically()
		{
			return _productService.GetProductsAlphabetically();
		}

		// GET api/product/price
		[HttpGet]
		[Route("price")]
		public IEnumerable<Product> GetByPrice()
		{
			return _productService.GetProductsByPrice();
		}

		// GET api/product/apple
		[HttpGet("{product}")]
		public Product Get(string product)
		{
			return _productService.GetProductByName(product);
		}
	}
}
