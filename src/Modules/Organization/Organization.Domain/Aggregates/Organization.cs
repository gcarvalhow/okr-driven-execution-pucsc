using Core.Domain.Events.Interfaces;
using Core.Domain.Primitives;
using Organization.Domain.Entities;
using Organization.Domain.Enumerations;
using Organization.Domain.Events;

namespace Organization.Domain.Aggregates;

public class Organization : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public Guid OwnerId { get; private set; }

    private readonly List<OrganizationMember> _members = [];
    public IReadOnlyList<OrganizationMember> Members => _members.AsReadOnly();

    public static Organization Create(string name, Guid ownerId)
    {
        Organization organization = new();

        organization.RaiseEvent<DomainEvent.OrganizationCreated>(version => new(
            Id: organization.Id,
            Name: name,
            OwnerId: ownerId,
            CreatedAt: organization.CreatedAt,
            Version: version
        ));

        return organization;
    }

    public void Update(string name)
    {
        RaiseEvent<DomainEvent.OrganizationUpdated>(version => new(
            Id: Id,
            Name: name,
            UpdatedAt: DateTimeOffset.UtcNow,
            Version: version
        ));
    }

    public void AddMember(Guid userId, string role)
    {
        var memberId = Guid.NewGuid();

        RaiseEvent<DomainEvent.MemberAdded>(version => new(
            Id: memberId,
            UserId: userId,
            OrganizationId: Id,
            Role: role,
            CreatedAt: DateTimeOffset.UtcNow,
            Version: version
        ));
    }

    protected override void ApplyEvent(IDomainEvent @event)
        => When(@event as dynamic);

    private void When(DomainEvent.OrganizationCreated @event)
    {
        Id = @event.Id;
        Name = @event.Name;
        OwnerId = @event.OwnerId;
    }

    private void When(DomainEvent.OrganizationUpdated @event)
    {
        Name = @event.Name;
    }

    private void When(DomainEvent.MemberAdded @event)
    {
        _members.Add(OrganizationMember.Create(
            id: @event.Id,
            userId: @event.UserId,
            organizationId: @event.OrganizationId,
            role: MemberRole.FromName(@event.Role)
        ));
    }
}