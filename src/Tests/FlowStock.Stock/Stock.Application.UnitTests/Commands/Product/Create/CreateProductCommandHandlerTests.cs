using BuildingBlocks.Application;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.Extensions.Logging;
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

        ProductCode expectedCode = ProductCode.CreateBySequence(3);

        Mock<IProductCategoryRepository> categoryRepository = new();
        categoryRepository.Setup(x => x.IsCategoryExistById(categoryId, It.IsAny<CancellationToken>())).ReturnsAsync(true);

        Mock<ICodeGenerator<ProductCode>> codeGenerator = new();
        codeGenerator.Setup(x => x.GenerateCodeAsync(It.IsAny<CancellationToken>())).ReturnsAsync(expectedCode);

        Mock<ICurrentUser> currentUser = new();
        currentUser.Setup(x => x.PersonId).Returns(personId);

        Mock<IProductRepository> productRepository = new();
        productRepository.Setup(x => x.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        Mock<IUnitOfWork> unitOfWork = new();
        unitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        Mock<ILogger<ProductCommandHandler>> logger = new();

        ProductCommandHandler handler = new(productRepository.Object, codeGenerator.Object, unitOfWork.Object, currentUser.Object, logger.Object, categoryRepository.Object);

        ProductCommand command = new(categoryId, "Samsung");

        //Act

        Guid id = await handler.Handle(command, CancellationToken.None);

        // Assert

        id.Should().NotBeEmpty();

        codeGenerator.Verify(
    x => x.GenerateCodeAsync(
        It.IsAny<CancellationToken>()),
    Times.Once);

        categoryRepository.Verify(x => x.IsCategoryExistById(categoryId, It.IsAny<CancellationToken>()), Times.Once);

        productRepository.Verify(
            x => x.AddAsync(
                It.Is<Product>(p => p.ProductCategoryId == categoryId && p.Name == "Samsung" && p.Code == expectedCode), It.IsAny<CancellationToken>()
                ),
            Times.Once);

        unitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

    }
}
