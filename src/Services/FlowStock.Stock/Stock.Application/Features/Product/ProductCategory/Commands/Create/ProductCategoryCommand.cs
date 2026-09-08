using MediatR;

namespace Stock.Application;

public record ProductCategoryCommand(string name) : IRequest<Guid>;