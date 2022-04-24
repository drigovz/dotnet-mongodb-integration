using FluentValidation;
using MongoDbIntegration.Application.Core.Products.Queries;

namespace MongoDbIntegration.Application.Core.Products.Validators
{
    public class GetProductByTitleQueryValidator : AbstractValidator<GetProductByTitleQuery>
    {
        public GetProductByTitleQueryValidator()
        {
            RuleFor(c => c.Title).NotEmpty().NotNull();
        }
    }
}
