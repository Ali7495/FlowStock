using MediatR;

namespace Usermanagement.Application;

public sealed record RegisterCommand(string FirstName, string LastName, string NationalCode, string Username, string Email, string Mobile, string Password) : IRequest<Guid>;
