using MediatR;

namespace Stock.Application;

public record ProductUpdateCommand(Guid productId, Guid categoryId, string productName) : IRequest<Guid>;
