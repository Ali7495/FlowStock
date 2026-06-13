namespace Usermanagement.Domain;

public class Person : BasicProperities
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalCode { get; set; }


    public ICollection<User> Users { get; set; }
}
