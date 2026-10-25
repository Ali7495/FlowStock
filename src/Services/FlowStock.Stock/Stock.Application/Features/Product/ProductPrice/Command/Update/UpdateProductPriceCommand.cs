using MediatR;

namespace Stock.Application;

public sealed record UpdateProductPriceCommand(Guid productId, decimal price) : IRequest<ProductPriceDto>;