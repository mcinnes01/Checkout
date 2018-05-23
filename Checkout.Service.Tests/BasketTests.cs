using System.Linq;
using System.Threading.Tasks;
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
				.ReturnsAsync(new Product { Name = "Apple", UnitPrice = 50 });

			_cache = new MemoryCache(new MemoryCacheOptions());

			_basketService = new BaseketService(_cache, _productService.Object);
		}

		[TestMethod]
		public async Task AddItemToBasketReturnsAppleFirst()
		{
			// Setup test
			_cache.Remove("1234");
			await _basketService.AddItemToBasket("Apple", 7);

			var result = await _basketService.GetBasketContents();
			Assert.IsTrue(result.Any(), "No items were returned");
			Assert.AreEqual(7, result.FirstOrDefault()?.Quantity, "There should initially be 7 items in the basket");
			Assert.AreEqual("Apple", result.FirstOrDefault()?.Product?.Name, "Apple was not the first product returned");
		}

		[TestMethod]
		public async Task EmptyBasketShouldReturnNoItems()
		{
			// Setup test
			_cache.Remove("1234");
			await _basketService.AddItemToBasket("Apple", 7);

			await _basketService.EmptyBasket();
			var result = await _basketService.GetBasketContents();

			Assert.IsTrue(!result.Any(), "Items were returned when they shouldn't have been");
		}

		[TestMethod]
		public async Task RemoveItemFromBasketShouldLeaveFiveApples()
		{
			// Setup test
			_cache.Remove("1234");
			await _basketService.AddItemToBasket("Apple", 7);

			var result = await _basketService.GetBasketContents();
			Assert.AreEqual(7, result.FirstOrDefault()?.Quantity, "There should initially be 7 items in the basket");

			await _basketService.RemoveItemFromBasket("Apple", 2);

			result = await _basketService.GetBasketContents();
			Assert.IsTrue(result.Any(), "No products were returned");
			Assert.AreEqual(5, result.First().Quantity, "Incorrect number of items in the basket, expected 5");
		}

		[TestMethod]
		public async Task EmptyBasketShouldLeaveNotItemsInBasket()
		{
			// Setup test
			_cache.Remove("1234");
			await _basketService.AddItemToBasket("Apple", 7);

			var result = await _basketService.GetBasketContents();
			Assert.AreEqual(7, result.FirstOrDefault()?.Quantity, "There should initially be 7 items in the basket");

			// Now empty the basket
			 await _basketService.EmptyBasket();

			result = await _basketService.GetBasketContents();
			Assert.IsFalse(result.Any(), "There are unexpected items in the basket");
		}
	}
}
