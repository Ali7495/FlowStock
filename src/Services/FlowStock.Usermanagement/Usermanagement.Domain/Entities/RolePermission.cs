namespace Usermanagement.Domain;

public class RolePermission : BasicProperties
{
    public Guid RoleId { get; set; }
    public Guid PermissionId { get; set; }

    public Role Role { get; set; }
    public Permission Permission { get; set; }
}
