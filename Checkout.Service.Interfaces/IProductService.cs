using System.Collections.Generic;
using System.Threading.Tasks;
using Checkout.Service.Models;

namespace Checkout.Service
{
	public interface IProductService
	{
		Task<IList<Product>> GetProductsAlphabetically();

		Task<IList<Product>> GetProductsByPrice();

		Task<Product> GetProductByName(string name);
	}
}
