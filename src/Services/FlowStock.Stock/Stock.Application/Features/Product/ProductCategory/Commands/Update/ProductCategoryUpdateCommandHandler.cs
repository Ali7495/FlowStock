using BuildingBlocks.Application;
using MediatR;
using Stock.Domain;

namespace Stock.Application;

public sealed class ProductCategoryUpdateCommandHandler : IRequestHandler<ProductCategoryUpdateCommand>
{
    private readonly IProductCategoryRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _currentUser;

    public ProductCategoryUpdateCommandHandler(IProductCategoryRepository repository, IUnitOfWork unitOfWork, ICurrentUser currentUser)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
    }

    public async Task Handle(ProductCategoryUpdateCommand request, CancellationToken cancellationToken)
    {
        ProductCategory productCategory = await _repository.GetByIdAsync(request.id,cancellationToken);

        productCategory.Name = request.name;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
