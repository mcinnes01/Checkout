using System.Collections.Generic;

namespace Checkout.Service.Models
{
	public class CheckoutBasket
	{
		public List<CheckoutItem> Items { get; set; } = new List<CheckoutItem>();
		public int Total { get; set; } = 0;
	}
}
