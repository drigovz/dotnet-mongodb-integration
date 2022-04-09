using MongoDbIntegration.Application.Core.Products.Commands;
using MongoDbIntegration.Domain.Validations;
using FluentValidation;

namespace MongoDbIntegration.Application.Core.Products.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Title).Title();
            RuleFor(x => x.Active).NotNull().NotEmpty();
            RuleFor(x => x.Price).NotEmpty().NotEqual(0);
        }
    }
}
