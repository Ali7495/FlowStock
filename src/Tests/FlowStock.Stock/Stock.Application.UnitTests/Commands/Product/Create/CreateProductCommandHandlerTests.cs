using BuildingBlocks.Application;
using Castle.Core.Logging;
using Moq;
using Stock.Domain;

namespace Stock.Application.UnitTests;


public class CreateProductCommandHandlerTests
{
    [Fact]
    public async Task Should_Create_Product_When_Command_Is_Valid()
    {
        //Arrange
        Guid categoryId = Guid.NewGuid();
        Guid personId = Guid.NewGuid();

        Mock<IProductRepository> productRepository = new();
        productRepository.Setup(x => x.IsProductCategoryValid(categoryId, It.IsAny<CancellationToken>())).ReturnsAsync(true);

        Mock<ICurrentUser> currentUser = new();
        currentUser.Setup(x => x.PersonId).Returns(personId);

        Mock<IUnitOfWork> unitOfWork = new();
        unitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        Mock<ILogger<ProductCommandHandler>> logger = new();

        ProductCommandHandler handler = new(productRepository.Object, unitOfWork.Object, currentUser.Object, logger.Object);

        ProductCommand command = new(categoryId, "Samsung");

        //Act

        Guid id = await handler.Handle(command, CancellationToken.None);

        // Assert

        productRepository.Verify(
            x => x.AddAsync(
                It.Is<Product>(p => p.ProductCategoryId == categoryId && p.Name == "Samsung"), It.IsAny<CancellationToken>()
                ),
            Times.Once);

        unitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

    }
}
