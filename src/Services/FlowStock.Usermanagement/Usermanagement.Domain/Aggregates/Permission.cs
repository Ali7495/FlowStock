namespace Usermanagement.Domain;

public class Permission
{
    public string Name { get; set; }

    public ICollection<RolePermission> RolePermissions { get; set; }
}
