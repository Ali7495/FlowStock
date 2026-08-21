using AutoMapper;
using MediatR;
using Stock.Domain;

namespace Stock.Application;

public class GetAllProductCagetoryQueryHandler : IRequestHandler<GetAllProductCategoryQuery, List<ProductCategoryDto>>
{
    private readonly IProductCategoryRepository _repository;
    private readonly IMapper _mapper;

    public GetAllProductCagetoryQueryHandler(IProductCategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<ProductCategoryDto>> Handle(GetAllProductCategoryQuery request, CancellationToken cancellationToken)
    {
        List<ProductCategory> productCategories = await _repository.GetAllAsync(cancellationToken);
        List<ProductCategoryDto> mapped = _mapper.Map<List<ProductCategoryDto>>(productCategories);
        return mapped;
    }
}
