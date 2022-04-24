using FluentValidation;
using MongoDbIntegration.Application.Core.Products.Queries;

namespace MongoDbIntegration.Application.Core.Products.Validators
{
    public class GetProductQueryValidator : AbstractValidator<GetProductQuery>
    {
        public GetProductQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
        }
    }
}
