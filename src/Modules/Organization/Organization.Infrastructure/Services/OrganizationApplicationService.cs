using Core.Application.EventBus;
using Core.Application.EventStore;
using Core.Application.Services;
using Core.Application.UnitOfWork;
using Organization.Application.Services;
using Organization.Persistence.Context;

namespace Organization.Infrastructure.Services;

public class OrganizationApplicationService(IEventStore<OrganizationDbContext> eventStore, IEventBus eventBusGateway, IUnitOfWork<OrganizationDbContext> unitOfWork)
    : ApplicationService<OrganizationDbContext>(eventStore, eventBusGateway, unitOfWork), IOrganizationApplicationService
{
}