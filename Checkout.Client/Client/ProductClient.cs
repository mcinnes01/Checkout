using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Checkout.Service.Models;

namespace Checkout.Client.Client
{
	internal class ProductClient
	{
		private readonly Uri _baseUri;

		public ProductClient()
		{
			_baseUri = new Uri("http://localhost:58461/");
		}

		public async Task ListProduct()
		{
			var query = new UriBuilder(_baseUri)
			{
				Path = "api/product"
			};

			var client = new HttpClient {BaseAddress = query.Uri};
			var response = await client.GetAsync(query.ToString());

			if (!response.IsSuccessStatusCode)
			{
				Console.WriteLine("Could not return a list of products");
				return;
			}

			var products = await response.Content.ReadAsAsync<List<Product>>();
			foreach (var product in products)
			{
				Console.WriteLine($"{product.Name} - £{(decimal)product.UnitPrice/100}");
			}
		}
	}
}
