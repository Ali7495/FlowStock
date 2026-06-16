namespace Usermanagement.Domain;

public class Role : BasicEntity 
{
    public string Name { get; set; }

    public ICollection<UserRole> UserRoles { get; set; }
    public ICollection<RolePermission> RolePermissions { get; set; }
    
}
