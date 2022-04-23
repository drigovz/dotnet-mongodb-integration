using FluentValidation;
using MongoDbIntegration.Application.Core.Products.Commands;

namespace MongoDbIntegration.Application.Core.Products.Validators
{
    public class RemoveProductCommandValidator : AbstractValidator<RemoveProductCommand>
    {
        public RemoveProductCommandValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty();
        }
    }
}
