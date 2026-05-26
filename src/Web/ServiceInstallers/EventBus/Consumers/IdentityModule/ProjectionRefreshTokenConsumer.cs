using Identity.Application.UseCases.Events;
using Identity.Domain.Events;
using MassTransit;

namespace Web.ServiceInstallers.EventBus.Consumers.IdentityModule;

public class ProjectionRefreshTokenConsumer(IProjectionRefreshTokenEventHandler eventHandler) :
    IConsumer<DomainEvent.UserRefreshTokenAdded>,
    IConsumer<DomainEvent.UserRefreshTokenRevoked>
{
    public Task Consume(ConsumeContext<DomainEvent.UserRefreshTokenAdded> context)
        => eventHandler.Handle(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.UserRefreshTokenRevoked> context)
        => eventHandler.Handle(context.Message, context.CancellationToken);
}