using System.Collections.Generic;
using Checkout.Service.Models;

namespace Checkout.Service
{
	public interface IBasketService
	{
		void AddItemToBasket(string product, int quantity);

		void RemoveItemFromBasket(string product, int quantity);

		void EmptyBasket();

		IList<BasketItem> GetBasketContents();
	}
}
