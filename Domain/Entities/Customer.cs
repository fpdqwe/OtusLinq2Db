using LinqToDB.Mapping;

namespace Domain.Entities
{
    [Table("customers")]
    public class Customer
    {
        [PrimaryKey, Identity, Column("id")]
        public int Id { get; set; }
        [Column("firstname"), NotNull]
        public string FirstName { get; set; }
        [Column("lastname"), NotNull]
        public string LastName { get; set; }
        [Column("age")]
        public int Age { get; set; }

		public override string ToString()
		{
            return $"{Id}: {FirstName}, {LastName} - {Age}";
		}
	}
}
