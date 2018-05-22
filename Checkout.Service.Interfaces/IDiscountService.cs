using System.Linq;
using Checkout.Service.Models;

namespace Checkout.Service
{
	public interface IDiscountService
	{
		IOrderedEnumerable<QuantityDiscount> GetDiscounts();

		IOrderedEnumerable<QuantityDiscount> GetDiscountsByProduct(string product);

		IOrderedEnumerable<QuantityDiscount> GetEligibleDiscounts(string product, int quantity);

		QuantityDiscount GetDiscountByProductQuantity(string product, int quantity);
	}
}
