using Identity.Domain.Aggregates;
using System.Security.Claims;

namespace Identity.Application.Services.Abstractions;

public interface ITokenService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken(User user);
    ClaimsPrincipal? ValidateRefreshToken(string token);
}