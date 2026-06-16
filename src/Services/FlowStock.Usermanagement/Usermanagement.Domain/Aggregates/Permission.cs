namespace Usermanagement.Domain;

public class Permission : BasicEntity
{
    public string Name { get; set;}


    public ICollection<RolePermission> RolePermissions { get; set; }
}
