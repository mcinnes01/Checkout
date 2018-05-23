using System.Collections.Generic;
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
		public IList<BasketItem> GetBasketContents()
		{
			return _basketService.GetBasketContents();
		}

		// PUT api/basket/Apple/3
		[HttpPut("{product}/{quantity}")]
		public OkResult AddItemToBasket(string product, int quantity)
		{
			_basketService.AddItemToBasket(product, quantity);

			return Ok();
		}

		// DELETE api/basket/Apple/3
		[HttpDelete("{product}/{quantity}")]
		public OkResult RemoveItemFromBasket(string product, int quantity)
		{
			_basketService.RemoveItemFromBasket(product, quantity);

			return Ok();
		}

		// DELETE api/basket/empty
		[HttpDelete("empty")]
		public OkResult EmptyBasket()
		{
			_basketService.EmptyBasket();

			return Ok();
		}
	}
}
