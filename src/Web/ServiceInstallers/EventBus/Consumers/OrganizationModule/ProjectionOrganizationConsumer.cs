using MassTransit;
using Organization.Application.UseCases.Events;
using Organization.Domain.Events;

namespace Web.ServiceInstallers.EventBus.Consumers.OrganizationModule;

public class ProjectionOrganizationConsumer(IProjectionOrganizationEventHandler eventHandler) :
    IConsumer<DomainEvent.OrganizationCreated>,
    IConsumer<DomainEvent.OrganizationUpdated>
{
    public Task Consume(ConsumeContext<DomainEvent.OrganizationCreated> context)
        => eventHandler.Handle(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.OrganizationUpdated> context)
        => eventHandler.Handle(context.Message, context.CancellationToken);
}