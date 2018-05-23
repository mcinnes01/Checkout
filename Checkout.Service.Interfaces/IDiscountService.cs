using System.Collections.Generic;
using System.Linq;
using Checkout.Service.Models;

namespace Checkout.Service
{
	public interface IDiscountService
	{
		IList<QuantityDiscount> GetDiscounts();

		IList<QuantityDiscount> GetDiscountsByProduct(string product);

		IList<QuantityDiscount> GetEligibleDiscounts(string product, int quantity);

		QuantityDiscount GetDiscountByProductQuantity(string product, int quantity);
	}
}
