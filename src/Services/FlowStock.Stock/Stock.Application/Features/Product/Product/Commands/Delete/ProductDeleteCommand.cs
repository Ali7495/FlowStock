using MediatR;

namespace Stock.Application;

public sealed record ProductDeleteCommand(Guid id) : IRequest;
