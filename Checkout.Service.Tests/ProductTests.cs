using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkout.Service.Models;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Checkout.Service.Tests
{
	[TestClass]
	public class ProductTests
	{
		private Mock<IOptions<List<Product>>> _product;
		private IProductService _productService;

		[TestInitialize]
		public void StartUp()
		{
			_product = new Mock<IOptions<List<Product>>>();

			_product.Setup(p => p.Value).Returns(new List<Product>
			{
				new Product {Name = "Apple", UnitPrice = 50},
				new Product {Name = "Biscuits", UnitPrice = 30},
				new Product {Name = "Coffee", UnitPrice = 180},
				new Product {Name = "Tissues", UnitPrice = 99}
			});

			_productService = new ProductService(_product.Object);
		}

		[TestMethod]
		public async Task GetProductsAlphabeticallyReturnsAtLeastOneProduct()
		{
			var result = await _productService.GetProductsAlphabetically();

			Assert.IsTrue(result.Any(), "No products were returned");
		}

		[TestMethod]
		public async Task GetProductsAlphabeticallyReturnsAppleFirst()
		{
			var result = await _productService.GetProductsAlphabetically();

			Assert.AreEqual("Apple", result.First()?.Name, "Apple was not the first product returned");
		}

		[TestMethod]
		public async Task GetProductsByPriceReturnsAtLeastOneProduct()
		{
			var result = await _productService.GetProductsByPrice();

			Assert.IsTrue(result.Any(), "No products were returned");
		}

		[TestMethod]
		public async Task GetProductsByPriceReturnsBiscuitPriceFirst()
		{
			var result = await _productService.GetProductsByPrice();

			Assert.AreEqual(result.First().UnitPrice, 30, "The first product was not 30p");
		}

		[TestMethod]
		public async Task GetProductByNameReturnsAProduct()
		{
			var result = await _productService.GetProductByName("Apple");

			Assert.IsNotNull(result, "No products were returned");
			Assert.AreEqual("Apple", result.Name, "Product returned was not Apple");
		}
	}
}
