using BuildingBlocks.Application;
using BuildingBlocks.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Stock.Domain;

namespace Stock.Application;

public sealed class ProductDeleteCommandHandler : IRequestHandler<ProductDeleteCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _currentUser;
    private readonly ILogger<ProductDeleteCommandHandler> _logger;

    public ProductDeleteCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, ICurrentUser currentUser
    , ILogger<ProductDeleteCommandHandler> logger)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
        _logger = logger;
    }

    public async Task Handle(ProductDeleteCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"User: {_currentUser.UserId} is deleting product: {request.id}");

        Product product = await _productRepository.GetByIdAsync(request.id, cancellationToken);
        if (product is null)
        {
            _logger.LogError($"Product with id : {request.id} is invalid!");
            throw new NullReferenceException("Invalid ProductId");
        }

        if (await _productRepository.HasDependenciesAsync(request.id,cancellationToken))
        {
            _logger.LogWarning($"Product with id {request.id} is in use so can not be deleted!");
            throw new DomainExceptions("Product is in use so can not be deleted!");
        }

        _productRepository.Delete(product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation($"Product with id : {request.id} has been successfully deleted by User: {_currentUser.UserId}");

    }

}
