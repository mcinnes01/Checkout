using System;
using System.Net.Http;
using System.Threading.Tasks;
using Checkout.Service.Models;

namespace Checkout.Client.Client
{
	internal class CheckoutClient
	{
		private readonly Uri _baseUri;

		public CheckoutClient()
		{
			_baseUri = new Uri("http://localhost:58461/");
		}

		public async Task Checkout()
		{
			var query = new UriBuilder(_baseUri)
			{
				Path = "api/checkout"
			};

			var client = new HttpClient {BaseAddress = query.Uri};
			var response = await client.PostAsync(query.ToString(), new MultipartFormDataContent());

			if (!response.IsSuccessStatusCode)
			{
				Console.WriteLine("Could not checkout basket");
				return;
			}

			var checkoutBasket = await response.Content.ReadAsAsync<CheckoutBasket>();
			foreach (var item in checkoutBasket.Items)
			{
				Console.WriteLine(item.Discount != null
					? $"{item.Product} - {item.Discount.Name} - {item.Quantity} - £{(decimal) item.Price/100}"
					: $"{item.Product} - {item.Quantity} - £{(decimal) item.Price/100}");
			}

			Console.WriteLine($"Total: £{(decimal)checkoutBasket.Total/100}");
		}
	}
}
