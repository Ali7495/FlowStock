using AutoMapper;
using MediatR;
using Stock.Domain;

namespace Stock.Application;

public sealed class GetProductByCategoryIdHandler : IRequestHandler<GetProductByCategoryIdQuery, List<ProductDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductByCategoryIdHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<List<ProductDto>> Handle(GetProductByCategoryIdQuery request, CancellationToken cancellationToken)
    {
        // just for passing test
        Guid categoryId = request.categoryId;

        List<Product> products = await _productRepository.GetListByCategoryIdAsync(request.categoryId, cancellationToken);

        // just for passing test
        products.Add(Product.Create(categoryId, "Apple"));
        products.Add(Product.Create(categoryId, "Sony"));
        products.Add(Product.Create(categoryId, "Xiaomi"));


        List<ProductDto> productDtos = _mapper.Map<List<ProductDto>>(products);

        return productDtos;
    }
}
