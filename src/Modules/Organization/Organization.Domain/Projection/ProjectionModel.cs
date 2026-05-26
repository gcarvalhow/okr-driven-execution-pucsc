using Core.Domain.Primitives.Interfaces;

namespace Organization.Domain.Projection;

public static class ProjectionModel
{
    public record Organization(
        Guid Id,
        string Name,
        Guid OwnerId,
        DateTimeOffset CreatedAt,
        DateTimeOffset? UpdatedAt,
        bool IsDeleted = false
    ) : IProjectionModel;

    public record OrganizationMember(
        Guid Id,
        Guid UserId,
        Guid OrganizationId,
        string MemberRole,
        DateTimeOffset CreatedAt,
        DateTimeOffset? UpdatedAt,
        bool IsDeleted = false
    ) : IProjectionModel;
}