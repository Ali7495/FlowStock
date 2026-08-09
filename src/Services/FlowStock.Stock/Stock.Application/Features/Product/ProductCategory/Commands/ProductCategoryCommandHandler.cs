using BuildingBlocks.Application;
using MediatR;
using Stock.Domain;

namespace Stock.Application;

public sealed class ProductCategoryCommandHandler : IRequestHandler<ProductCategoryCommand, Guid>
{
    private readonly IProductCategoryRepository _productCategoryRepository;
    private readonly ICurrentUser _currentUser;

    public ProductCategoryCommandHandler(IProductCategoryRepository productCategoryRepository, ICurrentUser currentUser)
    {
        _productCategoryRepository = productCategoryRepository;
        _currentUser = currentUser;
    }

    public Task<Guid> Handle(ProductCategoryCommand request, CancellationToken cancellationToken)
    {
        ProductCategory productCategory = ProductCategory.Create(request.name);
        _productCategoryRepository.AddAsync(productCategory, cancellationToken);
        return Task.FromResult(Guid.NewGuid());
    }
}
