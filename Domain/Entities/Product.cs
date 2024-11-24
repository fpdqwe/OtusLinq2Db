using LinqToDB.Mapping;

namespace Domain.Entities
{
    [Table("products")]
    public class Product
    {
        [PrimaryKey, Identity, Column("id")]
        public int Id { get; set; }
        [Column("name"), NotNull]
        public string Name { get; set; }
        [Column("description"), NotNull]
        public string Description { get; set; }
        [Column("stockquantity")]
        public int StockQuantity { get; set; }
        [Column("price")]
        public decimal Price { get; set; }

		public override string ToString()
		{
            return $"{Id}: {Name} - {Description}. Изначально: {StockQuantity}. Цена: {Price}";
		}
	}
}
