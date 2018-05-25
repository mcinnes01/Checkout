using System.Collections.Generic;
using System.IO;
using Checkout.Service;
using Checkout.Service.Converters;
using Checkout.Service.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Checkout
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("Data/Products.json", optional: false, reloadOnChange: true)
				.AddEnvironmentVariables();

			Configuration = builder.Build();
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddOptions();
			services.Configure<List<Product>>(options => Configuration.GetSection("Products").Bind(options));

			services.AddMemoryCache();
			services.AddMvc()
				.AddJsonOptions(options =>
				{
					options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
					options.SerializerSettings.Formatting = Formatting.Indented;
					options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
					JsonConvert.DefaultSettings = () => new JsonSerializerSettings
					{
						ContractResolver = new CamelCasePropertyNamesContractResolver(),
						Formatting = Formatting.Indented,
						NullValueHandling = NullValueHandling.Ignore
					};
				});

			ConfigureJsonDiscounts(services);
			services.AddSingleton<IDiscountService, DiscountService>();
			services.AddSingleton<IProductService, ProductService>();
			services.AddSingleton<IBasketService, BaseketService>();
			services.AddSingleton<ICheckoutService, CheckoutService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseMvc()
				.UseDefaultFiles()
				.UseStaticFiles();
		}

		private void ConfigureJsonDiscounts(IServiceCollection services)
		{
			var discountJson = JObject.Parse(File.ReadAllText("Data/Discounts.json"))["Discounts"];
			var discounts = JsonConvert.DeserializeObject<List<IDiscount>>(discountJson.ToString(),
				new JsonSerializerSettings { Converters = new List<JsonConverter> { new DiscountConverter()}});

			services.AddSingleton(discounts);
		}
	}
}