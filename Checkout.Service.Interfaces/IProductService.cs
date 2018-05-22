using System.Collections.Generic;
using Checkout.Service.Models;

namespace Checkout.Service
{
	public interface IProductService
	{
		IList<Product> GetProductsAlphabetically();

		IList<Product> GetProductsByPrice();

		Product GetProductByName(string name);
	}
}
