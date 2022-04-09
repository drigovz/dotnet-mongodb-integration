using MongoDbIntegration.Domain.Validations;

namespace MongoDbIntegration.Domain.Entities
{
    [BsonCollection("product")]
    public sealed class Product : BaseEntity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public bool Active { get; private set; }

        public Product(string title, string description, decimal price, bool active)
        {
            Title = title;
            Description = description;
            Price = price;
            Active = active;

            EntityValidation(this, new ProductValidation());
        }

        public Product Update(string title, string description, decimal price, bool active)
        {
            Title = title;
            Description = description;
            Price = price;
            Active = active;

            EntityValidation(this, new ProductValidation());
            return this;
        }
    }
}