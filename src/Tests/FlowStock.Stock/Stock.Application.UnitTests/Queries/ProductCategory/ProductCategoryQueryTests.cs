using AutoMapper;
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

        repository.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(categories);

        Mock<IMapper> mapper = new();

        mapper.Setup(m => m.Map<List<ProductCategoryDto>>(It.IsAny<List<ProductCategory>>())).Returns(new List<ProductCategoryDto>
        {
            new() { Name = "Gold" },
            new() { Name = "Silver" },
            new() { Name = "Jewlery" }
        });

        ProductCagetoryQueryHandler handler = new(repository.Object, mapper.Object);

        GetProductCategoryQuery query = new();

        //Act

        List<ProductCategoryDto> productCategories = await handler.Handle(query, CancellationToken.None);

        //Assert

        productCategories.Should().NotBeNull();
        productCategories.Should().HaveCountGreaterThan(1);
    }
}
