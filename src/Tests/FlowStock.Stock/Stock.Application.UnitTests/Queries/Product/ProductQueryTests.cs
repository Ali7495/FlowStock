using System.Reflection.Metadata;
using AutoMapper;
using FluentAssertions;
using Moq;
using Stock.Domain;

namespace Stock.Application.UnitTests;

public class ProductQueryTests
{
    [Theory]
    [InlineData("012dc93c-b4c2-428f-8f34-658314806b7a")]
    public async Task Should_Returns_Products_By_ProductCategoryId(Guid productCategoryId)
    {
        Guid categoryId = Guid.NewGuid();

         List<Product> products =
        [
            Product.Create(categoryId, "Apple", ProductCode.CreateBySequence(4)),
            Product.Create(categoryId, "Sony", ProductCode.CreateBySequence(5)),
            Product.Create(categoryId, "Xiaomi", ProductCode.CreateBySequence(6))
        ];

        List<ProductDto> expectedDtos =
        [
            new() {Name = "Apple", Code = ProductCode.CreateBySequence(4).ToString()},
            new() {Name = "Sony", Code = ProductCode.CreateBySequence(5).ToString()},
            new() {Name = "Xiaomi", Code = ProductCode.CreateBySequence(6).ToString()}
        ];

        Mock<IProductRepository> productRepository = new();
        productRepository.Setup(x=> x.GetListByCategoryIdAsync(categoryId, It.IsAny<CancellationToken>())).ReturnsAsync(products);

        Mock<IMapper> mapper = new();
        mapper.Setup(x=> x.Map<List<ProductDto>>(products)).Returns(new List<ProductDto>
        {
            new() {Name = "Apple", Code = ProductCode.CreateBySequence(4).ToString()},
            new() {Name = "Sony", Code = ProductCode.CreateBySequence(5).ToString()},
            new() {Name = "Xiaomi", Code = ProductCode.CreateBySequence(6).ToString()}
        });

        GetProductByCategoryIdHandler handler = new(productRepository.Object, mapper.Object);

        GetProductByCategoryIdQuery query = new(categoryId);

        //Act

        List<ProductDto> result = await handler.Handle(query, CancellationToken.None);

        //Assert

        result.Should().NotBeNull();
        result.Should().HaveCount(3);
        result.Should().BeEquivalentTo(expectedDtos);

        productRepository.Verify(x=> x.GetListByCategoryIdAsync(categoryId,It.IsAny<CancellationToken>()), Times.Once);
        mapper.Verify(
            x => x.Map<List<ProductDto>>(products),
            Times.Once);
    }
}
