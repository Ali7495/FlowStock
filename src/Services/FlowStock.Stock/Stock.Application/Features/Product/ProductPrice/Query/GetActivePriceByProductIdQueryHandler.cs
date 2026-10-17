using BuildingBlocks.Application;
using MediatR;
using Microsoft.Extensions.Logging;
using Stock.Domain;

public sealed class GetActivePriceByProductIdQueryHandler : IRequestHandler<GetActivePriceByProductIdQuery, ProductPrice>
{
    
    private readonly ICurrentUser _currentUser;
    private readonly ILogger<GetActivePriceByProductIdQueryHandler> _logger;

    public GetActivePriceByProductIdQueryHandler(IProductRepository productRepository, ICurrentUser currentUser, ILogger<GetActivePriceByProductIdQueryHandler> logger)
    {
        
    }

    public Task<ProductPrice> Handle(GetActivePriceByProductIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}