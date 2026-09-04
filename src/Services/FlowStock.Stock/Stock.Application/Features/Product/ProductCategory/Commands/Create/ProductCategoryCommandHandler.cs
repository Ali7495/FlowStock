using BuildingBlocks.Application;
using MediatR;
using Microsoft.Extensions.Logging;
using Stock.Domain;

namespace Stock.Application;

public sealed class ProductCategoryCommandHandler : IRequestHandler<ProductCategoryCommand, Guid>
{
    private readonly IProductCategoryRepository _productCategoryRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ProductCategoryCommandHandler> _logger;

    public ProductCategoryCommandHandler(IProductCategoryRepository productCategoryRepository, ICurrentUser currentUser
    , IUnitOfWork unitOfWork, ILogger<ProductCategoryCommandHandler> logger)
    {
        _productCategoryRepository = productCategoryRepository;
        _currentUser = currentUser;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Guid> Handle(ProductCategoryCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("User {userId} is creating ProductCategory {category}", _currentUser.UserId, request.name);

        ProductCategory productCategory = ProductCategory.Create(request.name);
        await _productCategoryRepository.AddAsync(productCategory, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("User {userId} created ProductCategory {category} with id {categoryId}",
         _currentUser.UserId, productCategory.Name, productCategory.Id);
         
        return productCategory.Id;
    }
}
