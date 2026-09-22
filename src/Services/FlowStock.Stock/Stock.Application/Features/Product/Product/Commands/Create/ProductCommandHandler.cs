using BuildingBlocks.Application;
using MediatR;
using Microsoft.Extensions.Logging;
using Stock.Domain;

namespace Stock.Application;

public sealed class ProductCommandHandler : IRequestHandler<ProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;
    private readonly ICodeGenerator<ProductCode> _codeGenerator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _currentUser;
    private readonly ILogger<ProductCommandHandler> _logger;
    public ProductCommandHandler(IProductRepository productRepository, ICodeGenerator<ProductCode> codeGenerator,  IUnitOfWork unitOfWork
            , ICurrentUser currentUser, ILogger<ProductCommandHandler> logger)
    {
        _productRepository = productRepository;
        _codeGenerator = codeGenerator;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
        _logger = logger;
    }

    public async Task<Guid> Handle(ProductCommand request, CancellationToken cancellationToken)
    {
        ProductCode productCode = await _codeGenerator.GenerateCodeAsync(cancellationToken);

        Product product = Product.Create(request.categoryId, request.productName, productCode);

        await _productRepository.AddAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Guid.NewGuid();
    }
}