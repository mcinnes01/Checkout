using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Checkout.Service.Tests
{
	[TestClass]
	public class BasketTests
	{
		private IBasketService _basketService;

		[TestInitialize]
		public void StartUp()
		{
			_basketService = new BaseketService();
		}

		[TestMethod]
		public void GetBasketContentsShouldReturnAtLeastOneItem()
		{
			var result = _basketService.GetBasketContents();

			Assert.IsTrue(result.Any(), "No items were returned");
		}

		[TestMethod]
		public void AddItemToBasketReturnsAppleFirst()
		{
			_basketService.AddItemToBasket("Apple", 7);

			var result = _basketService.GetBasketContents();

			Assert.IsTrue(result.Any(), "No items were returned");
			Assert.AreEqual("Apple", result.First()?.Product, "Apple was not the first product returned");
		}

		[TestMethod]
		public void RemoveItemFromBasketShouldLeaveFiveApples()
		{
			_basketService.EmptyBasket();
			_basketService.AddItemToBasket("Apple", 7);
			_basketService.RemoveItemFromBasket("Apple", 2);

			var result = _basketService.GetBasketContents();

			Assert.IsTrue(result.Any(), "No products were returned");
			Assert.AreEqual(5, result.First().Quantity, "Incorrect number of items in the basket, expected 5");
		}

		[TestMethod]
		public void EmptyBasketShouldLeaveNotItemsInBasket()
		{
			_basketService.AddItemToBasket("Apple", 7);
			 _basketService.EmptyBasket();

			var result = _basketService.GetBasketContents();

			Assert.IsFalse(result.Any(), "There are unexpected items in the basket");
		}
	}
}
