using FluentAssertions;
using Moq;
using Stock.Domain;

namespace Stock.Application.UnitTests;

public sealed class ProductCategoryQueryTests
{
    [Fact]
    public async Task Should_Returns_All_ProductCategories()
    {
        //Arrange
        List<ProductCategory> categories = new()
        {
            ProductCategory.Create("Gold"),
            ProductCategory.Create("Silver"),
            ProductCategory.Create("Jewlery")
        };

        Mock<IProductCategoryRepository> repository = new();

        repository.Setup(r=> r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(categories);

        ProductCagetoryQueryHandler handler = new(repository.Object);

        ProductCategoryQuery query = new();

        //Act
 
        List<ProductCategory> productCategories = handler.Handle(query,CancellationToken.None);

        //Assert

        productCategories.Should().NotBeNull();
        productCategories.Should().HaveCountGreaterThan(1);
    }
}
