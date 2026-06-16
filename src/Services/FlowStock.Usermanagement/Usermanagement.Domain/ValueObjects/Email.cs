using System.Text.RegularExpressions;

namespace Usermanagement.Domain;

public record Email
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("Email is required!");

        email = email.Trim().ToLowerInvariant();

        if (!IsValid(email))
            throw new DomainException("Email is not valid!");

        return Email.Create(email);
    }

    private static bool IsValid(string email)
    {
        return Regex.IsMatch(
            email,
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.IgnoreCase);
    }

    public override string ToString() => Value;

    public static implicit operator string(Email email) => email.Value;
}
