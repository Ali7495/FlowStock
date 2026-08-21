using AutoMapper;
using MediatR;
using Stock.Domain;

namespace Stock.Application;

public class GetProductCategoryByIdQueryHandler : IRequestHandler<GetProductCategoryByIdQuery, ProductCategoryDto>
{
    private readonly IMapper _mapper;
    private readonly IProductCategoryRepository _productCategoryRepository;

    public GetProductCategoryByIdQueryHandler(IMapper mapper, IProductCategoryRepository productCategoryRepository)
    {
        _productCategoryRepository = productCategoryRepository;
        _mapper = mapper;
    }

    public async Task<ProductCategoryDto> Handle(GetProductCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        ProductCategory productCategory = await _productCategoryRepository.GetByIdAsync(request.id,cancellationToken);

        return _mapper.Map<ProductCategoryDto>(productCategory);
    }
}
