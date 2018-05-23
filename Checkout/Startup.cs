using System.Collections.Generic;
using Checkout.Service;
using Checkout.Service.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Checkout
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("Data/Discounts.json", optional: false, reloadOnChange: true)
				.AddJsonFile("Data/Products.json", optional: false, reloadOnChange: true)
				.AddEnvironmentVariables();

			Configuration = builder.Build();
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddOptions();
			services.Configure<List<QuantityDiscount>>(options => Configuration.GetSection("QuantityDiscounts").Bind(options));
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
	}
}