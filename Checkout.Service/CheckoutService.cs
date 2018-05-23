﻿using System;
using System.Linq;
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

		public CheckoutBasket Checkout()
		{
			var checkoutBasket = new CheckoutBasket();
			var items = _basketService.GetBasketContents();

			if (!items.Any())
				return checkoutBasket;

			foreach (var item in items)
			{
				var discounts = _discountService.GetEligibleDiscounts(item.Product.Name, item.Quantity).ToList();
				if (discounts.Any())
				{
					var discount = discounts.First();
					var discountQuantity = Math.DivRem(item.Quantity, discount.Quantity, out var remainder);

					checkoutBasket.Items.Add(new CheckoutItem
					{
						Product = item.Product.Name,
						Discount = discount,
						Quantity = discountQuantity,
						Price = discountQuantity * discount.Price
					});

					item.Quantity = remainder;
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
			_basketService.EmptyBasket();

			return checkoutBasket;
		}
	}
}
