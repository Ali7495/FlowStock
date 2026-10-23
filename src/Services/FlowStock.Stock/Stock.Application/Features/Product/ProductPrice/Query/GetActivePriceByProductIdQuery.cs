using MediatR;
using Stock.Application;
using Stock.Domain;

public sealed record GetActivePriceByProductIdQuery(Guid productId) : IRequest<ProductPriceDto>;