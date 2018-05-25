using System.Collections.Generic;
using System.Threading.Tasks;

namespace Checkout.Service
{
	public interface IDiscountService
	{
		Task<IList<IDiscount>> GetDiscounts();

		Task<IList<IDiscount>> GetDiscountsByProduct(string product);
	}
}
