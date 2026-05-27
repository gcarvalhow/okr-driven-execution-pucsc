using Identity.Application.Services;
using Identity.Domain.Aggregates;
using Identity.Infrastructure.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Web.ServiceInstallers.Authentication.Options.Configurations;

internal sealed class JwtBearerOptionsConfiguration(IOptions<JwtOptions> jwtOptions)
    : IConfigureOptions<JwtBearerOptions>
{
    private readonly JwtOptions _options = jwtOptions.Value;

    public void Configure(JwtBearerOptions options)
    {
        options.MapInboundClaims = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = _options.Issuer,
            ValidateAudience = true,
            ValidAudience = _options.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key)),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = async context =>
            {
                var sub = context.Principal?.FindFirst("sub")?.Value;
                var stamp = context.Principal?.FindFirst("security_stamp")?.Value;

                if (sub is null || stamp is null || !Guid.TryParse(sub, out var userId))
                {
                    context.Fail("Invalid token claims.");
                    return;
                }

                var appService = context.HttpContext.RequestServices.GetRequiredService<IIdentityApplicationService>();
                var userResult = await appService.LoadAggregateAsync<User>(userId, context.HttpContext.RequestAborted);

                if (!userResult.IsSuccess || userResult.Value.SecurityStamp.ToString() != stamp)
                    context.Fail("Token has been invalidated.");
            },
            OnChallenge = context =>
            {
                context.HandleResponse();
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                var error = context.AuthenticateFailure?.Message ?? "Unauthorized";
                return context.Response.WriteAsync($"{{\"error\":\"{error}\"}}");
            }
        };
    }
}