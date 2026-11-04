using BuildingBlocks.Application;
using MediatR;
using Microsoft.Extensions.Logging;
using Stock.Domain;

namespace Stock.Application;

public sealed class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand, Guid>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _currentUser;
    private readonly ILogger<ProductUpdateCommandHandler> _logger;

    public ProductUpdateCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, ICurrentUser currentUser, ILogger<ProductUpdateCommandHandler> logger)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
        _logger = logger;
    }

    public async Task<Guid> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("User: {UserId} is updating product: {productId} - {productName}", _currentUser.UserId, request.productId, request.productName);

        Product product = await _productRepository.GetByIdAsync(request.productId, cancellationToken);
        if (product is null)
        {
            _logger.LogError("User: {UserId} tried to update invalid productId: {productId}", _currentUser.UserId, request.productId);
            throw new NullReferenceException("Invalid ProductId");
        }

        _logger.LogInformation("Current Product: id = {productId} - code = {productCode} - categoryId = {productProductCategoryId} - name = {productName}",
        product.Id, product.Code, request.categoryId, request.productName);

        product.Name = request.productName;
        product.ProductCategoryId = request.categoryId;

        _productRepository.Update(product);

         _logger.LogInformation("Updated Product: id = {productId} - code = {productCode} - categoryId = {categoryId} - name = {productName}",
         product.Id, product.Code, request.categoryId, product.Name);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("User: {UserId} successfully updated product: {productId} - {productCode}",
        _currentUser.UserId, product.Id, product.Code);
        

        return product.Id;
    }
}
