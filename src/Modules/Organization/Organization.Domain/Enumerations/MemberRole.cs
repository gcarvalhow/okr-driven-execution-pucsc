using Ardalis.SmartEnum;
using Newtonsoft.Json;

namespace Organization.Domain.Enumerations;

[method: JsonConstructor]
public class MemberRole(string name, int value) : SmartEnum<MemberRole>(name, value)
{
    public static readonly MemberRole Admin = new AdminRole();
    public static readonly MemberRole Manager = new ManagerRole();
    public static readonly MemberRole Collaborator = new CollaboratorRole();

    public static implicit operator MemberRole(string name)
        => FromName(name);

    public static implicit operator MemberRole(int value)
        => FromValue(value);

    public static implicit operator string(MemberRole role)
        => role.Name;

    public static implicit operator int(MemberRole role)
        => role.Value;

    public class AdminRole() : MemberRole(nameof(Admin), 0) { }
    public class ManagerRole() : MemberRole(nameof(Manager), 1) { }
    public class CollaboratorRole() : MemberRole(nameof(Collaborator), 2) { }
}