using AutoMapper;
using MediatR;
using Stock.Domain;

namespace Stock.Application;

public class ProductCagetoryQueryHandler : IRequestHandler<GetProductCategoryQuery, List<ProductCategoryDto>>
{
    private readonly IProductCategoryRepository _repository;
    private readonly IMapper _mapper;

    public ProductCagetoryQueryHandler(IProductCategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<ProductCategoryDto>> Handle(GetProductCategoryQuery request, CancellationToken cancellationToken)
    {
        List<ProductCategory> productCategories = await _repository.GetAllAsync(cancellationToken);
        List<ProductCategoryDto> fakeList = _mapper.Map<List<ProductCategoryDto>>(productCategories);
        return fakeList;
    }
}
