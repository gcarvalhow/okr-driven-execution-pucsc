using Core.Domain.Primitives.Interfaces;
using Core.Domain.Projection;

namespace Organization.Persistence.Projection;

public interface IOrganizationProjection<TProjection> : IProjection<TProjection>
    where TProjection : IProjectionModel
{
}