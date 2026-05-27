using Core.Infrastructure.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Web.ServiceInstallers.Authentication.Options.Configurations;

namespace Web.ServiceInstallers.Authentication;

internal sealed class AuthenticationServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services
            .ConfigureOptions<JwtBearerOptionsConfiguration>()
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();
    }
}