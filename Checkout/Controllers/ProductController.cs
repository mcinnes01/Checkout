using System.Collections.Generic;
using System.Threading.Tasks;
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
		public async Task<IEnumerable<Product>> GetAlphabetically()
		{
			return await _productService.GetProductsAlphabetically();
		}

		// GET api/product/price
		[HttpGet]
		[Route("price")]
		public async Task<IEnumerable<Product>> GetByPrice()
		{
			return await _productService.GetProductsByPrice();
		}

		// GET api/product/apple
		[HttpGet("{product}")]
		public async Task<Product> Get(string product)
		{
			return await _productService.GetProductByName(product);
		}
	}
}
