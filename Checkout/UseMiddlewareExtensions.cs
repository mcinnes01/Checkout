using System.Collections.Generic;
using System.Threading.Tasks;
using Checkout.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Checkout
{
	public static class UseMiddlewareExtensions
	{
		public static IApplicationBuilder UseJsonConverter(this IApplicationBuilder app)
		{
			return app.UseMiddleware<JsonConverterMiddleware>();
		}
	}

	public class JsonConverterMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly IOptions<List<IDiscount>> _discounts;

		public JsonConverterMiddleware(RequestDelegate next, IOptions<List<IDiscount>> discounts)
		{
			_next = next;
			_discounts = discounts;
		}

		public async Task Invoke(HttpContext context)
		{
			var jsonSettings = JsonConvert.SerializeObject(_discounts);
			await context.Response.WriteAsync(jsonSettings);
		}
	}
}
