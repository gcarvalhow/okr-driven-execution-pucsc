using Core.Persistence.Configurations;

namespace Identity.Persistence.Configurations;

using UserAggregate = Identity.Domain.Aggregates.User;

public class SnapshotConfiguration
{
    public class UserSnapshotConfiguration : SnapshotConfiguration<UserAggregate>;
}