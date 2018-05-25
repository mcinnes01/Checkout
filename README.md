# Checkout

This app provides a simple basket and checkout service and client.

The solution is structured as follows:

- Checkout - API
- Checkout.Service - Services consumed by the API
- Checkout.Service.Interfaces - Contract that describes the services and objects

- Checkout.Service.Tests - Tests for the services

- Checkout.Client - Console app to provide a simple client for the service

# Technology

The app is built using .Net Core 2.0, the project is using the latest versions of some packages. 
You will need to ensure on the build tab for each project, in the advanced options, 
that "C# lasest minor build version (lastest)" is selected. You will also need to ensure you have
the latest version on .net installed for .net core.

Instead of writing a database, I am using two JSON files to represent a discount and product data store, 
then leveraging the IOptions pattern in .Net core to read these files and inject them in to my services.

For the basket I am using an InMemoryCache, this again could represent a data store, or it is equally possible
a distributed cahce like redis could be used until the order goes to checkout. It also provides the ability to 
automatically dispose of a basket that has not be updated in a certain amount of time. In this case I applied a
5 minute sliding expiration.

# Run the app

You will also need to right click on the solution and ensure that the startup projects are set
to multiple, with the following project set to debug:

- Checkout
- Checkout.Client

If you run the app you should see 2 console apps:
- One will be for the Checkout API
- The second is the Checkout.Client

You will also get a web page for the checkout service

# Improvements

Currently discounts are only quantity / price discounts, in a real situation there may be different types such as,
percentage discounts, multi-buys, etc. This would lend its self to using Strategy pattern coupled with a factory to 
resolve the correct discount type.
