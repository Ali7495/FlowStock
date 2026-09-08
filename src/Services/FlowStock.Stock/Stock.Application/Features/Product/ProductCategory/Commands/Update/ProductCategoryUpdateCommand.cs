using MediatR;

namespace Stock.Application;

public sealed record ProductCategoryUpdateCommand : IRequest
{
    public Guid id { get; set; }
    public string name { get; set; }
}
