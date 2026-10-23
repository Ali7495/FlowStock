using MediatR;

namespace Stock.Application;

public sealed record GetAllPricesByProductQuery(Guid productId) : IRequest<List<ProductPriceAllInfoDto>>;
