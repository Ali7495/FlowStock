using MediatR;

namespace Stock.Application;

public sealed record GetProductByIdQuery(Guid id) : IRequest<ProductDto>;
