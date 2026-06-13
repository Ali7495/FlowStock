namespace Usermanagement.Domain;

public class Role : BasicProperities
{
    public string Name { get; set; }
    
    public ICollection<RolePermission> RolePermissions { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
}
