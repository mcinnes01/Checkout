using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Checkout.Service.Tests
{
	[TestClass]
	public class DiscountTests
	{
		private IDiscountService _discountService;

		[TestInitialize]
		public void StartUp()
		{
			_discountService = new DiscountService();
		}

		[TestMethod]
		public void GetDiscountsReturnsAtLeastOneDiscount()
		{
			var result = _discountService.GetDiscounts();

			Assert.IsTrue(result.Any(), "No discounts were returned");
		}

		[TestMethod]
		public void GetDiscountsByProductReturnsAtLeastOneDiscount()
		{
			var result = _discountService.GetDiscountsByProduct("Apple");

			Assert.IsTrue(result.Any(), "No discounts for apple were returned");
		}

		[TestMethod]
		public void GetDiscountByProductQuantityReturnsTheFirstDiscountThatMatches()
		{
			var result = _discountService.GetDiscountByProductQuantity("Apple", 3);

			Assert.IsNotNull(result, "No discounts were returned");
		}

		[TestMethod]
		public void GetProductsByPriceReturnsBiscuitPriceFirst()
		{
			var result = _discountService.GetEligibleDiscounts("Apple", 9);

			Assert.IsNotNull(result, "No eligible discounts were returned");
		}
	}
}
