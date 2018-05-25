namespace Checkout.Service.Models
{
	public class CheckoutEntry
	{
		public CheckoutItem Item { get; set; }
		public int Remainder { get; set; } = 0;
	}
}
