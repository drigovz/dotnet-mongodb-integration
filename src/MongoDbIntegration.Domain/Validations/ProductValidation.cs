using MongoDbIntegration.Domain.Entities;
using FluentValidation;

namespace MongoDbIntegration.Domain.Validations
{
    public class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation()
        {
            RuleFor(x => x.Title).Title();
            RuleFor(x => x.Price).NotEmpty().NotEqual(0);
        }
    }
}