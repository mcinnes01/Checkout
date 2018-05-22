using System.Collections.Generic;
using System.Linq;
using Checkout.Service.Models;

namespace Checkout.Service
{
	public class DiscountService : IDiscountService
	{
		public IOrderedEnumerable<QuantityDiscount> GetDiscounts()
		{
			throw new System.NotImplementedException();
		}

		public IOrderedEnumerable<QuantityDiscount> GetDiscountsByProduct(string product)
		{
			throw new System.NotImplementedException();
		}

		public IOrderedEnumerable<QuantityDiscount> GetEligibleDiscounts(string product, int quantity)
		{
			throw new System.NotImplementedException();
		}

		public QuantityDiscount GetDiscountByProductQuantity(string product, int quantity)
		{
			throw new System.NotImplementedException();
		}
	}
}
