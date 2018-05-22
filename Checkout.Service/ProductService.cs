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
			return _products.Value.OrderBy(p => p.Name).ToList();
		}

		public IList<Product> GetProductsByPrice()
		{
			return _products.Value.OrderBy(p => p.UnitPrice).ToList();
		}

		public Product GetProductByName(string name)
		{
			return _products.Value.SingleOrDefault(p => p.Name == name);
		}
	}
}
