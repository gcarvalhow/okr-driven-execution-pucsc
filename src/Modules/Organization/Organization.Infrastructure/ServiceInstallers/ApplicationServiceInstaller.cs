using Core.Application.Behaviors;
using Core.Infrastructure.Configurations;
using Core.Shared.Extensions;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Organization.Application.DependencyInjection;
using Organization.Application.Services;
using Organization.Infrastructure.Services;

namespace Organization.Infrastructure.ServiceInstallers;

internal sealed class ApplicationServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.Tap(services.TryAddTransient<IOrganizationApplicationService, OrganizationApplicationService>);
        services.AddEventInteractors();

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly);

            config.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
            config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });

        services.AddValidatorsFromAssembly(Application.AssemblyReference.Assembly);
    }
}