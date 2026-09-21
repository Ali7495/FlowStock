using MediatR;

namespace Stock.Application;

public record ProductCommand(Guid categoryId, string productName) : IRequest<Guid>;
