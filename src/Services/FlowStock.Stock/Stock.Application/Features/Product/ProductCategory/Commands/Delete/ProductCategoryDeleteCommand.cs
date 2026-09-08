using MediatR;

public sealed record ProductCategoryDeleteCommand(Guid id) : IRequest;