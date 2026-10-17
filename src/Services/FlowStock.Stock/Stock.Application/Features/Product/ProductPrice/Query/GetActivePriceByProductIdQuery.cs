using MediatR;
using Stock.Domain;

public sealed record GetActivePriceByProductIdQuery(Guid productId) : IRequest<ProductPrice>;