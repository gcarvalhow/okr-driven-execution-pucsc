using Identity.Application.UseCases.Events;
using Identity.Domain.Events;
using MassTransit;

namespace Web.ServiceInstallers.EventBus.Consumers.IdentityModule;

public class ProjectionUserConsumer(IProjectionUserEventHandler eventHandler) :
    IConsumer<DomainEvent.UserRegistered>,
    IConsumer<DomainEvent.UserNameUpdated>
{
    public Task Consume(ConsumeContext<DomainEvent.UserRegistered> context)
        => eventHandler.Handle(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.UserNameUpdated> context)
        => eventHandler.Handle(context.Message, context.CancellationToken);
}