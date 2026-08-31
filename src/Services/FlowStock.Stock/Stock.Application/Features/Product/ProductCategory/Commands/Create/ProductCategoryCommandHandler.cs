using BuildingBlocks.Application;
using MediatR;
using Stock.Domain;

namespace Stock.Application;

public sealed class ProductCategoryCommandHandler : IRequestHandler<ProductCategoryCommand, Guid>
{
    private readonly IProductCategoryRepository _productCategoryRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IUnitOfWork _unitOfWork;

    public ProductCategoryCommandHandler(IProductCategoryRepository productCategoryRepository, ICurrentUser currentUser, IUnitOfWork unitOfWork)
    {
        _productCategoryRepository = productCategoryRepository;
        _currentUser = currentUser;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(ProductCategoryCommand request, CancellationToken cancellationToken)
    {
        ProductCategory productCategory = ProductCategory.Create(request.name);
        await _productCategoryRepository.AddAsync(productCategory, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return productCategory.Id;
    }
}
