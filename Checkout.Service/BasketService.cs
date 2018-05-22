using System.Collections.Generic;
using Checkout.Service.Models;

namespace Checkout.Service
{
	public class BaseketService : IBasketService
	{
		public void AddItemToBasket(string product, int quantity)
		{
			throw new System.NotImplementedException();
		}

		public void RemoveItemFromBasket(string product, int quantity)
		{
			throw new System.NotImplementedException();
		}

		public void EmptyBasket()
		{
			throw new System.NotImplementedException();
		}

		public IList<BasketItem> GetBasketContents()
		{
			throw new System.NotImplementedException();
		}
	}
}
