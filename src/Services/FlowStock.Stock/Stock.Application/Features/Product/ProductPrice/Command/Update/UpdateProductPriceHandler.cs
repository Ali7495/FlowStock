using AutoMapper;
using BuildingBlocks.Application;
using MediatR;
using Microsoft.Extensions.Logging;
using Stock.Domain;

namespace Stock.Application;

public sealed class UpdateProductPriceHandler : IRequestHandler<UpdateProductPriceCommand, ProductPriceDto>
{
    private readonly IProductPriceRepository _productPriceRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _currentUser;
    private readonly ILogger<UpdateProductPriceHandler> _logger;
    private readonly IMapper _mapper;

    public UpdateProductPriceHandler(IProductPriceRepository productPriceRepository, IUnitOfWork unitOfWork, ICurrentUser currentUser,
            ILogger<UpdateProductPriceHandler> logger, IMapper mapper)
    {
        _productPriceRepository = productPriceRepository;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<ProductPriceDto> Handle(UpdateProductPriceCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("User: {UserId} is updating price of product: {ProductId} to price: {Price} .", _currentUser.UserId, request.productId, request.price);

        ProductPrice productPrice = await _productPriceRepository.GetActivePriceByProductIdAsync(request.productId, cancellationToken);

        if(productPrice is null)
        {
            _logger.LogError("There is no active price for product : {ProductId} . ", request.productId);
            throw new NotFoundException(nameof(productPrice),request.productId);
        }

        productPrice.Price = request.price;

        _logger.LogInformation("User: {UserId} successfully updated price of product: {ProductId} to the price: {Price}",
                    _currentUser.UserId, request.productId, request.price);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ProductPriceDto>(productPrice);
    }
}
