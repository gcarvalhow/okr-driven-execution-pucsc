using Core.Application.EventStore;
using Core.Application.UnitOfWork;
using Core.Infrastructure.Configurations;
using Core.Persistence.EventStore;
using Core.Persistence.Extensions;
using Core.Persistence.Options;
using Core.Persistence.UnitOfWork;
using Organization.Persistence.Context;
using Organization.Persistence.Context.Interfaces;
using Organization.Persistence.Projection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Organization.Persistence.Constants;

namespace Organization.Infrastructure.ServiceInstallers;

internal sealed class PersistenceServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IOrganizationProjection<>), typeof(OrganizationProjection<>));

        // Register Event Store
        services.AddScoped(typeof(IEventStore<OrganizationDbContext>), typeof(EventStore<OrganizationDbContext>));

        // Register Unit of Work
        services.AddScoped(typeof(IUnitOfWork<OrganizationDbContext>), typeof(UnitOfWork<OrganizationDbContext>));

        services.AddScoped<IOrganizationProjectionDbContext>(provider =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            string connectionString = configuration.GetSection("Projections").GetValue<string>("Organization");

            return new OrganizationProjectionDbContext(connectionString);
        })
        .AddDbContext<OrganizationDbContext>((provider, builder) =>
        {
            ConnectionStringOptions connectionString = provider.GetService<IOptions<ConnectionStringOptions>>()!.Value;

            builder.UseNpgsql(
                connectionString: connectionString,
                dbContextOptionsBuilder => dbContextOptionsBuilder.WithMigrationHistoryTableInSchema(Schemas.Organization));
        });
    }
}