using MediatR;

namespace Stock.Application;

public record GetProductByCategoryIdQuery(Guid categoryId) : IRequest<List<ProductDto>>;