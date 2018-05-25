using System.Linq;
using System.Threading.Tasks;
using Checkout.Service.Models;

namespace Checkout.Service
{
	public class CheckoutService : ICheckoutService
	{
		private readonly IBasketService _basketService;
		private readonly IDiscountService _discountService;

		public CheckoutService(IBasketService basketService, IDiscountService discountService)
		{
			_basketService = basketService;
			_discountService = discountService;
		}

		public async Task<CheckoutBasket> Checkout()
		{
			var checkoutBasket = new CheckoutBasket();
			var items = await _basketService.GetBasketContents();

			if (!items.Any())
				return checkoutBasket;

			foreach (var item in items)
			{
				var discounts = (await _discountService.GetDiscountsByProduct(item.Product.Name)).ToList();
				if (discounts.Any())
				{
					foreach (var discount in discounts)
					{
						if (item.Quantity <= 0)
							break;

						var entry = discount.Process(item, discount);
						checkoutBasket.Items.Add(entry.Item);

						item.Quantity = entry.Remainder;
					}
				}

				if (item.Quantity > 0)
				{
					checkoutBasket.Items.Add(new CheckoutItem
					{
						Product = item.Product.Name,
						Quantity = item.Quantity,
						Price = item.Quantity * item.Product.UnitPrice
					});
				}
			}

			checkoutBasket.Total = checkoutBasket.Items.Sum(t => t.Price);

			// Clean up basket
			await _basketService.EmptyBasket();

			return checkoutBasket;
		}
	}
}

