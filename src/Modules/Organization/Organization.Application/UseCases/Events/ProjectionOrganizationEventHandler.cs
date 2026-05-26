using Core.Application.EventBus;
using Microsoft.Extensions.Logging;
using Organization.Domain.Events;
using Organization.Domain.Projection;
using Organization.Persistence.Projection;

namespace Organization.Application.UseCases.Events;

public interface IProjectionOrganizationEventHandler : 
    IEventHandler<DomainEvent.OrganizationCreated>,
    IEventHandler<DomainEvent.OrganizationUpdated>;

public class ProjectionOrganizationEventHandler(
    IOrganizationProjection<ProjectionModel.Organization> organizationProjection,
    ILogger<ProjectionOrganizationEventHandler> logger
) : IProjectionOrganizationEventHandler
{
    public async Task Handle(DomainEvent.OrganizationCreated @event, CancellationToken cancellationToken = default)
    {
        try
        {
            await organizationProjection.ReplaceInsertAsync(new(
                @event.Id,
                @event.Name,
                @event.OwnerId,
                @event.CreatedAt,
                null
            ), cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to create organization projection: {Id}.", @event.Id);
            throw new InvalidOperationException($"Failed to create organization projection: {@event.Id}.", ex);
        }
    }

    public async Task Handle(DomainEvent.OrganizationUpdated @event, CancellationToken cancellationToken = default)
    {
        try
        {
            var existing = await organizationProjection.FindAsync(x => x.Id == @event.Id, cancellationToken);

            if (existing is null)
            {
                logger.LogWarning("Organization projection not found for update: {Id}.", @event.Id);
                return;
            }

            await organizationProjection.ReplaceInsertAsync(existing with
            {
                Name = @event.Name,
                UpdatedAt = @event.UpdatedAt
            }, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to update projection for organization: {Id}.", @event.Id);
            throw new InvalidOperationException($"Failed to update projection for organization: {@event.Id}.", ex);
        }
    }
}