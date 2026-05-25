using Core.Persistence.Configurations;

namespace Identity.Persistence.Configurations;

using UserAggregate = Identity.Domain.Aggregates.User;

public class StoreEventConfiguration
{
    public class UserStoreEventConfiguration : StoreEventConfiguration<UserAggregate>;
}