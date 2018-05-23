using System.Collections.Generic;
using System.Linq;
using Checkout.Service.Models;
using Microsoft.Extensions.Options;

namespace Checkout.Service
{
	public class DiscountService : IDiscountService
	{
		private readonly IOptions<List<QuantityDiscount>> _discounts;

		public DiscountService(IOptions<List<QuantityDiscount>> discounts)
		{
			_discounts = discounts;
		}

		public IList<QuantityDiscount> GetDiscounts()
		{
			return _discounts.Value
				.OrderBy(o => o.Product)
				.ThenBy(o => o.Price)
				.ToList();
		}

		public IList<QuantityDiscount> GetDiscountsByProduct(string product)
		{
			return _discounts.Value
				.Where(d => d.Product == product)
				.OrderBy(o => o.Price)
				.ToList();
		}

		public IList<QuantityDiscount> GetEligibleDiscounts(string product, int quantity)
		{
			return _discounts.Value
				.Where(d => d.Product == product && d.Quantity <= quantity)
				.OrderBy(o => o.Price)
				.ToList();
		}

		public QuantityDiscount GetDiscountByProductQuantity(string product, int quantity)
		{
			return _discounts.Value
				.FirstOrDefault(d => d.Product == product && d.Quantity == quantity);
		}
	}
}
