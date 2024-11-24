using Domain.Entities;
using LinqToDB;
using LinqToDB.Data;

namespace Domain
{
    public class Context : DataConnection
	{
		private readonly string _connectionString = "Host=localhost;Username=postgres;Password=admin;Database=shop;Include Error Detail=true";

		public Context(DataOptions options) : base(options) { }

        public ITable<Customer> Customers => this.GetTable<Customer>();
		public ITable<Product> Products => this.GetTable<Product>();
		public ITable<Order> Orders => this.GetTable<Order>();
    }
}
