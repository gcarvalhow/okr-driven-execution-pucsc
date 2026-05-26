using Core.Domain.Events;
using Core.Domain.Events.Interfaces;

namespace Identity.Domain.Events;

public static class DomainEvent
{
    #region User Events
    public sealed record UserRegistered(
        Guid Id,
        string GoogleId,
        string Name,
        string Email,
        string? GoogleAvatarUrl,
        Guid SecurityStamp,
        DateTimeOffset CreatedAt,
        ulong Version
    ) : Message, IDomainEvent;

    public sealed record UserNameUpdated(
        Guid Id,
        string Name,
        DateTimeOffset UpdatedAt,
        ulong Version
    ) : Message, IDomainEvent;

    public sealed record UserSecurityStampRegenerated(
        Guid Id,
        Guid SecurityStamp,
        DateTimeOffset UpdatedAt,
        ulong Version
    ) : Message, IDomainEvent;
    #endregion

    #region RefreshToken Events
    public sealed record UserRefreshTokenAdded(
        Guid UserId,
        Guid TokenId,
        string TokenHash,
        DateTimeOffset ExpiresAt,
        DateTimeOffset CreatedAt,
        ulong Version
    ) : Message, IDomainEvent;

    public sealed record UserRefreshTokenRevoked(
        Guid UserId,
        Guid TokenId,
        DateTimeOffset RevokedAt,
        ulong Version
    ) : Message, IDomainEvent;
    #endregion
}