using System.Reflection.Metadata;
using AutoMapper;
using FluentAssertions;
using Moq;
using Stock.Domain;

namespace Stock.Application.UnitTests;

public class ProductQueryTests
{
    [Fact]
    public async Task Should_Return_Products_For_Requested_Category()
    {
        // Arrange
        Guid categoryId = Guid.NewGuid();

        ProductCode appleCode = ProductCode.CreateBySequence(4);
        ProductCode sonyCode = ProductCode.CreateBySequence(5);
        ProductCode xiaomiCode = ProductCode.CreateBySequence(6);

        List<Product> products =
        [
            Product.Create(categoryId, "Apple", appleCode),
            Product.Create(categoryId, "Sony", sonyCode),
            Product.Create(categoryId, "Xiaomi", xiaomiCode)
        ];

        List<ProductDto> expectedDtos =
        [
            new() { Name = "Apple", Code = appleCode.Value },
            new() { Name = "Sony", Code = sonyCode.Value },
            new() { Name = "Xiaomi", Code = xiaomiCode.Value }
        ];

        Mock<IProductRepository> productRepository = new();

        productRepository
            .Setup(x => x.GetListByCategoryIdAsync(
                categoryId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(products);

        Mock<IMapper> mapper = new();

        mapper
            .Setup(x => x.Map<List<ProductDto>>(products))
            .Returns(expectedDtos);

        GetProductByCategoryIdHandler handler =
            new(productRepository.Object, mapper.Object);

        GetProductByCategoryIdQuery query = new(categoryId);

        // Act
        List<ProductDto> result =
            await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should()
            .BeEquivalentTo(expectedDtos, options => options.WithStrictOrdering());

        productRepository.Verify(
            x => x.GetListByCategoryIdAsync(
                categoryId,
                It.IsAny<CancellationToken>()),
            Times.Once);

        mapper.Verify(
            x => x.Map<List<ProductDto>>(products),
            Times.Once);
    }
}
