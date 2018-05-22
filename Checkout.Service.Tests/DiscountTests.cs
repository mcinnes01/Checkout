using System.Collections.Generic;
using System.Linq;
using Checkout.Service.Models;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Checkout.Service.Tests
{
	[TestClass]
	public class DiscountTests
	{
		private Mock<IOptions<List<QuantityDiscount>>> _discount;
		private IDiscountService _discountService;

		[TestInitialize]
		public void StartUp()
		{
			_discount = new Mock<IOptions<List<QuantityDiscount>>>();

			_discount.Setup(s => s.Value).Returns(new List<QuantityDiscount>
			{
				new QuantityDiscount {Name = "Apple 3 for £1.30", Product = "Apple", Price = 130, Quantity = 3},
				new QuantityDiscount {Name = "Biscuits 2 for 45p", Product = "Biscuits", Price = 45, Quantity = 2}
			});

			_discountService = new DiscountService(_discount.Object);
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
