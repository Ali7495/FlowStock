using BuildingBlocks.Application;
using MediatR;
using Microsoft.Extensions.Logging;
using Stock.Domain;

namespace Stock.Application;

public sealed class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand,Guid>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _currentUser;
    private readonly ILogger<ProductUpdateCommandHandler> _logger;

    public ProductUpdateCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, ICurrentUser currentUser, ILogger<ProductUpdateCommandHandler> logger)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
        _logger = logger;
    }

    public async Task<Guid> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
    {
        Product product = await _productRepository.GetByIdAsync(request.productId,cancellationToken);

        product.Name = request.productName;
        product.ProductCategoryId = request.categoryId;

        _productRepository.Update(product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}
