using Core.Infrastructure.EventBus.Configurations;
using MassTransit;
using Web.ServiceInstallers.EventBus.Extensions;

namespace Web.ServiceInstallers.EventBus.Consumers.OrganizationModule;

using OrganizationDomainEvents = Organization.Domain.Events.DomainEvent;

internal sealed class ConsumerConfiguration : IEventReceiveEndpointConfiguration
{
    private const string MODULE_NAME = "organization";

    public void AddEventReceiveEndpoints(IRabbitMqBusFactoryConfigurator rabbitMqBusFactoryConfigurator, IRegistrationContext registrationContext)
    {
        // Organization events
        rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<ProjectionOrganizationConsumer,
            OrganizationDomainEvents.OrganizationCreated>(registrationContext, MODULE_NAME);

        rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<ProjectionOrganizationConsumer,
            OrganizationDomainEvents.OrganizationUpdated>(registrationContext, MODULE_NAME);

        // Organization Member events
        rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<ProjectionOrganizationMemberConsumer,
            OrganizationDomainEvents.MemberAdded>(registrationContext, MODULE_NAME);
    }
}