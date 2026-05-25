using Core.Domain.Primitives.Interfaces;

namespace Identity.Domain.Projection;

public static class ProjectionModel
{
    public record User(
        Guid Id,
        string Name,
        string Email,
        string? GoogleAvatarUrl,
        DateTimeOffset CreatedAt,
        DateTimeOffset? UpdatedAt,
        bool IsDeleted = false
    ) : IProjectionModel;

    public record RefreshToken(
        Guid Id,
        Guid UserId,
        string TokenHash,
        DateTimeOffset ExpiresAt,
        bool IsRevoked,
        DateTimeOffset? RevokedAt,
        DateTimeOffset CreatedAt,
        DateTimeOffset? UpdatedAt,
        bool IsDeleted = false
    ) : IProjectionModel;
}