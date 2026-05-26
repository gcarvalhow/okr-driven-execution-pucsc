using MassTransit;
using Organization.Application.UseCases.Events;
using Organization.Domain.Events;

namespace Web.ServiceInstallers.EventBus.Consumers.OrganizationModule;

public class ProjectionOrganizationMemberConsumer(IProjectionOrganizationMemberEventHandler eventHandler) :
    IConsumer<DomainEvent.MemberAdded>
{
    public Task Consume(ConsumeContext<DomainEvent.MemberAdded> context)
        => eventHandler.Handle(context.Message, context.CancellationToken);
}