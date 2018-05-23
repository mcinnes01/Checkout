using System.Collections.Generic;
using System.Linq;
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

		public IList<Product> GetProductsAlphabetically()
		{
			// Orders the products by Name ascending
			return _products.Value.OrderBy(p => p.Name).ToList();
		}

		public IList<Product> GetProductsByPrice()
		{
			// Orders the products by UnitPrice ascending
			return _products.Value.OrderBy(p => p.UnitPrice).ToList();
		}

		public Product GetProductByName(string name)
		{
			// Returns a product by name
			return _products.Value.SingleOrDefault(p => p.Name == name);
		}
	}
}
