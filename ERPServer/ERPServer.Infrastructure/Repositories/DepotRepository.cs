using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using ERPServer.Infrastructure.Context;
using GenericRepository;

namespace ERPServer.Infrastructure.Repositories;
internal sealed class DepotRepository : Repository<Depot, ApplicationDbContext>, IDepotRepository
{
    public DepotRepository(ApplicationDbContext context) : base(context)
    {
    }
}
