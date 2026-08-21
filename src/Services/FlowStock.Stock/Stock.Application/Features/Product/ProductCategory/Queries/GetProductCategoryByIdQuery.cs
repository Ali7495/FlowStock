using MediatR;

namespace Stock.Application;

public record GetProductCategoryByIdQuery(Guid id) : IRequest<ProductCategoryDto>;
