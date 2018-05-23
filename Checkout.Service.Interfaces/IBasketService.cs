using System.Collections.Generic;
using System.Threading.Tasks;
using Checkout.Service.Models;

namespace Checkout.Service
{
	public interface IBasketService
	{
		Task AddItemToBasket(string product, int quantity);

		Task RemoveItemFromBasket(string product, int quantity);

		Task EmptyBasket();

		Task<IList<BasketItem>> GetBasketContents();
	}
}
