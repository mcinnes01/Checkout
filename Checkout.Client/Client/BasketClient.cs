using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Checkout.Service.Models;

namespace Checkout.Client.Client
{
	internal class BasketClient
	{
		private readonly Uri _baseUri;

		public BasketClient()
		{
			_baseUri = new Uri("http://localhost:58461/");
		}

		public async Task AddProduct()
		{
			Console.WriteLine("Enter the product name, it is case sensitive.");
			var item = Console.ReadLine();

			var query = new UriBuilder(_baseUri)
			{
				Path = $"api/product/{item}"
			};

			var client = new HttpClient { BaseAddress = query.Uri };
			var response = await client.GetAsync(query.ToString());

			if (!response.IsSuccessStatusCode)
			{
				Console.WriteLine("Product not found, if you would like to add a product type 'add' and enter a valid product.");
				Console.WriteLine("If you would like to see a list of available products, enter 'list'.");
				return;
			}

			var product = await response.Content.ReadAsAsync<Product>();

			Console.WriteLine($"Please enter the quantity of {item} you would like to add to the basket?");
			var quantity = Console.ReadLine();

			query.Path = $"api/basket/{product.Name}/{quantity}";
			response = await client.PutAsJsonAsync(query.Uri, product);

			if (response.IsSuccessStatusCode)
			{
				Console.WriteLine($"Added {product.Name} * {quantity} to your basket.");
				return;
			}

			Console.WriteLine($"Could not add {item} * {quantity} to your basket.");
		}

		public async Task RemoveProduct()
		{
			Console.WriteLine("Enter the product name, it is case sensitive.");
			var item = Console.ReadLine();

			var query = new UriBuilder(_baseUri)
			{
				Path = $"api/product/{item}"
			};

			var client = new HttpClient {BaseAddress = query.Uri};
			var response = await client.GetAsync(query.ToString());

			if (!response.IsSuccessStatusCode)
			{
				Console.WriteLine("Product not found, if you would like to remove a product type 'remove' and enter a valid product.");
				Console.WriteLine("If you would like to see a list of products in your basket, enter 'basket'.");
				return;
			}

			var product = await response.Content.ReadAsAsync<Product>();

			Console.WriteLine($"Please enter the quantity of {item} you would like to remove from the basket?");
			var quantity = Console.ReadLine();

			query.Path = $"api/basket/{product.Name}/{quantity}";
			response = await client.DeleteAsync(query.ToString());

			if (response.IsSuccessStatusCode)
			{
				Console.WriteLine($"Removed {product.Name} * {quantity} from your basket.");
				return;
			}

			Console.WriteLine($"Could not remove {item} * {quantity} to your basket.");
		}

		public async Task Contents()
		{
			var query = new UriBuilder(_baseUri)
			{
				Path = "api/basket"
			};

			var client = new HttpClient {BaseAddress = query.Uri};
			var response = await client.GetAsync(query.ToString());

			if (!response.IsSuccessStatusCode)
			{
				Console.WriteLine("Could not return basket contents");
				return;
			}

			var basketItems = await response.Content.ReadAsAsync<List<BasketItem>>();
			if (!basketItems.Any())
			{
				Console.WriteLine("There is nothing in your basket.");
				return;
			}

			foreach (var item in basketItems)
			{
				Console.WriteLine($"{item.Product.Name} - {item.Quantity}");
			}
		}

		public async Task Empty()
		{
			var query = new UriBuilder(_baseUri)
			{
				Path = "api/basket/empty"
			};

			var client = new HttpClient {BaseAddress = query.Uri};
			var response = await client.DeleteAsync(query.ToString());

			if (!response.IsSuccessStatusCode)
			{
				Console.WriteLine("Could not empty basket contents");
				return;
			}

			Console.WriteLine("Basket is empty");
		}
	}
}

