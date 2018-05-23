using System.Linq;
using Checkout.Service.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Checkout.Service.Tests
{
	[TestClass]
	public class BasketTests
	{
		private Mock<IProductService> _productService;
		private IMemoryCache _cache;
		private IBasketService _basketService;

		[TestInitialize]
		public void StartUp()
		{
			_productService = new Mock<IProductService>();
			_productService.Setup(s => s.GetProductByName(It.IsAny<string>()))
				.Returns(new Product { Name = "Apple", UnitPrice = 50 });

			_cache = new MemoryCache(new MemoryCacheOptions());

			_basketService = new BaseketService(_cache, _productService.Object);
		}

		[TestMethod]
		public void AddItemToBasketReturnsAppleFirst()
		{
			// Setup test
			_cache.Remove("1234");
			_basketService.AddItemToBasket("Apple", 7);

			var result = _basketService.GetBasketContents();
			Assert.IsTrue(result.Any(), "No items were returned");
			Assert.AreEqual(7, result.FirstOrDefault()?.Quantity, "There should initially be 7 items in the basket");
			Assert.AreEqual("Apple", result.FirstOrDefault()?.Product?.Name, "Apple was not the first product returned");
		}

		[TestMethod]
		public void EmptyBasketShouldReturnNoItems()
		{
			// Setup test
			_cache.Remove("1234");
			_basketService.AddItemToBasket("Apple", 7);

			_basketService.EmptyBasket();
			var result = _basketService.GetBasketContents();

			Assert.IsTrue(!result.Any(), "Items were returned when they shouldn't have been");
		}

		[TestMethod]
		public void RemoveItemFromBasketShouldLeaveFiveApples()
		{
			// Setup test
			_cache.Remove("1234");
			_basketService.AddItemToBasket("Apple", 7);

			var result = _basketService.GetBasketContents();
			Assert.AreEqual(7, result.FirstOrDefault()?.Quantity, "There should initially be 7 items in the basket");

			_basketService.RemoveItemFromBasket("Apple", 2);

			result = _basketService.GetBasketContents();
			Assert.IsTrue(result.Any(), "No products were returned");
			Assert.AreEqual(5, result.First().Quantity, "Incorrect number of items in the basket, expected 5");
		}

		[TestMethod]
		public void EmptyBasketShouldLeaveNotItemsInBasket()
		{
			// Setup test
			_cache.Remove("1234");
			_basketService.AddItemToBasket("Apple", 7);

			var result = _basketService.GetBasketContents();
			Assert.AreEqual(7, result.FirstOrDefault()?.Quantity, "There should initially be 7 items in the basket");

			// Now empty the basket
			 _basketService.EmptyBasket();

			result = _basketService.GetBasketContents();
			Assert.IsFalse(result.Any(), "There are unexpected items in the basket");
		}
	}
}
