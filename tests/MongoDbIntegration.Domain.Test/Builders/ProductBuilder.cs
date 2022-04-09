using Bogus;
using MongoDbIntegration.Domain.Entities;

namespace MongoDbIntegration.Domain.Test.Builders
{
    public class ProductBuilder
    {
        private static readonly Faker faker = new("en");

        public string Title = faker.Name.FirstName();
        public string Description = faker.Lorem.Paragraph();
        public decimal Price = faker.Random.Decimal(0, 100);
        public bool Active = faker.Random.Bool();

        public static ProductBuilder New() =>
            new();

        public ProductBuilder ProductWithTitle(string title)
        {
            Title = title;
            return this;
        }

        public ProductBuilder ProductWithDescription(string description)
        {
            Description = description;
            return this;
        }

        public ProductBuilder ProductWithPrice(decimal price)
        {
            Price = price;
            return this;
        }

        public Product Build()
            => new(Title, Description, Price, Active);
    }
}