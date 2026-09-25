using BuildingBlocks.Application;
using BuildingBlocks.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Stock.Domain;

namespace Stock.Application;

public sealed class ProductCommandHandler : IRequestHandler<ProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;
    private readonly IProductCategoryRepository _productCategoryRepository;
    private readonly ICodeGenerator<ProductCode> _codeGenerator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _currentUser;
    private readonly ILogger<ProductCommandHandler> _logger;
    public ProductCommandHandler(IProductRepository productRepository, ICodeGenerator<ProductCode> codeGenerator, IUnitOfWork unitOfWork
            , ICurrentUser currentUser, ILogger<ProductCommandHandler> logger, IProductCategoryRepository productCategoryRepository)
    {
        _productRepository = productRepository;
        _productCategoryRepository = productCategoryRepository;
        _codeGenerator = codeGenerator;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
        _logger = logger;
    }

    public async Task<Guid> Handle(ProductCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("User {userId} is creating Product {productName}", _currentUser.UserId, request.productName);

        if (!await _productCategoryRepository.IsCategoryExistById(request.categoryId, cancellationToken))
        {
            _logger.LogWarning("The category id {categoryId} is not exist", request.categoryId);
            throw new DomainExceptions("CategoryId is not exist!");
        }

        ProductCode productCode = await _codeGenerator.GenerateCodeAsync(cancellationToken);

        Product product = Product.Create(request.categoryId, request.productName, productCode);

        await _productRepository.AddAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("The user {userId} successfully created product: {productName}, with code: {productCode}, and Id: {productId}.",
         _currentUser.UserId, product.Name, product.Code, product.Id);

        return product.Id;
    }
}