using MediatR;

namespace Stock.Application;

public record ProductCategoryCommand() : IRequest<Guid>;