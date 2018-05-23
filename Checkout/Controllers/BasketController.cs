using System.Collections.Generic;
using System.Threading.Tasks;
using Checkout.Service;
using Checkout.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.Controllers
{
	[Route("api/[controller]")]
	public class BasketController : Controller
	{
		private readonly IBasketService _basketService;

		public BasketController(IBasketService basketService)
		{
			_basketService = basketService;
		}


		// GET api/basket
		[HttpGet]
		public async Task<IList<BasketItem>> GetBasketContents()
		{
			return await _basketService.GetBasketContents();
		}

		// PUT api/basket/Apple/3
		[HttpPut("{product}/{quantity}")]
		public async Task<OkResult> AddItemToBasket(string product, int quantity)
		{
			await _basketService.AddItemToBasket(product, quantity);

			return Ok();
		}

		// DELETE api/basket/Apple/3
		[HttpDelete("{product}/{quantity}")]
		public async Task<OkResult> RemoveItemFromBasket(string product, int quantity)
		{
			await _basketService.RemoveItemFromBasket(product, quantity);

			return Ok();
		}

		// DELETE api/basket/empty
		[HttpDelete("empty")]
		public async Task<OkResult> EmptyBasket()
		{
			await _basketService.EmptyBasket();

			return Ok();
		}
	}
}
