using MediatR;

namespace Stock.Application;

public sealed record ProductCategoryUpdateCommand(Guid id, string name) : IRequest;
