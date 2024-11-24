using Domain;
using LinqToDB;
using LinqToDB.DataProvider;
using LinqToDB.DataProvider.PostgreSQL;
using LinqToDB.DataProvider.SqlServer;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace OtusLinq2Db
{
	internal class Program
	{
		private static readonly string _connectionString = "Host=localhost;Username=postgres;Password=YCZ1J7_Ww-;Database=shop;";
		private static Repository _repos;
		static async Task Main(string[] args)
		{
			var options = new DataOptions()
				.UsePostgreSQL(_connectionString, PostgreSQLVersion.v15);
			_repos = new Repository(options);


			await NonParamQueryToProducts();
			await MaxPriceQueryToProducts();
			await MinPriceAndStockQueryToProducts();
			await NonParamQueryToCustomers();
			await MinAgeQueryToCustomers(35);
			await MaxAgeStartsWithQueryToCustomers(40, "Т");
			await NonParamQueryToOrders();
			await MinQuantityQueryToOrders(5);
			await FullQuantityQueryToOrders(5, 10);
			// await GetCustomerByProduct(7, 18);
		}

		// Products
		private static async Task NonParamQueryToProducts()
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			var products = await _repos.GetProductsAsync();
			sw.Stop();
			Console.WriteLine("GetProductsAsync() executed in " + sw.ElapsedMilliseconds);
			foreach (var product in products) { Console.WriteLine(product.ToString()); }
		}
		private static async Task MaxPriceQueryToProducts()
		{
			int maxPrice = 500;
			Stopwatch sw = new Stopwatch();
			sw.Start();
			var products = await _repos.GetProductsAsync(maxPrice);
			sw.Stop();
			Console.WriteLine($"GetProductsAsync(decimal maxPrice = {maxPrice}) executed in {sw.ElapsedMilliseconds} ms.");
			foreach (var product in products) { Console.WriteLine(product.ToString()); }
		}
		private static async Task MinPriceAndStockQueryToProducts()
		{
			int minPrice = 500;
			int minStock = 40;
			int maxStock = 110;
			Stopwatch sw = new Stopwatch();
			sw.Start();
			var products = await _repos.GetProductsAsync(minPrice, minStock, maxStock);
			sw.Stop();
			Console.WriteLine($"GetProductsAsync(decimal minPrice = {minPrice}, int minStock = {minStock}, " +
				$", int maxStock = {maxStock}) executed in {sw.ElapsedMilliseconds} ms.");
			foreach (var product in products) { Console.WriteLine(product.ToString()); }
		}

		// Customers
		private static async Task NonParamQueryToCustomers()
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			var customers = await _repos.GetCustomersAsync();
			sw.Stop();
			Console.WriteLine($"GetCustomersAsync() executed in {sw.ElapsedMilliseconds} ms.");
			foreach (var customer in customers) { Console.WriteLine(customer.ToString()); }
		}
		private static async Task MinAgeQueryToCustomers(int minAge)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			var customers = await _repos.GetCustomersAsync(minAge);
			sw.Stop();
			Console.WriteLine($"GetCustomersAsync(int minAge = {minAge}) executed in {sw.ElapsedMilliseconds} ms.");
			foreach (var customer in customers) { Console.WriteLine(customer.ToString()); }
		}
		private static async Task MaxAgeStartsWithQueryToCustomers(int maxAge, string nameStart)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			var customers = await _repos.GetCustomersAsync(maxAge, nameStart);
			sw.Stop();
			Console.WriteLine($"GetCustomersAsync(int maxAge = {maxAge}, string startsWith = {nameStart}) executed in {sw.ElapsedMilliseconds} ms.");
			foreach (var customer in customers) { Console.WriteLine(customer.ToString()); }
		}

		// Orders
		private static async Task NonParamQueryToOrders()
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			var orders = await _repos.GetOrdersAsync();
			sw.Stop();

			Console.WriteLine($"GetOrdersAsync() executed in {sw.ElapsedMilliseconds} ms.");

			foreach (var order in orders) Console.WriteLine(order.ToString());
		}
		private static async Task MinQuantityQueryToOrders(int minQuantity)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			var orders = await _repos.GetOrdersAsync(minQuantity);
			sw.Stop();

			Console.WriteLine($"GetOrdersAsync(int minQuantity) executed in {sw.ElapsedMilliseconds} ms.");

			foreach (var order in orders) Console.WriteLine(order.ToString());
		}
		private static async Task FullQuantityQueryToOrders(int minQuantity, int maxQuantity)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			var orders = await _repos.GetOrdersAsync(minQuantity, maxQuantity);
			sw.Stop();

			Console.WriteLine($"GetOrdersAsync(int minQuantity, int maxQuantity) executed in {sw.ElapsedMilliseconds} ms.");

			foreach (var order in orders) Console.WriteLine(order.ToString());
		}

		// Task 4
		private static async Task GetCustomerByProduct(int productId, int cMinAge)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			var orders = await _repos.GetCustomerByProduct(productId, cMinAge);
			sw.Stop();

			Console.WriteLine($"GetOrdersAsync() executed in {sw.ElapsedMilliseconds} ms.");

			foreach (var order in orders) Console.WriteLine(order.ToString());
		}
	}
}
