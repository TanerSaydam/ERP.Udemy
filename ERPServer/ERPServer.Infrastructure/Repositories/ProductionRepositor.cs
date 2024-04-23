using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using ERPServer.Infrastructure.Context;
using GenericRepository;

namespace ERPServer.Infrastructure.Repositories;
internal sealed class ProductionRepositor : Repository<Production, ApplicationDbContext>, IProductionRepository
{
    public ProductionRepositor(ApplicationDbContext context) : base(context)
    {
    }
}
