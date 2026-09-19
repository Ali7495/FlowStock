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
        Mock<IProductRepository> productMock = new();

        productMock.Setup(x=> x.IsProductCategoryValid("Mobile",It.IsAny<CancellationToken>())).ReturnsAsync(true);

        Mock<ICurrentUser> currentUser = new();

        currentUser.Setup(x=> x.PersonId).Returns(Guid.NewGuid());

        Mock<IUnitOfWork> unitOfWork = new();

        unitOfWork.Setup(x=> x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        Mock<ILogger<ProductCommandHandler>> logger = new();

        ProductCommandHandler handler = new(productMock.Object, currentUser.Object, logger.Object);

        ProductCommand command = new("Samsung");

        //Act

        Guid id = await handler.Handle(command, CancellationToken.None);

        // Assert

        productMock.Verify(x=> x.AddAsync(It.IsAny<Product>(),It.IsAny<CancellationToken>()),Times.Once);

    }
}
