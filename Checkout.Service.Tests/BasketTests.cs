using System.Linq;
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

			_cache = new MemoryCache(new MemoryCacheOptions());

			_basketService = new BaseketService(_cache, _productService.Object);
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
