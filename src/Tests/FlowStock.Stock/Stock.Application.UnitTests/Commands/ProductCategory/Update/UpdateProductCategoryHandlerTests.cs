using System.Data.Common;
using BuildingBlocks.Application;
using Moq;
using Stock.Domain;

namespace Stock.Application.UnitTests;

public class UpdateProductCategoryHandlerTests
{
    [Fact]
    public async Task Should_Update_ProductCategory_When_Command_Is_Valid()
    {
        // Arrange
        ProductCategory productCategory = ProductCategory.Create("Test");

        Mock<IProductCategoryRepository> repository = new();
        repository.Setup(r=> r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(productCategory);

        Mock<IUnitOfWork> unitOfWork = new();
        unitOfWork.Setup(r=> r.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        Mock<ICurrentUser> currentUser = new();
        currentUser.Setup(r=> r.PersonId).Returns(Guid.NewGuid());

        ProductCategoryUpdateCommandHandler handler = new(repository.Object, unitOfWork.Object, currentUser.Object);

        
        Guid id = Guid.NewGuid();

        ProductCategoryUpdateCommand productCategoryCommand = new(id,"Test2");

        // Act

        await handler.Handle(productCategoryCommand, CancellationToken.None);

        // Assert

        //repository.Verify(c=> c.Update(It.IsAny<ProductCategory>()),Times.Once);
        unitOfWork.Verify(c=> c.SaveChangesAsync(It.IsAny<CancellationToken>()),Times.Once);
    }
}
