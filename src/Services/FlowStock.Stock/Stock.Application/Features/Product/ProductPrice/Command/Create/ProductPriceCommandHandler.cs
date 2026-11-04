using AutoMapper;
using BuildingBlocks.Application;
using MediatR;
using Microsoft.Extensions.Logging;
using Stock.Domain;

namespace Stock.Application;

public sealed class ProductPriceCommandHandler : IRequestHandler<ProductPriceCommand, ProductPriceDto>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _currentUser;
    private readonly ILogger<ProductPriceCommandHandler> _logger;
    private readonly IMapper _mapper;

    public ProductPriceCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, ICurrentUser currentUser, 
    ILogger<ProductPriceCommandHandler> logger, IMapper mapper)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<ProductPriceDto> Handle(ProductPriceCommand request, CancellationToken cancellationToken)
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

        return _mapper.Map<ProductPriceDto>(product);
    }
}
