using Core.Application.Services.Interfaces;
using Organization.Persistence.Context;

namespace Organization.Application.Services;

public interface IOrganizationApplicationService : IApplicationService<OrganizationDbContext>
{
}