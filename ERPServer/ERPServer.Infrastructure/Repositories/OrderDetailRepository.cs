using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using ERPServer.Infrastructure.Context;
using GenericRepository;

namespace ERPServer.Infrastructure.Repositories;

internal sealed class OrderDetailRepository : Repository<OrderDetail, ApplicationDbContext>, IOrderDetailRepository
{
    public OrderDetailRepository(ApplicationDbContext context) : base(context)
    {
    }
}