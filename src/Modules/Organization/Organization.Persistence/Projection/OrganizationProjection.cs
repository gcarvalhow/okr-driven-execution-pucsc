using Core.Domain.Primitives.Interfaces;
using Core.Persistence.Projection;
using Organization.Persistence.Context.Interfaces;

namespace Organization.Persistence.Projection;

public class OrganizationProjection<TProjection>(IOrganizationProjectionDbContext context) : Projection<TProjection>(context), IOrganizationProjection<TProjection>
    where TProjection : IProjectionModel
{
}