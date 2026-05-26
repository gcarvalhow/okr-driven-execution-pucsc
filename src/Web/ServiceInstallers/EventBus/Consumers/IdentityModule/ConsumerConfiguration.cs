using Core.Infrastructure.EventBus.Configurations;
using MassTransit;
using Web.ServiceInstallers.EventBus.Extensions;

namespace Web.ServiceInstallers.EventBus.Consumers.IdentityModule;

using IdentityDomainEvents = Identity.Domain.Events.DomainEvent;

internal sealed class ConsumerConfiguration : IEventReceiveEndpointConfiguration
{
    private const string MODULE_NAME = "identity";

    public void AddEventReceiveEndpoints(IRabbitMqBusFactoryConfigurator rabbitMqBusFactoryConfigurator, IRegistrationContext registrationContext)
    {
        // User events
        rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<ProjectionUserConsumer,
            IdentityDomainEvents.UserRegistered>(registrationContext, MODULE_NAME);

        rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<ProjectionUserConsumer,
            IdentityDomainEvents.UserNameUpdated>(registrationContext, MODULE_NAME);

        // RefreshToken events
        rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<ProjectionRefreshTokenConsumer,
            IdentityDomainEvents.UserRefreshTokenAdded>(registrationContext, MODULE_NAME);

        rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<ProjectionRefreshTokenConsumer,
            IdentityDomainEvents.UserRefreshTokenRevoked>(registrationContext, MODULE_NAME);
    }
}