using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Checkout.Service
{
	public class DiscountService : IDiscountService
	{
		private readonly IOptions<List<IDiscount>> _discounts;

		public DiscountService(IOptions<List<IDiscount>> discounts)
		{
			_discounts = discounts;
		}

		public async Task<IList<IDiscount>> GetDiscounts()
		{
			var discounts = _discounts.Value
				.OrderBy(o => o.Product)
				.ToList();

			return await Task.FromResult(discounts);
		}

		public async Task<IList<IDiscount>> GetDiscountsByProduct(string product)
		{
			var discounts = _discounts.Value
				.Where(d => d.Product == product)
				.ToList();

			return await Task.FromResult(discounts);
		}
	}
}
