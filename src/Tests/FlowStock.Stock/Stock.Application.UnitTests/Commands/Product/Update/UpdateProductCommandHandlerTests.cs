using BuildingBlocks.Application;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Stock.Domain;

namespace Stock.Application.UnitTests;

public class UpdateProductCommandHandlerTests
{
    [Fact]
    public async Task Should_Update_Product_When_Command_Is_Valid()
    {
        //Arrange

        Guid categoryId = Guid.NewGuid();
        Guid personId = Guid.NewGuid();
        Guid productId = Guid.NewGuid();


        ProductCode expectedCode = ProductCode.CreateBySequence(3);

        Product product = Product.Create(categoryId, "Samsung", expectedCode);

        product.Id = productId;

        Mock<IProductRepository> productRepository = new();
        productRepository.Setup(x => x.GetByIdAsync(productId, It.IsAny<CancellationToken>())).ReturnsAsync(product);

        Mock<IUnitOfWork> unitOfWork = new();
        unitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        Mock<ICurrentUser> currentUser = new();
        currentUser.Setup(r => r.PersonId).Returns(personId);

        Mock<ILogger<ProductUpdateCommandHandler>> logger = new();

        ProductUpdateCommandHandler handler = new(productRepository.Object, unitOfWork.Object, currentUser.Object, logger.Object);

        ProductUpdateCommand command = new(productId, categoryId, "Apple");

        //Act

        Guid id = await handler.Handle(command, CancellationToken.None);

        //Assert
        id.Should().Be(productId);
        product.Name.Should().Be("Apple");
        product.ProductCategoryId.Should().Be(categoryId);
        productRepository.Verify(x=> x.GetByIdAsync(productId, It.IsAny<CancellationToken>()),Times.Once);
        productRepository.Verify(x => 
            x.Update(It.Is<Product>(p=> p.Id == productId && p.Name == "Apple" && p.ProductCategoryId == categoryId && p.Code == expectedCode))
                , Times.Once);
        unitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
