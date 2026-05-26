using Core.Domain.Primitives;
using Organization.Domain.Enumerations;

namespace Organization.Domain.Entities;

public class OrganizationMember : Entity
{
    public Guid UserId { get; private set; }
    public Guid OrganizationId { get; private set; }
    public MemberRole Role { get; private set; } = default!;

    public OrganizationMember(Guid id, Guid userId, Guid organizationId, MemberRole role)
    {
        Id = id;
        UserId = userId;
        OrganizationId = organizationId;
        Role = role;
    }

    public static OrganizationMember Create(Guid id, Guid userId, Guid organizationId, MemberRole role)
        => new(id, userId, organizationId, role);
}