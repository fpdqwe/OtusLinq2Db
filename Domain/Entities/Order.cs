using LinqToDB.Mapping;


namespace Domain.Entities
{
    [Table("orders")]
    public class Order
    {
        [PrimaryKey, Identity, Column("id")]
        public int Id { get; set; }
        [Column("customerid")]
        public int CustomerId { get; set; }
        [Column("productid")]
        public int ProductId { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }
        [Association(ThisKey = "productid", OtherKey ="ID")]
        public Product Product { get; set; }
        [Association(ThisKey = "customerid", OtherKey ="ID")]
        public Customer Customer { get; set; }
		public override string ToString()
		{
            return $"{Id}: customer - {CustomerId}, product - {ProductId}. Quantity: {Quantity}";
		}
	}
}
