using Domain.Entities;
using LinqToDB;
using LinqToDB.Data;
using System.Diagnostics;

namespace Domain
{
	public class Repository
	{
		private readonly DataOptions _options;
        public Repository(DataOptions options)
        {
            _options = options;
        }

		// Product queries
        public async Task<List<Product>> GetProductsAsync()
        {
            using(var context = new Context(_options))
            {
				return await context.Products.OrderByDescending(x => x.Id).ToListAsync();
			}
        }
		public async Task<List<Product>> GetProductsAsync(decimal maxPrice)
		{
			using (var context = new Context(_options))
			{
				return await context.Products
					.Where(prod => prod.Price <= maxPrice)
					.ToListAsync();
			}
		}
		public async Task<List<Product>> GetProductsAsync(decimal minPrice, int minStock, int maxStock)
		{
			using (var context = new Context(_options))
			{
				return await context.Products
					.Where(prod => prod.Price >= minPrice)
					.Where(prod => prod.StockQuantity > minStock && prod.StockQuantity < maxStock)
					.ToListAsync();
			}
		}
		public async Task AddListAsync(List<Product> products)
        {
            using(var context = new Context(_options))
            {
				await context.BulkCopyAsync(products);
			}
        }

		// Customers queries
		public async Task<List<Customer>> GetCustomersAsync()
		{
			using(var context = new Context(_options))
			{
				return await context.Customers.OrderByDescending(c => c.Id).ToListAsync();
			}
		}
		public async Task<List<Customer>> GetCustomersAsync(int minAge)
		{
			using(var context = new Context(_options))
			{
				return await context.Customers
					.Where(c => c.Age > minAge)
					.OrderByDescending(c => c.Id)
					.ToListAsync();
			}
		}
		public async Task<List<Customer>> GetCustomersAsync(int maxAge, string nameStart)
		{
			using(var context = new Context(_options))
			{
				return await context.Customers
					.Where(c => c.Age <= maxAge)
					.Where(c => c.FirstName.StartsWith(nameStart))
					.OrderByDescending (c => c.Age)
					.ToListAsync();
			}
		}
		public async Task AddListAsync(List<Customer> customers)
		{
			using (var context = new Context(_options))
			{
				await context.BulkCopyAsync(customers);
			}
		}

		// Orders queries
		public async Task<List<Order>> GetOrdersAsync()
		{
			using(var context = new Context(_options))
			{
				return await context.Orders.OrderByDescending(c => c.Id).ToListAsync();
			}
		}
		public async Task<List<Order>> GetOrdersAsync(int minQuantity)
		{
			using(var context = new Context(_options))
			{
				return await context.Orders
					.Where(c => c.Quantity >= minQuantity)
					.OrderByDescending(c => c.Id)
					.ToListAsync();
			}
		}
		public async Task<List<Order>> GetOrdersAsync(int minQuantity, int maxQuantity)
		{
			using (var context = new Context(_options))
			{
				return await context.Orders
					.Where(c => c.Quantity >= minQuantity && c.Quantity <= maxQuantity)
					.OrderByDescending(c => c.Id)
					.ToListAsync();
			}
		}
		public async Task AddOrdersAsync(List<Order> orders)
		{
			using (var context = new Context(_options))
			{
				await context.BulkCopyAsync(orders);
			}
		}

		// Task 4
		// Не сделал потому что выпадает исключение "Association key 'customerid' not found for type 'Domain.Entities.Order."
		// Хз как исправить
		public async Task<List<Order>> GetCustomerByProduct(int productId, int minAge)
		{
			using(var context =new Context(_options))
			{
				return await context.Orders.LoadWith(p => p.Customer)
					.ToListAsync();
			}
		}
	}
}
