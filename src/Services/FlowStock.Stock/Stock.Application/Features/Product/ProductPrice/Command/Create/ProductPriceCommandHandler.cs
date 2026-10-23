using BuildingBlocks.Application;
using MediatR;
using Microsoft.Extensions.Logging;
using Stock.Domain;

namespace Stock.Application;

public sealed class ProductPriceCommandHandler : IRequestHandler<ProductPriceCommand, Product>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _currentUser;
    private readonly ILogger<ProductPriceCommandHandler> _logger;

    public ProductPriceCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, ICurrentUser currentUser, ILogger<ProductPriceCommandHandler> logger)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
        _logger = logger;
    }

    public async Task<Product> Handle(ProductPriceCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("User: {UserId} is adding price to product: {ProductId} .",_currentUser.UserId, request.productId);

        Product product = await _productRepository.GetProductWithPricesByIdAsync(request.productId, cancellationToken);

        if (product is null)
        {
            _logger.LogError("The product: {ProductId} is not exist!" , request.productId);
            throw new NotFoundException(nameof(Product),request.productId);
        }

        product.CreateProductPrice(request.price);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("The price : {Price} is updated for product : {ProductId} ", request.price, request.productId);

        return product;
    }
}
