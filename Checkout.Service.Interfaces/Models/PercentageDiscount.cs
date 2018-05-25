namespace Checkout.Service.Models
{
	public class PercentageDiscount : IDiscount
	{
		public string Type => GetType().Name;
		public string Name { get; set; }
		public string Product { get; set; }
		public int Percentage { get; set; }

		public CheckoutEntry Process(BasketItem item, IDiscount discount)
		{
			var percentageDiscount = discount as PercentageDiscount;
			var entry = new CheckoutEntry
			{
				Item = new CheckoutItem
				{
					Product = item.Product.Name,
					Quantity = item.Quantity,
					Price = item.Quantity * item.Product.UnitPrice
				}
			};

			if (percentageDiscount == null)
				return entry;

			entry.Item.Discount = percentageDiscount;
			entry.Item.Price = entry.Item.Price * (100 - percentageDiscount.Percentage);
			return entry;
		}
	}
}
