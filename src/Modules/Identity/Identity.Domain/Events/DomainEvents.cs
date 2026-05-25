using Core.Domain.Events;
using Core.Domain.Events.Interfaces;

namespace Identity.Domain.Events;

public static class DomainEvents
{
    #region User Events
    public sealed record UserSecurityStampRegenerated(
        Guid UserId,
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