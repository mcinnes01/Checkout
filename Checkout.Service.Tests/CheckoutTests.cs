using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkout.Service.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Checkout.Service.Tests
{
	[TestClass]
	public class CheckoutTests
	{
		private Mock<IBasketService> _basketService;
		private Mock<IDiscountService> _discountService;
		private ICheckoutService _checkoutService;

		[TestInitialize]
		public void StartUp()
		{
			_basketService = new Mock<IBasketService>();
			_discountService = new Mock<IDiscountService>();

			_checkoutService = new CheckoutService(_basketService.Object, _discountService.Object);
		}

		[TestMethod]
		public async Task CheckoutWithNoItemsReturnsZero()
		{
			// Setup Test
			_basketService.Setup(s => s.GetBasketContents()).ReturnsAsync(new List<BasketItem>());

			var result = await _checkoutService.Checkout();
			Assert.IsTrue(!result.Items.Any(), "Items were returned and shouldn't be");
			Assert.AreEqual(0, result.Total, "Total should be zero");
		}

		[TestMethod]
		public async Task CheckoutWithItemsAndNoDiscountsReturnsItemsWithTotal()
		{
			// Setup Test
			_basketService.Setup(s => s.GetBasketContents()).ReturnsAsync(new List<BasketItem>
			{
				new BasketItem {Product = new Product {Name = "Apple", UnitPrice = 50}, Quantity = 7},
				new BasketItem {Product = new Product {Name = "Biscuits", UnitPrice = 30}, Quantity = 6},
				new BasketItem {Product = new Product {Name = "Coffee", UnitPrice = 180}, Quantity = 3}
			});
			_discountService.Setup(s => s.GetEligibleDiscounts(It.IsAny<string>(), It.IsAny<int>()))
				.ReturnsAsync(new List<QuantityDiscount>());

			var result = await _checkoutService.Checkout();

			Assert.IsTrue(result.Items.Any(), "No items were returned");
			Assert.AreEqual(3, result.Items.Count, "Incorrect number of items returned");
			Assert.AreEqual(1070, result.Total, "Total doesn't match expected amount");
		}

		[TestMethod]
		public async Task CheckoutWithItemsAndNoDiscountsReturnsItemsWithDiscountedTotalAndSeparateDiscountItems()
		{
			// Setup Test
			_basketService.Setup(s => s.GetBasketContents()).ReturnsAsync(new List<BasketItem>
			{
				new BasketItem {Product = new Product {Name = "Apple", UnitPrice = 50}, Quantity = 7},
				new BasketItem {Product = new Product {Name = "Biscuits", UnitPrice = 30}, Quantity = 6},
				new BasketItem {Product = new Product {Name = "Coffee", UnitPrice = 180}, Quantity = 3}
			});
			_discountService.Setup(s => s.GetEligibleDiscounts("Apple", It.IsInRange(3, 10, Range.Inclusive)))
				.ReturnsAsync(new List<QuantityDiscount>
				{
					new QuantityDiscount { Name ="Apple 3 for £1.30", Price = 130, Product = "Apple", Quantity = 3}
				});
			_discountService.Setup(s => s.GetEligibleDiscounts("Biscuits", It.IsInRange(2, 10, Range.Inclusive)))
				.ReturnsAsync(new List<QuantityDiscount>
				{
					new QuantityDiscount { Name ="Biscuits 2 for 45p", Price = 45, Product = "Biscuits", Quantity = 2}
				});
			_discountService.Setup(s => s.GetEligibleDiscounts("Coffee", It.IsAny<int>()))
				.ReturnsAsync(new List<QuantityDiscount>());

			var result = await _checkoutService.Checkout();

			Assert.IsTrue(result.Items.Any(), "No items were returned");
			Assert.AreEqual(4, result.Items.Count, "Incorrect number of items returned");
			Assert.AreEqual(985, result.Total, "Total doesn't match expected amount");
		}
	}
}
