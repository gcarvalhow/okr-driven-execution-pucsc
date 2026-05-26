using Core.Domain.Events;
using Core.Domain.Events.Interfaces;

namespace Organization.Domain.Events;

public static class DomainEvent
{
    public sealed record OrganizationCreated(
        Guid Id, 
        string Name, 
        Guid OwnerId,
        DateTimeOffset CreatedAt, 
        ulong Version
    ) : Message, IDomainEvent;

    public sealed record OrganizationUpdated(
        Guid Id,
        string Name,
        DateTimeOffset UpdatedAt,
        ulong Version
    ) : Message, IDomainEvent;

    public sealed record MemberAdded(
        Guid Id,
        Guid UserId,
        Guid OrganizationId,
        string Role,
        DateTimeOffset CreatedAt, 
        ulong Version
    ) : Message, IDomainEvent;
}