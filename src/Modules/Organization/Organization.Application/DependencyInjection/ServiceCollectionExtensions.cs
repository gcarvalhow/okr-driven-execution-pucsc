using Microsoft.Extensions.DependencyInjection;
using Organization.Application.UseCases.Events;

namespace Organization.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEventInteractors(this IServiceCollection services)
        => services
            .AddScoped<IProjectionOrganizationEventHandler, ProjectionOrganizationEventHandler>()
            .AddScoped<IProjectionOrganizationMemberEventHandler, ProjectionOrganizationMemberEventHandler>();
}