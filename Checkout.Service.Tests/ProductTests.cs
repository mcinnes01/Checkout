using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Checkout.Service.Tests
{
	[TestClass]
	public class ProductTests
	{
		private IProductService _productService;

		[TestInitialize]
		public void StartUp()
		{
			_productService = new ProductService();
		}

		[TestMethod]
		public void GetProductsAlphabeticallyReturnsAtLeastOneProduct()
		{
			var result = _productService.GetProductsAlphabetically();

			Assert.IsTrue(result.Any(), "No products were returned");
		}

		[TestMethod]
		public void GetProductsAlphabeticallyReturnsAppleFirst()
		{
			var result = _productService.GetProductsAlphabetically();

			Assert.AreEqual("Apple", result.First()?.Name, "Apple was not the first product returned");
		}

		[TestMethod]
		public void GetProductsByPriceReturnsAtLeastOneProduct()
		{
			var result = _productService.GetProductsByPrice();

			Assert.IsTrue(result.Any(), "No products were returned");
		}

		[TestMethod]
		public void GetProductsByPriceReturnsBiscuitPriceFirst()
		{
			var result = _productService.GetProductsByPrice();

			Assert.AreEqual(result.First().UnitPrice, 30, "The first product was not 30p");
		}

		[TestMethod]
		public void GetProductByNameReturnsAProduct()
		{
			var result = _productService.GetProductByName("Apple");

			Assert.IsNotNull(result, "No products were returned");
			Assert.AreEqual("Apple", result.Name, "Product returned was not Apple");
		}
	}
}
