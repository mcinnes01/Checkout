using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkout.Service.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Checkout.Service
{
	public class BaseketService : IBasketService
	{
		private readonly IMemoryCache _cache;
		private readonly IProductService _productService;

		public BaseketService(IMemoryCache cache, IProductService productService)
		{
			_cache = cache;
			_productService = productService;
		}

		public async Task AddItemToBasket(string product, int quantity)
		{
			// Check the product exists
			var item = await _productService.GetProductByName(product);

			if (item == null)
				return;

			// This key would relate to a customer, hard coding as an example
			// We try to retrieve the basket from our cache, this could be a db in production
			// Or it could use session with something like Redis to enable stock return for
			// Unprocessed baskets
			var items = _cache.GetOrCreate("1234", entry =>
			{
				// Basket timeout after 5 minutes
				entry.SlidingExpiration = TimeSpan.FromMinutes(5);
				return (entry.Value ?? new List<BasketItem>()) as List<BasketItem>;
			});

			items.Add(new BasketItem
			{
				Product = item,
				Quantity = quantity
			});

			// Group the items by product and add the quantities
			var aggregate = items.GroupBy(i => i.Product.Name)
				.Select(g => g.Aggregate((i1, i2) => new BasketItem
				{
					Product = i1.Product,
					Quantity = i1.Quantity + i2.Quantity
				}))
				.ToList();

			_cache.Set("1234", aggregate, new MemoryCacheEntryOptions()
				.SetSlidingExpiration(TimeSpan.FromMinutes(5)));
		}

		public async Task RemoveItemFromBasket(string product, int quantity)
		{
			// Check the item exists
			var item = await _productService.GetProductByName(product);

			if (item == null)
				return;

			// Retrieve the basket as above
			var items = _cache.GetOrCreate("1234", entry =>
			{
				entry.SlidingExpiration = TimeSpan.FromMinutes(5);
				return (entry.Value ?? new List<BasketItem>()) as List<BasketItem>;
			});

			// Group in to a dictionary to allow us to update or remove
			var aggregate = items.GroupBy(i => i.Product.Name)
				.Select(g => g.Aggregate((i1, i2) => new BasketItem
				{
					Product = i1.Product,
					Quantity = i1.Quantity + i2.Quantity
				}))
				.ToDictionary(x => x.Product.Name);

			// Check the product is already in the basket
			if (!aggregate.TryGetValue(product, out var basketItem))
				return;

			// Update the quantity
			basketItem.Quantity = basketItem.Quantity - quantity;

			// Remove product entirely if quanity <= 0
			if (basketItem.Quantity <= 0)
				aggregate.Remove(product);

			// Update cache
			var basketItems = aggregate.Select(x => x.Value).ToList();
			_cache.Set("1234", basketItems, new MemoryCacheEntryOptions()
				.SetSlidingExpiration(TimeSpan.FromMinutes(5)));
		}

		public async Task EmptyBasket()
		{
			_cache.Remove("1234");
			await Task.CompletedTask;
		}

		public async Task<IList<BasketItem>> GetBasketContents()
		{
			if (!_cache.TryGetValue("1234", out List<BasketItem> items))
				return new List<BasketItem>();

			var aggregate = items.GroupBy(i => i.Product.Name)
				.Select(g => g.Aggregate((i1, i2) => new BasketItem
				{
					Product = i1.Product,
					Quantity = i1.Quantity + i2.Quantity
				}));

			return await Task.FromResult(aggregate.ToList());
		}
	}
}
