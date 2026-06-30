namespace Usermanagement.Domain;

public class Person : BasicEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalCode { get; set; }

    public static Person Create(string firstName, string lastName, string nationalCode)
    {
        return new()
        {
            FirstName = firstName,
            LastName = lastName,
            NationalCode = nationalCode
        };
    }

    public User CreateUser(string username, Email email, string hashedPassword, string mobile)
    {
        User user = User.Create(username,email,hashedPassword,mobile);

        AddUser(user);

        return user;
    }

    private void AddUser(User user)
    {
        Users.Add(user);
    }

    public ICollection<User> Users { get; set; }
}
