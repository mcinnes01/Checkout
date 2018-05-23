using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

		public async Task<IList<QuantityDiscount>> GetDiscounts()
		{
			var discounts = _discounts.Value
				.OrderBy(o => o.Product)
				.ThenBy(o => o.Price)
				.ToList();

			return await Task.FromResult(discounts);
		}

		public async Task<IList<QuantityDiscount>> GetDiscountsByProduct(string product)
		{
			var discounts = _discounts.Value
				.Where(d => d.Product == product)
				.OrderBy(o => o.Price)
				.ToList();

			return await Task.FromResult(discounts);
		}

		public async Task<IList<QuantityDiscount>> GetEligibleDiscounts(string product, int quantity)
		{
			var discounts = _discounts.Value
				.Where(d => d.Product == product && d.Quantity <= quantity)
				.OrderBy(o => o.Price)
				.ToList();

			return await Task.FromResult(discounts);
		}

		public async Task<QuantityDiscount> GetDiscountByProductQuantity(string product, int quantity)
		{
			var discount = _discounts.Value
				.FirstOrDefault(d => d.Product == product && d.Quantity == quantity);

			return await Task.FromResult(discount);
		}
	}
}
