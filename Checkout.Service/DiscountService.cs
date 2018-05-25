using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkout.Service.Models;
using Microsoft.Extensions.Options;

namespace Checkout.Service
{
	public class DiscountService : IDiscountService
	{
		private readonly List<IDiscount> _discounts;

		public DiscountService(List<IDiscount> discounts)
		{
			_discounts = discounts;
		}

		public async Task<IList<IDiscount>> GetDiscounts()
		{
			var discounts = _discounts
				.OrderBy(o => o.Product)
				.ToList();

			return await Task.FromResult(discounts);
		}

		public async Task<IList<IDiscount>> GetDiscountsByProduct(string product)
		{
			var discounts = _discounts
				.Where(d => d.Product == product)
				.ToList();

			return await Task.FromResult(discounts);
		}
	}
}
