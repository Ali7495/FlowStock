using BuildingBlocks.Application;
using BuildingBlocks.Domain;
using MediatR;
using Stock.Domain;

public sealed class ProductCategoryDeleteCommandHandler : IRequestHandler<ProductCategoryDeleteCommand>
{
    private readonly IProductCategoryRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _currentUser;

    public ProductCategoryDeleteCommandHandler(IProductCategoryRepository repository, IUnitOfWork unitOfWork, ICurrentUser currentUser)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
    }

    public async Task Handle(ProductCategoryDeleteCommand request, CancellationToken cancellationToken)
    {
        ProductCategory productCategory = await _repository.GetWithProductsById(request.id,cancellationToken);

        if (productCategory.Products is not null && productCategory.Products.Count > 0)
            throw new DomainExceptions("Has Products!");

        productCategory.IsDeleted = true;

        await _unitOfWork.SaveChangesAsync(cancellationToken);    
    }
}