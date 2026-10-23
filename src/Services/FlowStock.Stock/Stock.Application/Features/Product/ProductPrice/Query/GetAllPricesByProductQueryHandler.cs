using AutoMapper;
using BuildingBlocks.Application;
using MediatR;
using Microsoft.Extensions.Logging;
using Stock.Domain;

namespace Stock.Application;

public sealed class GetAllPricesByProductQueryHandler : IRequestHandler<GetAllPricesByProductQuery, List<ProductPriceAllInfoDto>>
{
    private readonly IProductPriceRepository _productPriceRepository;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;
    private readonly ILogger<GetAllPricesByProductQueryHandler> _logger;

    public GetAllPricesByProductQueryHandler(IProductPriceRepository productPriceRepository, IMapper mapper, ICurrentUser currentUser,
        ILogger<GetAllPricesByProductQueryHandler> logger)
    {
        _productPriceRepository = productPriceRepository;
        _mapper = mapper;
        _currentUser = currentUser;
        _logger = logger;
    }

    public async Task<List<ProductPriceAllInfoDto>> Handle(GetAllPricesByProductQuery request, CancellationToken cancellationToken)
    {
        List<ProductPrice> productPrices = await _productPriceRepository.GetAllPricesByProductIdAsync(request.productId, cancellationToken);

        _logger.LogInformation("The user {UserId} successfully get prices of product : {ProductId} .", _currentUser.UserId, request.productId);

        return _mapper.Map<List<ProductPriceAllInfoDto>>(productPrices);
    }
}
