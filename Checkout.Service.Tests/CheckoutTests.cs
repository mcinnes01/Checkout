using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Checkout.Service.Tests
{
	[TestClass]
	public class CheckoutTests
	{
		private ICheckoutService _checkoutService;

		[TestInitialize]
		public void StartUp()
		{
			_checkoutService = new CheckoutService();
		}

		[TestMethod]
		public void CheckoutWithNoItemsReturnsZero()
		{
			var result = _checkoutService.Checkout();

			Assert.IsTrue(!result.Items.Any(), "Items were returned and shouldn't be");
			Assert.AreEqual(0, result.Total, "Total should be zero");
		}

		[TestMethod]
		public void CheckoutWithItemsReturnsItemsWithTotal()
		{
			var result = _checkoutService.Checkout();

			Assert.IsTrue(result.Items.Any(), "No items were returned");
		}
	}
}
