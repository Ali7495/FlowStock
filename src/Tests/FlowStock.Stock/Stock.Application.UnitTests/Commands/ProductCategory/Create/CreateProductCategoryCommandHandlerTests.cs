using BuildingBlocks.Application;
using FluentAssertions;
using Moq;
using Stock.Domain;

namespace Stock.Application.UnitTests;

public class CreateProductCategoryCommandHandlerTests
{
    [Fact]
    public async Task Should_Create_ProductCategory_When_Command_Is_Valid()
    {
        //Arrange
        Mock<IProductCategoryRepository> categoryMock = new();

        categoryMock.Setup(c=> c.IsCategoryExistByName("Gold",It.IsAny<CancellationToken>())).ReturnsAsync(false);

        Mock<ICurrentUser> currentUser = new();

        currentUser.Setup(c=> c.PersonId).Returns(Guid.NewGuid());

        ProductCategoryCommandHandler handler = new(categoryMock.Object, currentUser.Object);

        ProductCategoryCommand command = new("Gold");

        //Act
        Guid id = await handler.Handle(command, CancellationToken.None);

        //Assert
        id.Should().NotBeEmpty();
        categoryMock.Verify(c=> c.AddAsync(It.IsAny<ProductCategory>(),It.IsAny<CancellationToken>()),Times.Once);
    }

    
}
