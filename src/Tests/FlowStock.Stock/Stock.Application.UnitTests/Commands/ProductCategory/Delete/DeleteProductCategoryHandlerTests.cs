using BuildingBlocks.Application;
using BuildingBlocks.Domain;
using Moq;
using Stock.Domain;

namespace Stock.Application.UnitTests;

public class DeleteProductCategoryHandlerTests
{
    [Fact]
    public async Task Should_Delete_When_Does_Not_Have_Products()
    {
        // Act
        ProductCategory productCategory = new()
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Products = new List<Product>()
        };

        Mock<IProductCategoryRepository> repository = new();
        repository.Setup(r => r.GetWithProductsById(productCategory.Id, It.IsAny<CancellationToken>())).ReturnsAsync(productCategory);

        Mock<IUnitOfWork> unitOfWork = new();
        unitOfWork.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        Mock<ICurrentUser> currentUser = new();
        currentUser.Setup(r => r.PersonId).Returns(Guid.NewGuid);

        ProductCategoryDeleteCommandHandler handler = new(repository.Object, unitOfWork.Object, currentUser.Object);

        ProductCategoryDeleteCommand command = new(productCategory.Id);

        // Arrange

        await handler.Handle(command, CancellationToken.None);

        // Assert


        unitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Should_Not_Delete_When_Has_Products()
    {
        // Arrange
        ProductCategory productCategory = new()
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Products = new List<Product>()
            {
                new Product()
            }
        };

        Mock<IProductCategoryRepository> repository = new();
        repository.Setup(r => r.GetWithProductsById(productCategory.Id, It.IsAny<CancellationToken>())).ReturnsAsync(productCategory);

        Mock<IUnitOfWork> unitOfWork = new();
        unitOfWork.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        Mock<ICurrentUser> currentUser = new();
        currentUser.Setup(r => r.PersonId).Returns(Guid.NewGuid);

        ProductCategoryDeleteCommandHandler handler = new(repository.Object, unitOfWork.Object, currentUser.Object);

        ProductCategoryDeleteCommand command = new(productCategory.Id);

        // Act & Assert

        await Assert.ThrowsAsync<DomainExceptions>(() => handler.Handle(command, CancellationToken.None));

        Assert.False(productCategory.IsDeleted);

        unitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}