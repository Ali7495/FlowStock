using MediatR;
using Stock.Domain;

namespace Stock.Application;

public sealed record ProductPriceCommand(Guid productId, decimal price) : IRequest<Product>;
