using System;

namespace Checkout.Service.Models
{
	public class QuantityDiscount : IDiscount
	{
		public string Type => GetType().Name;
		public string Name { get; set; }
		public string Product { get; set; }
		public int Quantity { get; set; }
		public int Price { get; set; }

		public CheckoutEntry Process(BasketItem item, IDiscount discount)
		{
			var quantityDiscount = discount as QuantityDiscount;
			var entry = new CheckoutEntry
			{
				Item = new CheckoutItem
				{
					Product = item.Product.Name,
					Quantity = item.Quantity,
					Price = item.Quantity * item.Product.UnitPrice
				}
			};

			if (quantityDiscount == null)
				return entry;

			var quantity = Math.DivRem(item.Quantity, quantityDiscount.Quantity, out var remainder);
			entry.Item.Discount = quantityDiscount;
			entry.Item.Quantity = quantity;
			entry.Item.Price = quantityDiscount.Price * quantity;
			entry.Remainder = remainder;
			return entry;
		}
	}
}
