using System;
using System.Threading.Tasks;
using Checkout.Client.Client;

namespace Checkout.Client
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var basketClient = new BasketClient();
			var productClient = new ProductClient();
			var checkoutClient = new CheckoutClient();

			Help();

			while (true)
			{
				// Get the command
				var command = Console.ReadLine();

				switch (command)
				{
					case "list":
						await productClient.ListProduct();
						break;
					case "add":
						await basketClient.AddProduct();
						break;
					case "remove":
						await basketClient.RemoveProduct();
						break;
					case "basket":
						await basketClient.Contents();
						break;
					case "empty":
						await basketClient.Empty();
						break;
					case "checkout":
						await checkoutClient.Checkout();
						break;
					case "help":
						Help();
						break;
					default:
						Console.WriteLine("What would you like to do?");
						break;
				}
			}
		}

		private static void Help()
		{
			Console.WriteLine("Hello I'm a shopping basket!");
			Console.WriteLine("You can ask me to list products by typing: 'list'.");
			Console.WriteLine("You can add items to your basket by typing 'add', then entering the product name and quantity when prompted.");
			Console.WriteLine("You can remove items to your basket by typing 'remove', then entering the product name and quantity when prompted.");
			Console.WriteLine("You can see items in your basket by typing 'basket'.");
			Console.WriteLine("You can empty your basket by typing 'empty'.");
			Console.WriteLine("You can checkout by typing 'checkout'.");
			Console.WriteLine("To see this help info at anytime, type 'help'.");
		}
	}
}
