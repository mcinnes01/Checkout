using System.Collections.Generic;
using System.Threading.Tasks;
using Checkout.Service.Models;

namespace Checkout.Service
{
	public interface IDiscountService
	{
		Task<IList<QuantityDiscount>> GetDiscounts();

		Task<IList<QuantityDiscount>> GetDiscountsByProduct(string product);

		Task<IList<QuantityDiscount>> GetEligibleDiscounts(string product, int quantity);

		Task<QuantityDiscount> GetDiscountByProductQuantity(string product, int quantity);
	}
}
