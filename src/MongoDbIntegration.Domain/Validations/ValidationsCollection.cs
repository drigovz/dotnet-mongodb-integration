using FluentValidation;

namespace MongoDbIntegration.Domain.Validations
{
    public static class ValidationsCollection
    {
        public static IRuleBuilderOptions<T, string> Title<T>(this IRuleBuilder<T, string> ruleBuilder) =>
            ruleBuilder.NotNull().NotEmpty().WithMessage("Title is required!");
    }
}