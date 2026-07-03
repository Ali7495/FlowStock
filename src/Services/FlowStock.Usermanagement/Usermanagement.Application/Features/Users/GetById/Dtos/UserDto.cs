namespace Usermanagement.Application;

public sealed record UserDto(Guid Id,
    string FirstName,
    string LastName,
    string Username,
    string Email,
    string Mobile);
