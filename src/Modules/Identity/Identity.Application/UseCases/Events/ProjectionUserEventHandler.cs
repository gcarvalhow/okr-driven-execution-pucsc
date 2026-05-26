using Core.Application.EventBus;
using Identity.Domain.Events;
using Identity.Domain.Projection;
using Identity.Persistence.Projection;
using Microsoft.Extensions.Logging;

namespace Identity.Application.UseCases.Events;

public interface IProjectionUserEventHandler : 
    IEventHandler<DomainEvent.UserRegistered>,
    IEventHandler<DomainEvent.UserNameUpdated>;

public class ProjectionUserEventHandler(
    IIdentityProjection<ProjectionModel.User> userProjection,
    ILogger<ProjectionUserEventHandler> logger
) : IProjectionUserEventHandler
{
    public async Task Handle(DomainEvent.UserRegistered @event, CancellationToken cancellationToken = default)
    {
        try
        {
            await userProjection.ReplaceInsertAsync(new(
                @event.Id,
                @event.GoogleId,
                @event.Name,
                @event.Email,
                @event.GoogleAvatarUrl,
                @event.CreatedAt,
                null
            ), cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to create user projection: {Id}.", @event.Id);
            throw new InvalidOperationException($"Failed to create user projection: {@event.Id}.", ex);
        }
    }

    public async Task Handle(DomainEvent.UserNameUpdated @event, CancellationToken cancellationToken = default)
    {
        try
        {
            var existing = await userProjection.FindAsync(x => x.Id == @event.Id, cancellationToken);

            if (existing is null)
            {
                logger.LogWarning("User projection not found for update: {Id}.", @event.Id);
                return;
            }

            await userProjection.ReplaceInsertAsync(existing with
            {
                Name = @event.Name,
                UpdatedAt = @event.UpdatedAt
            }, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to update projection for user: {Id}.", @event.Id);
            throw new InvalidOperationException($"Failed to update projection for user: {@event.Id}.", ex);
        }
    }
}