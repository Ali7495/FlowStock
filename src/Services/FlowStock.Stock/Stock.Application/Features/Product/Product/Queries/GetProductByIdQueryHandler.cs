using AutoMapper;
using BuildingBlocks.Application;
using MediatR;
using Microsoft.Extensions.Logging;
using Stock.Domain;

namespace Stock.Application;

public sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;
    private readonly ILogger<GetProductByIdQueryHandler> _logger;

    public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper, ICurrentUser currentUser, ILogger<GetProductByIdQueryHandler> logger)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _currentUser = currentUser;
        _logger = logger;
    }

    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("User: {UserId} is getting product by id : {ProductId} including Category.",_currentUser.UserId,request.id);

        Product product = await _productRepository.GetProductWithCategoryByIdAsync(request.id, cancellationToken);
        if (product is null)
        {
            _logger.LogError("Product with id : {ProductId} is not exist!",request.id);
            throw new NullReferenceException("ProductId is invalid");
        }

        ProductDto productDto = _mapper.Map<ProductDto>(product);
        
        return productDto;
    }
}
