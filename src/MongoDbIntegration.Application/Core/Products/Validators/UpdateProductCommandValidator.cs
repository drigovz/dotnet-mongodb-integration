using FluentValidation;
using MongoDbIntegration.Application.Core.Products.Commands;
using MongoDbIntegration.Domain.Validations;

namespace MongoDbIntegration.Application.Core.Products.Validators
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
            RuleFor(x => x.Title).Title();
            RuleFor(x => x.Price).NotEmpty().NotEqual(0);
        }
    }
}
