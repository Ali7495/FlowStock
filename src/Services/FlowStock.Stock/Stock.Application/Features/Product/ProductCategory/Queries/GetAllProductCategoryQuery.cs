using MediatR;

namespace Stock.Application;

public record GetAllProductCategoryQuery : IRequest<List<ProductCategoryDto>>
{

}
