using Core.Domain.Events.Interfaces;
using Core.Domain.Primitives;
using Identity.Domain.Entities;
using Identity.Domain.Events;

namespace Identity.Domain.Aggregates;

public class User : AggregateRoot
{
    public string GoogleId { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string? GoogleAvatarUrl { get; private set; } = string.Empty;
    public Guid SecurityStamp { get; private set; }
    public List<RefreshToken> RefreshTokens { get; private set; } = [];

    #region User Methods
    public static User Create(string googleId, string name, string email, string? googleAvatarUrl)
    {
        User user = new();

        user.RaiseEvent<DomainEvent.UserRegistered>(version => new(
            Id: user.Id,
            GoogleId: googleId,
            Name: name,
            Email: email,
            GoogleAvatarUrl: googleAvatarUrl,
            SecurityStamp: Guid.NewGuid(),
            CreatedAt: user.CreatedAt,
            Version: version
        ));

        return user;
    }

    public void UpdateName(string Name)
    {
        RaiseEvent<DomainEvent.UserNameUpdated>(version => new(
            Id: Id,
            Name: Name,
            UpdatedAt: DateTimeOffset.UtcNow,
            Version: version
        ));
    }

    public void RegenerateSecurityStamp()
    {
        RaiseEvent<DomainEvent.UserSecurityStampRegenerated>(version => new(
            Id: Id,
            SecurityStamp: Guid.NewGuid(),
            UpdatedAt: DateTimeOffset.UtcNow,
            Version: version
        ));
    }
    #endregion

    #region RefreshToken Methods
    public Guid AddRefreshToken(string tokenHash, DateTimeOffset expiresAt)
    {
        var tokenId = Guid.NewGuid();

        RaiseEvent<DomainEvent.UserRefreshTokenAdded>(version => new(
            UserId: Id,
            TokenId: tokenId,
            TokenHash: tokenHash,
            ExpiresAt: expiresAt,
            CreatedAt: DateTimeOffset.UtcNow,
            Version: version
        ));

        return tokenId;
    }

    public void RevokeRefreshToken(Guid tokenId)
    {
        RaiseEvent<DomainEvent.UserRefreshTokenRevoked>(version => new(
            UserId: Id,
            TokenId: tokenId,
            RevokedAt: DateTimeOffset.UtcNow,
            Version: version
        ));
    }

    public void RevokeAllRefreshTokens()
    {
        foreach (var token in RefreshTokens.Where(t => t.IsValid()))
            RevokeRefreshToken(token.Id);
    }
    #endregion

    protected override void ApplyEvent(IDomainEvent @event)
        => When(@event as dynamic);

    #region User Event Handlers
    private void When(DomainEvent.UserRegistered @event)
    {
        Id = @event.Id;
        GoogleId = @event.GoogleId;
        Name = @event.Name;
        Email = @event.Email;
        GoogleAvatarUrl = @event.GoogleAvatarUrl;
        SecurityStamp = @event.SecurityStamp;
    }

    private void When(DomainEvent.UserNameUpdated @event)
    {
        Name = @event.Name;
        UpdatedAt = @event.UpdatedAt;
    }

    private void When(DomainEvent.UserSecurityStampRegenerated @event)
    {
        SecurityStamp = @event.SecurityStamp;
        UpdatedAt = @event.UpdatedAt;
    }
    #endregion

    #region RefreshToken Event Handlers
    private void When(DomainEvent.UserRefreshTokenAdded @event)
        => RefreshTokens.Add(RefreshToken.Create(@event.TokenId, @event.UserId, @event.TokenHash, @event.ExpiresAt));

    private void When(DomainEvent.UserRefreshTokenRevoked @event)
    {
        var token = RefreshTokens.FirstOrDefault(t => t.Id == @event.TokenId);
        token?.Revoke(@event.RevokedAt);
    }
    #endregion
}