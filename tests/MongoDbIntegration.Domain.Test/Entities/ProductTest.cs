using MongoDbIntegration.Domain.Test.Builders;
using FluentAssertions;
using Xunit;

namespace MongoDbIntegration.Domain.Test.Entities
{
    public class ProductTest
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        [Trait("Domain", "Product")]
        public void Should_Not_Create_Product_With_Invalid_Title(string invalidTitle)
        {
            var product = ProductBuilder.New().ProductWithTitle(invalidTitle).Build();

            product.Valid.Should().BeFalse();
            product.ValidationResult?.Errors.Should().NotBeEmpty();
            product.ValidationResult?.Errors.Should().HaveCount(c => c > 0).And.OnlyHaveUniqueItems();
            product.ValidationResult?.Errors.Should().Contain(x => x.ErrorMessage.Contains("Title"));
        }
        
        [Fact]
        [Trait("Domain", "Product")]
        public void Should_Not_Create_Product_With_Invalid_Price()
        {
            var product = ProductBuilder.New().ProductWithPrice(0).Build();

            product.Valid.Should().BeFalse();
            product.ValidationResult?.Errors.Should().NotBeEmpty();
            product.ValidationResult?.Errors.Should().HaveCount(c => c > 0).And.OnlyHaveUniqueItems();
            product.ValidationResult?.Errors.Should().Contain(x => x.ErrorMessage.Contains("Price"));
        }
    }
}