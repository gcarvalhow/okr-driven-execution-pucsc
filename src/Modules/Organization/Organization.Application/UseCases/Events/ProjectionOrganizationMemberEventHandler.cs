using Core.Application.EventBus;
using Microsoft.Extensions.Logging;
using Organization.Domain.Events;
using Organization.Domain.Projection;
using Organization.Persistence.Projection;

namespace Organization.Application.UseCases.Events;

public interface IProjectionOrganizationMemberEventHandler : 
    IEventHandler<DomainEvent.MemberAdded>;

public class ProjectionOrganizationMemberEventHandler(
    IOrganizationProjection<ProjectionModel.OrganizationMember> organizationMemberProjection,
    ILogger<ProjectionOrganizationMemberEventHandler> logger
) : IProjectionOrganizationMemberEventHandler
{
    public async Task Handle(DomainEvent.MemberAdded @event, CancellationToken cancellationToken = default)
    {
        try
        {
            await organizationMemberProjection.ReplaceInsertAsync(new(
                @event.Id,
                @event.UserId,
                @event.OrganizationId,
                @event.Role,
                @event.CreatedAt,
                null
            ), cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to project member added: {Id}.", @event.Id);
            throw new InvalidOperationException($"Failed to project member added: {@event.Id}.", ex);
        }
    }
}