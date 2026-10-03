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
        _logger.LogInformation($"User: {_currentUser.UserId} is getting product by id : {request.id} including Category.");

        Product product = await _productRepository.GetProductWithCategoryById(request.id, cancellationToken);
        if (product is null)
        {
            _logger.LogError($"Product with id : {request.id} is not exist!");
            throw new NullReferenceException("ProductId is invalid");
        }

        ProductDto productDto = _mapper.Map<ProductDto>(product);
        
        return productDto;
    }
}
