using MediatR;

namespace Stock.Application;

public record GetProductCategoryQuery : IRequest<List<ProductCategoryDto>>
{

}
