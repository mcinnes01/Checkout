using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkout.Service.Models;
using Microsoft.Extensions.Options;

namespace Checkout.Service
{
	public class ProductService : IProductService
	{
		private readonly IOptions<List<Product>> _products;

		public ProductService(IOptions<List<Product>> products)
		{
			_products = products;
		}

		public async Task<IList<Product>> GetProductsAlphabetically()
		{
			// Orders the products by Name ascending
			var products = _products.Value.OrderBy(p => p.Name).ToList();
			return await Task.FromResult(products);
		}

		public async Task<IList<Product>> GetProductsByPrice()
		{
			// Orders the products by UnitPrice ascending
			var products = _products.Value.OrderBy(p => p.UnitPrice).ToList();
			return await Task.FromResult(products);
		}

		public async Task<Product> GetProductByName(string name)
		{
			// Returns a product by name
			var product = _products.Value.SingleOrDefault(p => p.Name == name);
			return await Task.FromResult(product);
		}
	}
}
