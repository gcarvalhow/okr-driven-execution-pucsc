using Core.Persistence.Projection.Abstractions;
using Organization.Persistence.Context.Interfaces;

namespace Organization.Persistence.Context;

public class OrganizationProjectionDbContext(string connectionString) : MongoDbContext(connectionString), IOrganizationProjectionDbContext
{
}