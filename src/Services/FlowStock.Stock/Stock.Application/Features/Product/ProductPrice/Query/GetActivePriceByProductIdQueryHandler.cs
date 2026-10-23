using AutoMapper;
using BuildingBlocks.Application;
using MediatR;
using Microsoft.Extensions.Logging;
using Stock.Application;
using Stock.Domain;

public sealed class GetActivePriceByProductIdQueryHandler : IRequestHandler<GetActivePriceByProductIdQuery, ProductPriceDto>
{
    
    private readonly ICurrentUser _currentUser;
    private readonly ILogger<GetActivePriceByProductIdQueryHandler> _logger;
    private readonly IProductPriceRepository _productPriceRepository;
    private readonly IMapper _mapper;

    public GetActivePriceByProductIdQueryHandler(IProductPriceRepository productPriceRepository, ICurrentUser currentUser,
     ILogger<GetActivePriceByProductIdQueryHandler> logger, IMapper mapper)
    {
        _productPriceRepository = productPriceRepository;
        _currentUser = currentUser;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<ProductPriceDto> Handle(GetActivePriceByProductIdQuery request, CancellationToken cancellationToken)
    {
        ProductPrice productPrice = await _productPriceRepository.GetActivePriceByProductIdAsync(request.productId, cancellationToken);

        if (productPrice is null)
        {
            _logger.LogError("There is no active price of product: {productId} . User: {userId}", request.productId, _currentUser.UserId);
            throw new NotFoundException(nameof(productPrice), request.productId);
        }

        _logger.LogInformation("The user {UserId} successfully got active price of product : {ProductId}", _currentUser.UserId, request.productId);

        return _mapper.Map<ProductPriceDto>(productPrice);
    }
}